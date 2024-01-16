using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationSystem.Migrations
{
    /// <inheritdoc />
    public partial class Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Jobseekers_JobseekerID",
                table: "Employers");

            migrationBuilder.RenameColumn(
                name: "JobseekerID",
                table: "Employers",
                newName: "CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_JobseekerID",
                table: "Employers",
                newName: "IX_Employers_CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Companies_CompanyID",
                table: "Employers",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Companies_CompanyID",
                table: "Employers");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Employers",
                newName: "JobseekerID");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_CompanyID",
                table: "Employers",
                newName: "IX_Employers_JobseekerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Jobseekers_JobseekerID",
                table: "Employers",
                column: "JobseekerID",
                principalTable: "Jobseekers",
                principalColumn: "JobseekerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
