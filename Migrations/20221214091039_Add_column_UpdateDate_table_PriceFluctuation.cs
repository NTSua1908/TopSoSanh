using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopSoSanh.Migrations
{
    public partial class Add_column_UpdateDate_table_PriceFluctuation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "PriceFluctuations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "PriceFluctuations");
        }
    }
}
