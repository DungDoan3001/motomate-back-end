using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class updatemroefieldsforvehiclereview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FK_trip_request_id",
                table: "table_vehicle_review",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FK_user_id",
                table: "table_vehicle_review",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "8170e8f6-132c-4b99-bf05-b880978b2d76");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "4ea9cfa0-05d0-49a7-aa57-7e213bf54b00");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "1137c1d1-3b8e-416d-a45f-85e1b46f00af");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "a2d701e2-525e-4e86-a072-0be2cb3d3d93");

            migrationBuilder.CreateIndex(
                name: "IX_table_vehicle_review_FK_trip_request_id",
                table: "table_vehicle_review",
                column: "FK_trip_request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_table_vehicle_review_FK_user_id",
                table: "table_vehicle_review",
                column: "FK_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_table_vehicle_review_table_trip_request_FK_trip_request_id",
                table: "table_vehicle_review",
                column: "FK_trip_request_id",
                principalTable: "table_trip_request",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_vehicle_review_table_users_FK_user_id",
                table: "table_vehicle_review",
                column: "FK_user_id",
                principalTable: "table_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_vehicle_review_table_trip_request_FK_trip_request_id",
                table: "table_vehicle_review");

            migrationBuilder.DropForeignKey(
                name: "FK_table_vehicle_review_table_users_FK_user_id",
                table: "table_vehicle_review");

            migrationBuilder.DropIndex(
                name: "IX_table_vehicle_review_FK_trip_request_id",
                table: "table_vehicle_review");

            migrationBuilder.DropIndex(
                name: "IX_table_vehicle_review_FK_user_id",
                table: "table_vehicle_review");

            migrationBuilder.DropColumn(
                name: "FK_trip_request_id",
                table: "table_vehicle_review");

            migrationBuilder.DropColumn(
                name: "FK_user_id",
                table: "table_vehicle_review");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "63064f3d-8f6e-4d0b-a571-b9dc88bc9a29");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "58553b3e-0c05-4a69-91df-5aa28a0ba23d");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "5b80bd5b-e4c1-41b2-8470-8d80ae10a746");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "537a4f16-3c42-4705-aabf-2b5bb20f2ff1");
        }
    }
}
