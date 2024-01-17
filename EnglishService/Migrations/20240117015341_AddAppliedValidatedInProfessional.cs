using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishService.Migrations
{
    /// <inheritdoc />
    public partial class AddAppliedValidatedInProfessional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasApplied",
                table: "Professionals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "Professionals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasApplied",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "Professionals");
        }
    }
}
