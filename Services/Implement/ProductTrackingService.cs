using Hangfire;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using TopSoSanh.DTO;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class ProductTrackingService : IProductTrackingService
    {
        private readonly ICrawlDataGearvnService _crawlDataGearvnService;
        private readonly ICrawlDataAnphatService _crawlDataAnphatService;
        private readonly ICrawlDataAnkhangService _crawlDataAnkhangService;
        private readonly ISendMailService _sendMailService;
        private readonly ApiDbContext _dbContext;

        public ProductTrackingService(ICrawlDataGearvnService crawlDataGearvnService, 
            ICrawlDataAnphatService crawlDataAnphatService, 
            ICrawlDataAnkhangService crawlDataAnkhangService, 
            ISendMailService sendMailService, 
            ApiDbContext dbContext)
        {
            _crawlDataGearvnService = crawlDataGearvnService;
            _crawlDataAnphatService = crawlDataAnphatService;
            _crawlDataAnkhangService = crawlDataAnkhangService;
            _sendMailService = sendMailService;
            _dbContext = dbContext;
        }

        public void SubscribeProduct(SubscribeProductModel model)
        {
            Product product = _dbContext.Products
                    .Where(x => x.ItemUrl.ToLower().Equals(model.ProductUrl.ToLower())
                    && x.Name.ToLower().Equals(model.ProductName.ToLower()) 
                    && x.ImageUrl.ToLower().Equals(model.ImageUrl.ToLower())).AsNoTracking().FirstOrDefault();

            if (product == null)
            {
                product = new Product()
                {
                    ImageUrl = model.ImageUrl,
                    ItemUrl = model.ProductUrl,
                    Name = model.ProductName
                };
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                //add hangfire
                RecurringJob.AddOrUpdate<IProductTrackingService>(Guid.NewGuid().ToString(), x => x.ProductTracking(product.ItemUrl), Cron.Hourly);
            }

            var notification = new Notification()
            {
                Email = model.Email,
                Price = model.Price,
                ProductId = product.Id,
                UserName = model.UserName
            };

            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
        }

        public void ProductTracking(string productUrl)
        {
            double newPrice = 0;
            if (productUrl.Contains("gearvn.com"))
                newPrice = _crawlDataGearvnService.CrawlPrice(productUrl);
            else if (productUrl.Contains("anphatpc.com.vn"))
                newPrice = _crawlDataAnphatService.CrawlPrice(productUrl);
            else if (productUrl.Contains("ankhang.vn"))
                newPrice = _crawlDataAnkhangService.CrawlPrice(productUrl);

            Product product = _dbContext.Products.AsNoTracking()
                    .Where(x => x.ItemUrl.ToLower().Equals(productUrl.ToLower())).First();

            int hour = DateTime.Now.Hour;

            var priceFluctuation = _dbContext.PriceFluctuations
                .Where(x => x.ProductId == product.Id && x.Hour == hour).FirstOrDefault();

            if (priceFluctuation == null)
            {
                priceFluctuation = new PriceFluctuation()
                {
                    Price = newPrice,
                    ProductId = product.Id,
                    Hour = hour
                };
                _dbContext.PriceFluctuations.Add(priceFluctuation);
            }
            else priceFluctuation.Price = newPrice;

            var usersSubscribe = _dbContext.Notifications.AsNoTracking()
                .Where(x => x.ProductId == product.Id && x.Price >= newPrice);
            foreach (var user in usersSubscribe)
            {
                _sendMailService.SendMailAsync(new Helper.MailContent()
                {
                    To = user.Email,
                    Subject = "Thông báo thông tin giảm giá",
                    UserName = user.UserName,
                    ItemUrl = product.ItemUrl,
                    ImageUrl = product.ImageUrl,
                    ItemName = product.Name
                });
            }

            LogHelper.LogWrite(newPrice + " " + product.ItemUrl);

            _dbContext.SaveChanges();
        }

        public List<double> GetTrackingResult(Guid productId)
        {
            double[] result = _dbContext.PriceFluctuations
                .Where(x => x.ProductId == productId)
                .Select(x => x.Price).ToArray();

            int currentHour = DateTime.Now.Hour;

            for (int i = 0; i < result.Length - 1; i++)
            {
                for (int j = i + 1; j < result.Length; j++)
                {
                    if ((result[i] >= currentHour && result[j] > currentHour
                        || result[i] <= currentHour && result[j] < currentHour)
                        && result[i] > result[j]
                        || result[i] <= currentHour && result[j] > currentHour)
                    {
                        double temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }

            return result.ToList();
        }
    }
}
