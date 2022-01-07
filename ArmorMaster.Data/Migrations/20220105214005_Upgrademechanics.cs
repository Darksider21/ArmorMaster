using Microsoft.EntityFrameworkCore.Migrations;

namespace ArmorMaster.Data.Migrations
{
    public partial class Upgrademechanics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemUpgradeLevel",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemUpgradeLevel",
                table: "Items");
        }
    }
}
