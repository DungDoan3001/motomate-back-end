using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class addbiofields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bio",
                table: "table_users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "a91b81d8-7809-49ca-a6ca-77f1c6058b5f");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "5685bc11-268c-4205-a03e-df68b4bbcbac");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "335efc31-4104-40f3-975c-58f697706346");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "97ef0aab-8e46-4caf-b1f1-ba7615156f84");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bio",
                table: "table_users");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "415e0fc9-fc5a-4971-8c28-624628ea4524");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "e5478f34-777f-4ef1-8910-8c9dc287c215");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "a542151e-589b-4c73-b66b-8154d9edc002");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "4c492075-7dc0-413d-b796-9e9e675394a5");
        }
    }
}
