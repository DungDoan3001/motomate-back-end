using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class updatemorefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "drop_off_date_time",
                table: "table_trip_request",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "parent_order_id",
                table: "table_trip_request",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "pick_up_date_time",
                table: "table_trip_request",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "drop_off_date_time",
                table: "table_checkout_order_vehicle",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "pick_up_date_time",
                table: "table_checkout_order_vehicle",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "drop_off_date_time",
                table: "table_trip_request");

            migrationBuilder.DropColumn(
                name: "parent_order_id",
                table: "table_trip_request");

            migrationBuilder.DropColumn(
                name: "pick_up_date_time",
                table: "table_trip_request");

            migrationBuilder.DropColumn(
                name: "drop_off_date_time",
                table: "table_checkout_order_vehicle");

            migrationBuilder.DropColumn(
                name: "pick_up_date_time",
                table: "table_checkout_order_vehicle");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "caca28b5-0737-46e3-9890-7a1042b15497");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "bb6226dd-9889-4514-a5cd-d8f379e697fd");
        }
    }
}
