using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateedAt",
                table: "Applications",
                newName: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_VacancyId",
                table: "Applications",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Vacancies_VacancyId",
                table: "Applications",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_UserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Vacancies_VacancyId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_UserId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_VacancyId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Applications",
                newName: "CreateedAt");
        }
    }
}
