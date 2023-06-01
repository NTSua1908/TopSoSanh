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

            public const string ConfirmOrder = 
              "<html><head><style> .main {{ max-width: 600px; border: 10px solid #be7a65; background-color: #ebe8d2; border-radius: 20px; padding: 20px; }} " +
                ".greeting {{ font-size: 16px; }} .center {{ margin: auto;}} .info {{ margin: 0 auto;}} " +
                ".info-container h3{{ text-align: center; }} .info-container .info div img {{ display: block; border: 3px solid #be7a65; border-radius: 10px; margin: auto; }}  td {{ padding: 10px 20px 0 0px; }} tr td:first-child {{ font-weight: 600; }}</style></head>" +
                "<body><div class=\"main\">" +
                "<div class=\"greeting\"> <p>Chào {0},</p> <p>Sản phẩm bạn quan tâm đã được đặt hàng thành công. Nhấn vào <a href='{1}'>đây</a> để xem chi tiết sản phẩm trên website.</p></div><div class=\"center\"> <div class=\"container\"> " +
                "<div class=\"order-container\"><div class=\"order\">  <h3>Thông tin nhận hàng</h3>  <table>      <tr>   <td>Tên người nhận</td><td>{2}</td></tr><tr>   <td>Email</td><td>{3}</td></tr><tr>   " +
                "<td>Số điện thoại</td><td>{4}</td></tr><tr><td>Hình thức thanh toán</td><td>COD (Thanh toán khi nhận hàng)</td></tr><tr><td>Địa chỉ đặt hàng</td><td>{5}</td>" +
                "</tr></table></div></div> <div class=\"info-container\"><div class=\"info\">  <h3>{6}</h3>  <div><img src='{7}' alt=\"\"   style=\"max-width: 200px;\"></div>" +
                "</div> </div> <p>Đơn hàng của bạn hiện đang được xử lý. Bên bán sẽ sớm liên hệ để giao hàng.</p> <p>Nhấn vào <a href='{8}'>đây</a> để hủy theo dõi sản phẩm.</p> <p>Cảm ơn,<br>Đội ngũ phát triển TopSoSanh.</p> </div></div></div><body></html>";

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
