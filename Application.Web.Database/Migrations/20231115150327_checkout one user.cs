using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class checkoutoneuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_table_checkout_order_FK_user_id",
                table: "table_checkout_order");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "8a10fd6f-f874-45e4-b4a9-03d7827a8482");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "cf86d794-a5ac-4e73-8b7c-8dbcd7484ab3");

            migrationBuilder.CreateIndex(
                name: "IX_table_checkout_order_FK_user_id",
                table: "table_checkout_order",
                column: "FK_user_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_table_checkout_order_FK_user_id",
                table: "table_checkout_order");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "9e6be09d-94fa-4743-ae63-2c3efce5dfc4");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "da70c2a2-568e-4ce5-b900-c382634adc48");

            migrationBuilder.CreateIndex(
                name: "IX_table_checkout_order_FK_user_id",
                table: "table_checkout_order",
                column: "FK_user_id");
        }
    }
}
