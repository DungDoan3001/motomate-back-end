using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class updatecart : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_table_cart_FK_user_id",
				table: "table_cart");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "cdc4ccee-6838-4be0-8475-b135eb5d1dbf");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "abc5920b-4336-4872-8a31-ddfc632544bf");

			migrationBuilder.CreateIndex(
				name: "IX_table_cart_FK_user_id",
				table: "table_cart",
				column: "FK_user_id",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_table_cart_FK_user_id",
				table: "table_cart");

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

			migrationBuilder.CreateIndex(
				name: "IX_table_cart_FK_user_id",
				table: "table_cart",
				column: "FK_user_id");
		}
	}
}
