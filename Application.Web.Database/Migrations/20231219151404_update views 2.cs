using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class updateviews2 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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

			migrationBuilder.CreateIndex(
				name: "IX_table_view_created_at",
				table: "table_view",
				column: "created_at");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_table_view_created_at",
				table: "table_view");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
				column: "ConcurrencyStamp",
				value: "bd7629e8-bc18-4332-986e-ecdd8308a89c");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
				column: "ConcurrencyStamp",
				value: "d36de90c-5114-4b31-89fc-50b6daa240b9");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "82c816b7-72a3-4a6f-9bc8-8db2a2939eaa");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "8bdd6674-c037-48fc-8b68-fc2776938180");
		}
	}
}
