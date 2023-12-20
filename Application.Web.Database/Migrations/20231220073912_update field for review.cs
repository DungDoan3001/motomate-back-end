using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class updatefieldforreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_reviewed",
                table: "table_trip_request",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "50412763-247d-4d71-8085-64b2818cfd33");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "57ebeb5b-0eba-4492-9b85-de8840feef66");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "fdf3eeda-8dfd-4317-aa41-aa7b4ac040ba");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "b0fe4bf6-bd00-44ba-88bc-0dac71417f41");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_reviewed",
                table: "table_trip_request");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "242f8ae2-4bc8-45d9-bc95-5bfdf22946bc");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "b57fb636-d8fa-4487-8ef2-87a3921a4f21");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "1f141839-b00d-4cbb-9b22-3126fa060960");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "06341368-8bd1-4cfd-919c-799f73127683");
        }
    }
}
