﻿using Hangfire;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICrawlDataCustomShopService _crawlDataCustomShopService;
        private readonly IAutoOrderService _autoOrderService;
        private readonly ISendMailService _sendMailService;
        private readonly UserResolverService _userResolverService;
        private readonly ApiDbContext _dbContext;

        public ProductTrackingService(ICrawlDataGearvnService crawlDataGearvnService,
            ICrawlDataAnphatService crawlDataAnphatService,
            ICrawlDataAnkhangService crawlDataAnkhangService,
            ICrawlDataCustomShopService crawlDataCustomShopService,
            IAutoOrderService autoOrderService,
            ISendMailService sendMailService,
            UserResolverService userResolverService,
            ApiDbContext dbContext)
        {
            _crawlDataGearvnService = crawlDataGearvnService;
            _crawlDataAnphatService = crawlDataAnphatService;
            _crawlDataAnkhangService = crawlDataAnkhangService;
            _crawlDataCustomShopService = crawlDataCustomShopService;
            _autoOrderService = autoOrderService;
            _sendMailService = sendMailService;
            _userResolverService = userResolverService;
            _dbContext = dbContext;
        }

        public void SubscribeProduct(SubscribeProductModel model, string hostName, ErrorModel errors)
        {
            Product? product = _dbContext.Products
                    .Where(x => x.ItemUrl.ToLower().Equals(model.ProductUrl.ToLower())
                    && x.Name.ToLower().Equals(model.ProductName.ToLower())
                    && x.ImageUrl.ToLower().Equals(model.ImageUrl.ToLower())).AsNoTracking().FirstOrDefault();

			Location location = null;
			if (model.LocationId.HasValue)
			{
				location = _dbContext.Locations.FirstOrDefault(x => x.Id == model.LocationId && x.UserId == _userResolverService.GetUser());
			}

			if (model.IsAutoOrder && location == null)
            {
                errors.Add(String.Format(ErrorResource.NotFound, "Location"));
                return;
            }


            if (product == null)
            {
                product = new Product()
                {
                    ImageUrl = model.ImageUrl,
                    ItemUrl = model.ProductUrl,
                    Name = model.ProductName,
                    Shop = model.ProductUrl.Contains("gearvn") ? Shop.Gearvn : model.ProductUrl.Contains("anphatpc") ? Shop.Anphat : Shop.Ankhang
                };
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                //add hangfire
                RecurringJob.AddOrUpdate<IProductTrackingService>(Guid.NewGuid().ToString(), x => x.ProductTracking(product.ItemUrl, hostName), Cron.Hourly);
            }

            

            var notification = new Notification()
            {
                Email = model.Email,
                Price = model.Price,
                ProductId = product.Id,
                UserName = model.UserName,
                IsAutoOrder = model.IsAutoOrder,
                UserId = _userResolverService.GetUser(),
                Address = location?.Address,
                District = location?.District,
                Commune = location?.Commune,
                Province = location?.Province,
                OrderEmail = location?.Email,
                OrderName = location?.Name,
                PhoneNumber = location?.PhoneNumber
            };

            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
        }

        public void SubscribeProductFromCustomShop(SubscribeProductCustomModel model, string hostName)
        {
            Product product = new Product()
            {
                ImageUrl = model.ImageUrl,
                ItemUrl = model.ProductUrl,
                Name = model.ProductName
            };
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            //add hangfire
            RecurringJob.AddOrUpdate<IProductTrackingService>(Guid.NewGuid().ToString(), x => x.ProductTrackingCustom(product.ItemUrl, model.ProductPriceSelector, hostName), Cron.Hourly);

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

        public void ProductTracking(string productUrl, string hostName)
        {
            double newPrice = 0;
            Product product = _dbContext.Products.AsNoTracking()
                    .Where(x => x.ItemUrl.ToLower().Equals(productUrl.ToLower())).First();

            switch (product.Shop)
            {
                case Shop.Anphat:
                    newPrice = _crawlDataAnphatService.CrawlPrice(productUrl);
                    break;
                case Shop.Gearvn:
                    newPrice = _crawlDataGearvnService.CrawlPrice(productUrl);
                    break;
                case Shop.Ankhang:
                    newPrice = _crawlDataAnkhangService.CrawlPrice(productUrl);
                    break;
                default:
                    break;
            }

            int hour = DateTime.Now.Hour;

            var priceFluctuation = _dbContext.PriceFluctuations
                .Where(x => x.ProductId == product.Id && x.UpdatedDate.Hour == hour).FirstOrDefault();

            if (priceFluctuation == null)
            {
                priceFluctuation = new PriceFluctuation()
                {
                    Price = newPrice,
                    ProductId = product.Id,
                    UpdatedDate = DateTime.Now
                };
                _dbContext.PriceFluctuations.Add(priceFluctuation);
            }
            else
            {
                priceFluctuation.Price = newPrice;
                priceFluctuation.UpdatedDate = DateTime.Now;
            }

            var notifications = _dbContext.Notifications
                .Where(x => x.ProductId == product.Id && x.Price >= newPrice && x.IsActive).Include(x => x.Product);

            foreach (var notification in notifications)
            {
                if (!notification.IsAutoOrder)
                {
					_sendMailService.SendMailTrackingAsync(
							new Helper.MailContent()
							{
								To = notification.Email,
								Subject = "Thông báo thông tin giảm giá"
							},
							notification.UserName,
							product.Name,
							product.ItemUrl,
							product.ImageUrl,
							"https://" +
								hostName +
								$"/api/ProductTracking/UnSubscribe?email={notification.Email}&token={notification.Id}"
						);
				} 
                else
                {
                    Func<Notification, string, bool> autoOrder;
                    switch (notification.Product.Shop)
                    {
                        case Shop.Anphat:
                            autoOrder = _autoOrderService.OrderAnPhat; break;
						case Shop.Ankhang:
							autoOrder = _autoOrderService.OrderAnKhang; break;
                        default:
							autoOrder = _autoOrderService.OrderGearvn; break;
					}

                    if (autoOrder(notification, productUrl))
                    {
                        _sendMailService.SendMailOrderAsync(
                            new Helper.MailContent()
                            {
                                To = notification.Email,
                                Subject = "Thông báo đặt hàng thành công"
                            },
                            notification.UserName,
                            product.Name,
                            product.ItemUrl,
                            product.ImageUrl,
                            "https://" +
                                hostName +
                                $"/api/ProductTracking/UnSubscribe?email={notification.Email}&token={notification.Id}",
                            notification
                        );
                        notification.IsActive = false;
                    }
                }
            }

            LogHelper.LogWrite(newPrice + " " + product.ItemUrl);
            _dbContext.SaveChanges();
        }

        public void ProductTrackingCustom(string productUrl, string priceSelector, string hostName)
        {
            double newPrice = _crawlDataCustomShopService.CrawlPrice(productUrl, priceSelector);

            Product product = _dbContext.Products.AsNoTracking()
                    .Where(x => x.ItemUrl.ToLower().Equals(productUrl.ToLower())).First();

            int hour = DateTime.Now.Hour;

            var priceFluctuation = _dbContext.PriceFluctuations
                .Where(x => x.ProductId == product.Id && x.UpdatedDate.Hour == hour).FirstOrDefault();

            if (priceFluctuation == null)
            {
                priceFluctuation = new PriceFluctuation()
                {
                    Price = newPrice,
                    ProductId = product.Id,
                    UpdatedDate = DateTime.Now
                };
                _dbContext.PriceFluctuations.Add(priceFluctuation);
            }
            else
            {
                priceFluctuation.Price = newPrice;
                priceFluctuation.UpdatedDate = DateTime.Now;
            }

            var usersSubscribe = _dbContext.Notifications.AsNoTracking()
                .Where(x => x.ProductId == product.Id && x.Price >= newPrice);
            foreach (var user in usersSubscribe)
            {
                _sendMailService.SendMailTrackingAsync(new Helper.MailContent()
                    {
                        To = user.Email,
                        Subject = "Thông báo thông tin giảm giá"
                    },
                    user.UserName,
                    product.ItemUrl,
                    product.ImageUrl,
                    product.Name,
                    "https://" + hostName + $"/api/ProductTracking/UnSubscribe?email={user.Email}&token={user.Id}"
                );
            }

            LogHelper.LogWrite(newPrice + " " + product.ItemUrl);

            _dbContext.SaveChanges();
        }

        public TrackingResultModel GetTrackingResult(string productUrl)
        {
            var query = _dbContext.PriceFluctuations
                .Where(x => x.Product.ItemUrl == productUrl)
                .AsNoTracking()
                .OrderBy(x => x.UpdatedDate.Hour)
                .Select(x => new
                {
                    Price = x.Price,
                    Hour = x.UpdatedDate.Hour
                })
                .ToList();

            TrackingResultModel result = new TrackingResultModel();
            result.Hours = query.Select(x => x.Hour + ":00");
            result.Prices = query.Select(x => x.Price);

            return result;
        }

        public string UnSubscribeProduct(string email, string token)
        {
            var notification = _dbContext.Notifications
                .Where(x => x.Id.ToString() == token && x.Email == email)
                .FirstOrDefault();

            if (notification == null)
            {
                return "Hủy theo dõi sản phẩm KHÔNG thành công";
            }
            else
            {
                _dbContext.Notifications.Remove(notification);
                _dbContext.SaveChanges();
                return "Hủy theo dõi sản phẩm thành công";
            }
        }

        
    }
}
