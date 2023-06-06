using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopSoSanh.Migrations
{
    public partial class Delete_Table_ProductTracking_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1B3D7E19-B1A5-4CA2-A491-54593FA16531",
                column: "ConcurrencyStamp",
                value: "5514ca13-578b-4850-93ad-4ddd50f36179");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8D04DCE2-969A-435D-BBA4-DF3F325983DC",
                column: "ConcurrencyStamp",
                value: "9c9b0ebc-ab4c-4d59-9fec-e66f076296d9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69BD714F-9576-45BA-B5B7-F00649BE00DE",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOjp3XCftW3Dc2oxrOf9JtRiJPSQfLV5Uk6P5To8EEe2ii246ORE5QHXmOmbjN7Qew==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductTrackings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAutoOrder = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTrackings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTrackings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTrackings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1B3D7E19-B1A5-4CA2-A491-54593FA16531",
                column: "ConcurrencyStamp",
                value: "c4c878a0-0c2a-449b-b558-0c9d0cbda347");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8D04DCE2-969A-435D-BBA4-DF3F325983DC",
                column: "ConcurrencyStamp",
                value: "1975dc73-bee9-4362-899e-fd1fbc9e14bf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69BD714F-9576-45BA-B5B7-F00649BE00DE",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKjSffZjACbh5O8uS5peCczLBXnERBH6vlLCNGwXwB1jw9z+bsvytxSMGgy+G2SDiQ==");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTrackings_LocationId",
                table: "ProductTrackings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTrackings_ProductId",
                table: "ProductTrackings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTrackings_UserId",
                table: "ProductTrackings",
                column: "UserId");
        }
    }
}
