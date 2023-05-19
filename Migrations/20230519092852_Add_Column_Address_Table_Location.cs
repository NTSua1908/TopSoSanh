using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopSoSanh.Migrations
{
    public partial class Add_Column_Address_Table_Location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Locations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1B3D7E19-B1A5-4CA2-A491-54593FA16531",
                column: "ConcurrencyStamp",
                value: "15ce5a28-132c-4063-ba9b-eb57badf9a7e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8D04DCE2-969A-435D-BBA4-DF3F325983DC",
                column: "ConcurrencyStamp",
                value: "efecb83f-241f-43aa-bb64-2110cbb95ee2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69BD714F-9576-45BA-B5B7-F00649BE00DE",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECS6ueTAsV2rZCCqWMn3Smqu4GTTuwyExBJJdDpk0DWRbtubLf8GmEgM7IAx6H68YA==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Locations");

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
    }
}
