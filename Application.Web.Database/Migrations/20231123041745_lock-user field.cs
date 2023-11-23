using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class lockuserfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_locked",
                table: "table_users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "6786e95b-6963-4265-801e-3695719ace84");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "d81b1e51-3514-4c91-80bc-91c7643f2162");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "810f12c5-7a6e-4477-aa10-c8bec6d4cbd9");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "3bc3a4d9-e6cf-46d9-976c-5c830ce972eb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_locked",
                table: "table_users");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "694a090e-7ee6-4086-9a4f-9a6b4df53a6b");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "b095434f-279c-457c-b18b-f7a60424baec");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "d8570d82-8c0f-4dd4-a3de-e1615f0b0bff");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "1db4271a-8bb9-4810-ac89-5b3893159710");
        }
    }
}
