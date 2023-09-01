using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class removeLogoValueFromBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Brand");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "e09c3ea0-6f88-4b15-a7ac-be4f3adf9936");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Brand",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "bf7b275d-c7d6-4c62-b32a-1d53b92db072");
        }
    }
}
