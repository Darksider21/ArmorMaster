using Microsoft.EntityFrameworkCore.Migrations;

namespace ArmorMaster.Data.Migrations
{
    public partial class NewStatTypeSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemStats_ItemStatTypes_ItemStatTypeId",
                table: "ItemStats");

            migrationBuilder.DropTable(
                name: "ItemStatTypes");

            migrationBuilder.DropIndex(
                name: "IX_ItemStats_ItemStatTypeId",
                table: "ItemStats");

            migrationBuilder.DropColumn(
                name: "ItemStatTypeId",
                table: "ItemStats");

            migrationBuilder.AddColumn<string>(
                name: "StatType",
                table: "ItemStats",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatType",
                table: "ItemStats");

            migrationBuilder.AddColumn<int>(
                name: "ItemStatTypeId",
                table: "ItemStats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemStatTypes",
                columns: table => new
                {
                    ItemStatTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatTypes", x => x.ItemStatTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemStats_ItemStatTypeId",
                table: "ItemStats",
                column: "ItemStatTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStats_ItemStatTypes_ItemStatTypeId",
                table: "ItemStats",
                column: "ItemStatTypeId",
                principalTable: "ItemStatTypes",
                principalColumn: "ItemStatTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
