using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishService.Migrations
{
    /// <inheritdoc />
    public partial class AddRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "Professionals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_RegionId",
                table: "Professionals",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RegionId",
                table: "Customers",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Region_RegionId",
                table: "Customers",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_Region_RegionId",
                table: "Professionals",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Region_RegionId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_Region_RegionId",
                table: "Professionals");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropIndex(
                name: "IX_Professionals_RegionId",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RegionId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Customers");
        }
    }
}
