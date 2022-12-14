using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopSoSanh.Migrations
{
    public partial class Delete_column_Hour_table_PriceFluctuation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "PriceFluctuations");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "PriceFluctuations",
                newName: "UpdatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "PriceFluctuations",
                newName: "UpdateDate");

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "PriceFluctuations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
