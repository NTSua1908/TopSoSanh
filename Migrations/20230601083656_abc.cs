using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopSoSanh.Migrations
{
    public partial class abc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Locations_LocationId",
                table: "Notifications");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Notifications",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Commune",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OrderEmail",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Notifications",
                type: "longtext",
                nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Locations_LocationId",
                table: "Notifications",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Locations_LocationId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Commune",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "OrderEmail",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Notifications");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Notifications",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1B3D7E19-B1A5-4CA2-A491-54593FA16531",
                column: "ConcurrencyStamp",
                value: "da5f5782-9aef-4130-8780-03be356446b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8D04DCE2-969A-435D-BBA4-DF3F325983DC",
                column: "ConcurrencyStamp",
                value: "0616ba64-c45e-482a-a9b7-265a1686224e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69BD714F-9576-45BA-B5B7-F00649BE00DE",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEDQo2gxEx7dz+kcAvY3LnmCCJGbQOF4G4cro7CP1fwhrW0B9y8652WbTxlPWCk69lw==");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Locations_LocationId",
                table: "Notifications",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
