using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.iTechQuiz.Repositories.Migrations
{
    public partial class addpagemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurveyPage",
                table: "Questions");

            migrationBuilder.AddColumn<Guid>(
                name: "SurveyPageId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyPageId",
                table: "Questions",
                column: "SurveyPageId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_SurveyId",
                table: "Pages",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Pages_SurveyPageId",
                table: "Questions",
                column: "SurveyPageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Pages_SurveyPageId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyPageId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SurveyPageId",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "SurveyPage",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
