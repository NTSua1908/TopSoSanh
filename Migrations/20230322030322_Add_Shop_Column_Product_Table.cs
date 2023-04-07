using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopSoSanh.Migrations
{
    public partial class Add_Shop_Column_Product_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Shop",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1B3D7E19-B1A5-4CA2-A491-54593FA16531",
                column: "ConcurrencyStamp",
                value: "4d9614f9-ea83-449e-abc8-1baa042f90b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8D04DCE2-969A-435D-BBA4-DF3F325983DC",
                column: "ConcurrencyStamp",
                value: "108eb045-a289-44f5-aef7-f1d34418d322");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69BD714F-9576-45BA-B5B7-F00649BE00DE",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJBR8o4FFJBgElVjUZRsWTJgCncCLWJzwpDzJBMrrTzS/s9m8BwLOX15T6mK5upD2g==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shop",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1B3D7E19-B1A5-4CA2-A491-54593FA16531",
                column: "ConcurrencyStamp",
                value: "3b2be423-dde3-4cb3-9541-c33a5d04fdb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8D04DCE2-969A-435D-BBA4-DF3F325983DC",
                column: "ConcurrencyStamp",
                value: "987d8b4b-95cb-4993-9a81-9c5cf507ff0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69BD714F-9576-45BA-B5B7-F00649BE00DE",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBb3h/DPNx8pTdzrCqQGYWZgPwhwBDBBQSW3gzXundyDqswvUAgfxYlAYPoxb8H6lQ==");
        }
    }
}
