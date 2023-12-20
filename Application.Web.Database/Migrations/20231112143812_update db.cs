using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class updatedb : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "client_secret",
				table: "table_cart",
				type: "text",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "payment_intent_id",
				table: "table_cart",
				type: "text",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "table_trip_request",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_lessee_id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_lessor_id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
					status = table.Column<bool>(type: "boolean", nullable: false),
					pick_up_location = table.Column<string>(type: "text", nullable: true),
					drop_off_location = table.Column<string>(type: "text", nullable: true),
					payment_intent_id = table.Column<string>(type: "text", nullable: true),
					created_at = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_trip_request", x => x.id);
					table.ForeignKey(
						name: "FK_table_trip_request_table_users_FK_lessee_id",
						column: x => x.FK_lessee_id,
						principalTable: "table_users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_table_trip_request_table_users_FK_lessor_id",
						column: x => x.FK_lessor_id,
						principalTable: "table_users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_table_trip_request_table_vehicle_FK_vehicle_id",
						column: x => x.FK_vehicle_id,
						principalTable: "table_vehicle",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "8d17f495-316b-4e8c-93c5-a127fff5100b");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "96909c82-4deb-43a1-be5b-f3f6c0ad4b96");

			migrationBuilder.CreateIndex(
				name: "IX_table_trip_request_FK_lessee_id",
				table: "table_trip_request",
				column: "FK_lessee_id");

			migrationBuilder.CreateIndex(
				name: "IX_table_trip_request_FK_lessor_id",
				table: "table_trip_request",
				column: "FK_lessor_id");

			migrationBuilder.CreateIndex(
				name: "IX_table_trip_request_FK_vehicle_id",
				table: "table_trip_request",
				column: "FK_vehicle_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "table_trip_request");

			migrationBuilder.DropColumn(
				name: "client_secret",
				table: "table_cart");

			migrationBuilder.DropColumn(
				name: "payment_intent_id",
				table: "table_cart");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "291c51cf-82f9-455d-9e44-39b0dab51ac4");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "c78878ce-562d-4e28-be8a-36276283ae62");
		}
	}
}
