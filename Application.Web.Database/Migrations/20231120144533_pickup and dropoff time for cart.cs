using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class pickupanddropofftimeforcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "drop_off_date_time",
                table: "table_cart_vehicle",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "pick_up_date_time",
                table: "table_cart_vehicle",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "975c2c3e-4e5d-4a44-8308-30f828b841d7");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "e372ad6e-7dc3-48fe-9b4c-8a27ac6b3ab0");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "3ddab62b-773b-4538-9e46-fa824d1ba1fb");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "80e5b9b9-c590-40e1-8351-fad7c94f54f3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "drop_off_date_time",
                table: "table_cart_vehicle");

            migrationBuilder.DropColumn(
                name: "pick_up_date_time",
                table: "table_cart_vehicle");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "afe8b657-2649-460c-969b-3c08720b8c3a");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "76e7b226-5b67-40e3-a632-ce9ada0b73fc");

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
        }
    }
}
