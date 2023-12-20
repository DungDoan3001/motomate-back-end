using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class updatecheckout : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "client_secret",
				table: "table_cart");

			migrationBuilder.DropColumn(
				name: "payment_intent_id",
				table: "table_cart");

			migrationBuilder.CreateTable(
				name: "table_checkout_order",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_user_id = table.Column<Guid>(type: "uuid", nullable: false),
					payment_intent_id = table.Column<string>(type: "text", nullable: true),
					client_secret = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_checkout_order", x => x.id);
					table.ForeignKey(
						name: "FK_table_checkout_order_table_users_FK_user_id",
						column: x => x.FK_user_id,
						principalTable: "table_users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "table_checkout_order_vehicle",
				columns: table => new
				{
					PK_FK_checkout_id = table.Column<Guid>(type: "uuid", nullable: false),
					PK_FK_vehicle_id = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_checkout_order_vehicle", x => new { x.PK_FK_checkout_id, x.PK_FK_vehicle_id });
					table.ForeignKey(
						name: "FK_table_checkout_order_vehicle_table_checkout_order_PK_FK_che~",
						column: x => x.PK_FK_checkout_id,
						principalTable: "table_checkout_order",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_table_checkout_order_vehicle_table_vehicle_PK_FK_vehicle_id",
						column: x => x.PK_FK_vehicle_id,
						principalTable: "table_vehicle",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

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

			migrationBuilder.CreateIndex(
				name: "IX_table_checkout_order_vehicle_PK_FK_vehicle_id",
				table: "table_checkout_order_vehicle",
				column: "PK_FK_vehicle_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "table_checkout_order_vehicle");

			migrationBuilder.DropTable(
				name: "table_checkout_order");

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
		}
	}
}
