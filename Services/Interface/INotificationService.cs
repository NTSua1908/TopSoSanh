using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface INotificationService
    {
        NotificationDetailModel GetDetail(Guid notificationId, ErrorModel errors);
        PaginationDataModel<NotificationModel> GetAll(PaginationRequestModel req);
        void Update(Guid id, NotificationUpdateModel model, ErrorModel errors);
        void ToggleNotification(Guid notificationId, ErrorModel errors);
        void ToggleAutoOrder(Guid notificationId, ErrorModel errors);
        void Delete(Guid notificationId, ErrorModel errors);
    }
}
