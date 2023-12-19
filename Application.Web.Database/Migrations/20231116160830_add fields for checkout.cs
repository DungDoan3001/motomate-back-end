using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class addfieldsforcheckout : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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
				value: "ec1b6745-ddaf-4ce5-94c9-d4ac046e4f2f");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "9675d641-6800-42ca-b419-b2b376378565");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "drop_off_location",
				table: "table_checkout_order");

			migrationBuilder.DropColumn(
				name: "pick_up_location",
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
		}
	}
}
