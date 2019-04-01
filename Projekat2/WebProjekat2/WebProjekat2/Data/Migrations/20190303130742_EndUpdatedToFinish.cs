using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjekat2.Data.Migrations
{
    public partial class EndUpdatedToFinish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "End",
                table: "Trainings",
                newName: "Finish");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Finish",
                table: "Trainings",
                newName: "End");
        }
    }
}
