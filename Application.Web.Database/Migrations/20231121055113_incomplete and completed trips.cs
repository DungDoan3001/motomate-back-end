using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class incompleteandcompletedtrips : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "table_completed_trip",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_trip_id = table.Column<Guid>(type: "uuid", nullable: false),
					duration = table.Column<TimeSpan>(type: "interval", nullable: false),
					ammount = table.Column<decimal>(type: "numeric", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_completed_trip", x => x.id);
					table.ForeignKey(
						name: "FK_table_completed_trip_table_trip_request_FK_trip_id",
						column: x => x.FK_trip_id,
						principalTable: "table_trip_request",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "table_in_complete_trip",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_trip_id = table.Column<Guid>(type: "uuid", nullable: false),
					reason = table.Column<string>(type: "text", nullable: true),
					cancel_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_in_complete_trip", x => x.id);
					table.ForeignKey(
						name: "FK_table_in_complete_trip_table_trip_request_FK_trip_id",
						column: x => x.FK_trip_id,
						principalTable: "table_trip_request",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

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

			migrationBuilder.CreateIndex(
				name: "IX_table_completed_trip_FK_trip_id",
				table: "table_completed_trip",
				column: "FK_trip_id",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_table_in_complete_trip_FK_trip_id",
				table: "table_in_complete_trip",
				column: "FK_trip_id",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "table_completed_trip");

			migrationBuilder.DropTable(
				name: "table_in_complete_trip");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
				column: "ConcurrencyStamp",
				value: "975c2c3e-4e5d-4a44-8308-30f828b841d7");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
				column: "ConcurrencyStamp",
				value: "e372ad6e-7dc3-48fe-9b4c-8a27ac6b3ab0");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "3ddab62b-773b-4538-9e46-fa824d1ba1fb");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "80e5b9b9-c590-40e1-8351-fad7c94f54f3");
		}
	}
}
