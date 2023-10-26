using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class linkroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "b5931c34-ba57-4ca9-a512-2a061973e22d");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "e0b94f40-5d6b-4625-9b4f-ff0051e7563e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "5065ee02-f7eb-4fd9-8290-c23b64286d4a");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "786fbfd9-fbf9-4056-a037-7ec87c528c7c");
        }
    }
}
