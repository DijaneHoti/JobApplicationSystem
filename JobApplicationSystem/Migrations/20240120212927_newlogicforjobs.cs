using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationSystem.Migrations
{
    /// <inheritdoc />
    public partial class newlogicforjobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobseekers_JobPostings_JobPostingID",
                table: "Jobseekers");

            migrationBuilder.DropIndex(
                name: "IX_Jobseekers_JobPostingID",
                table: "Jobseekers");

            migrationBuilder.DropColumn(
                name: "JobPostingID",
                table: "Jobseekers");

            migrationBuilder.CreateTable(
                name: "JobPostSeekers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerID = table.Column<int>(type: "int", nullable: false),
                    JobPostingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostSeekers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPostSeekers_JobPostings_JobPostingID",
                        column: x => x.JobPostingID,
                        principalTable: "JobPostings",
                        principalColumn: "JobPostingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPostSeekers_Jobseekers_JobSeekerID",
                        column: x => x.JobSeekerID,
                        principalTable: "Jobseekers",
                        principalColumn: "JobseekerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostSeekers_JobPostingID",
                table: "JobPostSeekers",
                column: "JobPostingID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostSeekers_JobSeekerID",
                table: "JobPostSeekers",
                column: "JobSeekerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPostSeekers");

            migrationBuilder.AddColumn<int>(
                name: "JobPostingID",
                table: "Jobseekers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_JobPostingID",
                table: "Jobseekers",
                column: "JobPostingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobseekers_JobPostings_JobPostingID",
                table: "Jobseekers",
                column: "JobPostingID",
                principalTable: "JobPostings",
                principalColumn: "JobPostingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
