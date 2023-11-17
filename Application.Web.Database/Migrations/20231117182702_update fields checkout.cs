using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class updatefieldscheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "drop_off_location",
                table: "table_checkout_order");

            migrationBuilder.DropColumn(
                name: "pick_up_location",
                table: "table_checkout_order");

            migrationBuilder.AddColumn<decimal>(
                name: "ammount",
                table: "table_trip_request",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "drop_off_location",
                table: "table_checkout_order_vehicle",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pick_up_location",
                table: "table_checkout_order_vehicle",
                type: "text",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ammount",
                table: "table_trip_request");

            migrationBuilder.DropColumn(
                name: "drop_off_location",
                table: "table_checkout_order_vehicle");

            migrationBuilder.DropColumn(
                name: "pick_up_location",
                table: "table_checkout_order_vehicle");

            migrationBuilder.AddColumn<string>(
                name: "drop_off_location",
                table: "table_checkout_order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pick_up_location",
                table: "table_checkout_order",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "f2a749bd-623b-4edd-8dd0-29b4f4c3f55a");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "9982bf93-95a8-4204-bca6-f3839a109b49");
        }
    }
}
