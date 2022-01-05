using Microsoft.EntityFrameworkCore.Migrations;

namespace ArmorMaster.Data.Migrations
{
    public partial class ItemstatChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemStatID",
                table: "ItemStats",
                newName: "ItemBonusStatID");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Items",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "Potential",
                table: "Items",
                newName: "ItemPotential");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Items",
                newName: "ItemLevel");

            migrationBuilder.AddColumn<double>(
                name: "BaseStatQuantity",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "BaseStatType",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseStatQuantity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BaseStatType",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemBonusStatID",
                table: "ItemStats",
                newName: "ItemStatID");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "Items",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ItemPotential",
                table: "Items",
                newName: "Potential");

            migrationBuilder.RenameColumn(
                name: "ItemLevel",
                table: "Items",
                newName: "Level");
        }
    }
}
