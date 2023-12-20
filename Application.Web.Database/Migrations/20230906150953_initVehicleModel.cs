using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class initVehicleModel : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Vehicle",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_owner_id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_model_id = table.Column<Guid>(type: "uuid", nullable: false),
					purchase_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					condition_percentage = table.Column<int>(type: "integer", nullable: false),
					license_plate = table.Column<string>(type: "text", nullable: true),
					insurance_number = table.Column<string>(type: "text", nullable: true),
					insurance_expiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					status = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Vehicle", x => x.id);
					table.ForeignKey(
						name: "FK_Vehicle_Model_FK_model_id",
						column: x => x.FK_model_id,
						principalTable: "Model",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Vehicle_Users_FK_owner_id",
						column: x => x.FK_owner_id,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "VehicleImage",
				columns: table => new
				{
					VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
					ImageId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_VehicleImage", x => new { x.VehicleId, x.ImageId });
					table.ForeignKey(
						name: "FK_VehicleImage_Image_ImageId",
						column: x => x.ImageId,
						principalTable: "Image",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_VehicleImage_Vehicle_VehicleId",
						column: x => x.VehicleId,
						principalTable: "Vehicle",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "8f05eaa7-d287-47d8-a975-f605978efca6");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "faa5b583-84d0-4a9d-b38f-c2ffaebb800f");

			migrationBuilder.CreateIndex(
				name: "IX_Vehicle_FK_model_id",
				table: "Vehicle",
				column: "FK_model_id");

			migrationBuilder.CreateIndex(
				name: "IX_Vehicle_FK_owner_id",
				table: "Vehicle",
				column: "FK_owner_id");

			migrationBuilder.CreateIndex(
				name: "IX_VehicleImage_ImageId",
				table: "VehicleImage",
				column: "ImageId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "VehicleImage");

			migrationBuilder.DropTable(
				name: "Vehicle");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "429fe43b-6758-4a1f-9612-517fbb4a3e8c");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "8cdff5cc-79bf-41dd-a2bb-591000c7fbc5");
		}
	}
}
