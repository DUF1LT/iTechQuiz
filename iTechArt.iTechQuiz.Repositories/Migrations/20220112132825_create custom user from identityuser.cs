using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.iTechQuiz.Repositories.Migrations
{
    public partial class createcustomuserfromidentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystemUser",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SurveysAmount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SurveysAmount",
                table: "Users");
        }
    }
}
