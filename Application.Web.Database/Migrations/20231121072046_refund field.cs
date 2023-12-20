using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class refundfield : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "refund_id",
				table: "table_in_complete_trip",
				type: "text",
				nullable: true);

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
				column: "ConcurrencyStamp",
				value: "694a090e-7ee6-4086-9a4f-9a6b4df53a6b");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
				column: "ConcurrencyStamp",
				value: "b095434f-279c-457c-b18b-f7a60424baec");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "d8570d82-8c0f-4dd4-a3de-e1615f0b0bff");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "1db4271a-8bb9-4810-ac89-5b3893159710");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "refund_id",
				table: "table_in_complete_trip");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
				column: "ConcurrencyStamp",
				value: "966d4cb8-e207-447a-8b43-914e12bf3b09");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
				column: "ConcurrencyStamp",
				value: "32c4523c-2745-47c5-9312-5e25000b637c");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "594c88b2-e17c-43bb-8b0f-641aefc3bed3");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "fd2213e4-28cc-473c-ad29-870bd7c924fb");
		}
	}
}
