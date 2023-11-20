using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class addnewtworoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "7618488b-9d39-4ce7-a5ba-75711af57eb3");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "2f12cf13-179f-4b0e-9c3b-0ed3ee8d99f0");

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("19021360-14c6-49ab-a91b-79c388115f4e"), "afe8b657-2649-460c-969b-3c08720b8c3a", "Staff", "STAFF" },
                    { new Guid("20308981-486a-456e-833c-1a8821121b3e"), "76e7b226-5b67-40e3-a632-ce9ada0b73fc", "Lessor", "LESSOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"));

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "bae0319d-a288-40de-9ec0-42c8c1d400eb");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "ee747352-e178-4b5f-916a-61e6fa652eb3");
        }
    }
}
