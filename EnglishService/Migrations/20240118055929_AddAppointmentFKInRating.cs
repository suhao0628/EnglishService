using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishService.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentFKInRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Ratings_RatingId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RatingId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AppointmentId",
                table: "Ratings",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Ratings_RatingId",
                table: "Appointments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Appointments_AppointmentId",
                table: "Ratings",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Ratings_RatingId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Appointments_AppointmentId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_AppointmentId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Ratings");

            migrationBuilder.AlterColumn<int>(
                name: "RatingId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Ratings_RatingId",
                table: "Appointments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
