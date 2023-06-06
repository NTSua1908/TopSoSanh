using Microsoft.EntityFrameworkCore;
using TopSoSanh.DTO;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class NotificationService : INotificationService
    {
        private readonly ApiDbContext _dbContext;
        private readonly UserResolverService _userResolverService;

        public NotificationService(ApiDbContext dbContext, UserResolverService userResolverService)
        {
            _dbContext = dbContext;
            _userResolverService = userResolverService;
        }

        public NotificationDetailModel GetDetail(Guid notificationId, ErrorModel errors)
        {
            var notification = _dbContext.Notifications.Where(x => x.Id == notificationId && x.UserId == _userResolverService.GetUser())
                                                        .Include(x => x.Product).FirstOrDefault();

            if (notification == null)
            {
                errors.Add(String.Format(ErrorResource.NotFound, "Notification"));
                return new NotificationDetailModel();
            }
            return new NotificationDetailModel(notification);
        }

        public PaginationDataModel<NotificationModel> GetAll(PaginationRequestModel req)
        {
            var notifications = _dbContext.Notifications.Where(x => x.UserId == _userResolverService.GetUser()).Include(x => x.Product).AsQueryable();
            if (!string.IsNullOrEmpty(req.SearchText))
            {
                string searchText = req.SearchText.ToLower();
                notifications = notifications.Where(x => x.Address.ToLower().Contains(searchText)
                                                      || x.Commune.ToLower().Contains(searchText)
                                                      || x.District.ToLower().Contains(searchText)
                                                      || x.Province.ToLower().Contains(searchText)
                                                      || x.Product.Name.ToLower().Contains(searchText)
                                                      || x.UserName.ToLower().Contains(searchText)
                                                      || x.OrderName.ToLower().Contains(searchText)
                                                      || x.Email.ToLower().Contains(searchText)
                                                      || x.OrderEmail.ToLower().Contains(searchText)
                                                      || x.PhoneNumber.ToLower().Contains(searchText));
            }
            return new PaginationDataModel<NotificationModel>(notifications.Select(x => new NotificationModel(x)).ToList(), req);
        }

        public void Update(Guid id, NotificationUpdateModel model, ErrorModel errors)
        {
            var notification = _dbContext.Notifications.Where(x => x.Id == id && x.UserId == _userResolverService.GetUser()).FirstOrDefault();
            if (notification == null)
            {
                errors.Add(string.Format(ErrorResource.NotFound, "Notification"));
                return;
            }
            model.UpdateEntity(notification);
            _dbContext.SaveChanges();
        }

        public void ToggleNotification(Guid notificationId, ErrorModel errors)
        {
            var notification = _dbContext.Notifications.Where(x => x.Id == notificationId && x.UserId == _userResolverService.GetUser()).FirstOrDefault();
            if (notification == null)
            {
                errors.Add(string.Format(ErrorResource.NotFound, "Notification"));
                return;
            }
            notification.IsActive = !notification.IsActive;
            _dbContext.SaveChanges();
        }

        public void ToggleAutoOrder(Guid notificationId, ErrorModel errors)
        {
            var notification = _dbContext.Notifications.Where(x => x.Id == notificationId && x.UserId == _userResolverService.GetUser()).FirstOrDefault();
            if (notification == null)
            {
                errors.Add(string.Format(ErrorResource.NotFound, "Notification"));
                return;
            }
            notification.IsAutoOrder = !notification.IsAutoOrder;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid notificationId, ErrorModel errors)
        {
            var notification = _dbContext.Notifications.Where(x => x.Id == notificationId && x.UserId == _userResolverService.GetUser()).FirstOrDefault();
            if (notification == null)
            {
                errors.Add(string.Format(ErrorResource.NotFound, "Notification"));
                return;
            }
            _dbContext.Notifications.Remove(notification);
            _dbContext.SaveChanges();
        }
    }
}
