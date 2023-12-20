using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class initcart : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "table_cart",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_user_id = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_cart", x => x.id);
					table.ForeignKey(
						name: "FK_table_cart_table_users_FK_user_id",
						column: x => x.FK_user_id,
						principalTable: "table_users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "table_cart_vehicle",
				columns: table => new
				{
					PK_FK_cart_id = table.Column<Guid>(type: "uuid", nullable: false),
					PK_FK_vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
					quantity = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_cart_vehicle", x => new { x.PK_FK_cart_id, x.PK_FK_vehicle_id });
					table.ForeignKey(
						name: "FK_table_cart_vehicle_table_cart_PK_FK_vehicle_id",
						column: x => x.PK_FK_vehicle_id,
						principalTable: "table_cart",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_table_cart_vehicle_table_vehicle_PK_FK_vehicle_id",
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
				value: "1c8b76f1-cbc8-4230-994d-bb95aeccbf17");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "fe15b201-26ea-4fbb-881f-2e9228ee3384");

			migrationBuilder.CreateIndex(
				name: "IX_table_cart_FK_user_id",
				table: "table_cart",
				column: "FK_user_id");

			migrationBuilder.CreateIndex(
				name: "IX_table_cart_vehicle_PK_FK_vehicle_id",
				table: "table_cart_vehicle",
				column: "PK_FK_vehicle_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "table_cart_vehicle");

			migrationBuilder.DropTable(
				name: "table_cart");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "47ad36fd-dbb1-4ff4-9a71-9cbb23a6bc7e");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "8cf594ae-2bdc-4d4a-9f4d-8d548c47aaef");
		}
	}
}
