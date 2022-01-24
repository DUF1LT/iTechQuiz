using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.iTechQuiz.Repositories.Migrations
{
    public partial class addquestionTypelookuptablerenamefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Users_FounderId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "PassDate",
                table: "UsersPassSurveys",
                newName: "PassedAt");

            migrationBuilder.RenameColumn(
                name: "FounderId",
                table: "Surveys",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_FounderId",
                table: "Surveys",
                newName: "IX_Surveys_CreatedById");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TypeId",
                table: "Questions",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionTypes_TypeId",
                table: "Questions",
                column: "TypeId",
                principalTable: "QuestionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_CreatedById",
                table: "Surveys",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionTypes_TypeId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Users_CreatedById",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TypeId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "PassedAt",
                table: "UsersPassSurveys",
                newName: "PassDate");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Surveys",
                newName: "FounderId");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_CreatedById",
                table: "Surveys",
                newName: "IX_Surveys_FounderId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_FounderId",
                table: "Surveys",
                column: "FounderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
