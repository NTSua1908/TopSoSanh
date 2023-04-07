namespace TopSoSanh.Helper
{
    public static class ConstanstHelper
    {
        public static class EmailConstant
        {
            public const string EmailTracking = "<div class=\"main\">" +
               "<p>Chào {0},</p>" +
               "<p>Sản phẩm bạn quan tâm đang giảm giá. Nhấn vào <a href='{1}'>đây</a> để xem chi tiết trên website. " +
               "Thời gian giảm giá có hạn.</p>" +
               "<div class=\"container\">" +
               "<h3>{2}</h3>" +
               "<div><img src=\"{3}\" alt=\"\"></div></div>" +
               "<p>Nhấn vào <a href='{4}'>đây</a> để hủy theo dõi sản phẩm.</p>" +
               "<p>Cảm ơn,<br>Đội ngũ phát triển TopSoSanh.</p>";

            public const string ConfirmEmail = "<p>Chào {0},</p>" +
               "<p>Tài khoản của bạn đã được tạo thành công. Vui lòng nhấn vào <a href='{1}'>đây</a> để xác nhận email của bạn. Thời gian hiệu lực trong vòng 24h." +
               "<p>Cảm ơn,<br>Đội ngũ phát triển TopSoSanh.</p>";

            public const string ResetPassword = "<p>Chào {0},</p>" +
               "<p>Tài khoản của bạn đã được đặt lại mật khẩu thành công. Mật khẩu mới của bạn là </p>" +
               "<h3><b>{1}</b></h3>" +
               "<p>Cảm ơn,<br>Đội ngũ phát triển TopSoSanh.</p>";
        }

        public static class RoleConstant
        {
            public const string Admin = "0";
            public const string User = "1";
        }

        public static class CrawlConstant
        {
            public const int Amount = 10;
        }

        public static string GetFeDomain(IConfiguration Configuration)
        {
            return Configuration.GetSection("Domain:FeDomain").Value;
        }
    }
}
