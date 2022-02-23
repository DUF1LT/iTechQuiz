using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.iTechQuiz.Repositories.Migrations
{
    public partial class surveylistforuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Surveys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Surveys");
        }
    }
}
