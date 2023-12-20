using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class something : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "5065ee02-f7eb-4fd9-8290-c23b64286d4a");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "786fbfd9-fbf9-4056-a037-7ec87c528c7c");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "ceaf3739-2f2a-4d73-bfdd-051b80c31a7f");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "5468eced-f76e-47d1-8314-f9b1d4d08eac");
		}
	}
}
