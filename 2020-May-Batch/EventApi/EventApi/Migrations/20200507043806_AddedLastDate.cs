using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventApi.Migrations
{
    public partial class AddedLastDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "reg_url",
                table: "events",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_reg_date",
                table: "events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_reg_date",
                table: "events");

            migrationBuilder.AlterColumn<string>(
                name: "reg_url",
                table: "events",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250);
        }
    }
}
