using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class updateforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_cart_vehicle_table_cart_PK_FK_vehicle_id",
                table: "table_cart_vehicle");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "c0ae3d56-863e-4afc-9d18-0e8d1f99d4a0");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "41fc447b-86fd-424d-a38b-63b47c3e1665");

            migrationBuilder.AddForeignKey(
                name: "FK_table_cart_vehicle_table_cart_PK_FK_cart_id",
                table: "table_cart_vehicle",
                column: "PK_FK_cart_id",
                principalTable: "table_cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_cart_vehicle_table_cart_PK_FK_cart_id",
                table: "table_cart_vehicle");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "d098d7d9-5fdd-4e88-9c6c-5b14a3206ab3");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "8405f480-aa88-4155-9e8f-c08f5fb3b0c8");

            migrationBuilder.AddForeignKey(
                name: "FK_table_cart_vehicle_table_cart_PK_FK_vehicle_id",
                table: "table_cart_vehicle",
                column: "PK_FK_vehicle_id",
                principalTable: "table_cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
