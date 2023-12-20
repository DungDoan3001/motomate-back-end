using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class addviewtable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "table_view",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					continent = table.Column<string>(type: "text", nullable: true),
					continent_geo_name_id = table.Column<string>(type: "text", nullable: true),
					country = table.Column<string>(type: "text", nullable: true),
					country_code = table.Column<string>(type: "text", nullable: true),
					country_geo_name_id = table.Column<string>(type: "text", nullable: true),
					ip_address = table.Column<string>(type: "text", nullable: true),
					latitude = table.Column<string>(type: "text", nullable: true),
					longtitude = table.Column<string>(type: "text", nullable: true),
					region = table.Column<string>(type: "text", nullable: true),
					region_geo_name_id = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_view", x => x.id);
				});

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
				column: "ConcurrencyStamp",
				value: "bda1520b-a927-424c-b568-c3003f209c2f");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
				column: "ConcurrencyStamp",
				value: "d89d7e5d-b635-43ff-a5a6-e99362f94f11");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "0fc2a328-601f-435e-b133-54ce14d347f2");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "ccb816e4-f9ca-4334-b332-32fb53c515c0");

			migrationBuilder.CreateIndex(
				name: "IX_table_view_ip_address",
				table: "table_view",
				column: "ip_address");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "table_view");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
				column: "ConcurrencyStamp",
				value: "a91b81d8-7809-49ca-a6ca-77f1c6058b5f");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
				column: "ConcurrencyStamp",
				value: "5685bc11-268c-4205-a03e-df68b4bbcbac");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "335efc31-4104-40f3-975c-58f697706346");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "97ef0aab-8e46-4caf-b1f1-ba7615156f84");
		}
	}
}
