using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.iTechQuiz.Repositories.Migrations
{
    public partial class passsurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Files_FileId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersPassSurveys",
                table: "UsersPassSurveys");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_FileId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Answers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsersPassSurveys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersPassSurveys",
                table: "UsersPassSurveys",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPassSurveys_UserId_SurveyId",
                table: "UsersPassSurveys",
                columns: new[] { "UserId", "SurveyId" },
                unique: true,
                filter: "[UserId] != '00000000-0000-0000-0000-000000000000'");

            migrationBuilder.CreateIndex(
                name: "IX_Files_AnswerId",
                table: "Files",
                column: "AnswerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Answers_AnswerId",
                table: "Files",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Answers_AnswerId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersPassSurveys",
                table: "UsersPassSurveys");

            migrationBuilder.DropIndex(
                name: "IX_UsersPassSurveys_UserId_SurveyId",
                table: "UsersPassSurveys");

            migrationBuilder.DropIndex(
                name: "IX_Files_AnswerId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersPassSurveys");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "SurveyId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "Answers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersPassSurveys",
                table: "UsersPassSurveys",
                columns: new[] { "UserId", "SurveyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_FileId",
                table: "Answers",
                column: "FileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Files_FileId",
                table: "Answers",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
