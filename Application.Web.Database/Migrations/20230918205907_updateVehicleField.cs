using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class updateVehicleField : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_VehicleImage_Image_ImageId",
				table: "VehicleImage");

			migrationBuilder.DropForeignKey(
				name: "FK_VehicleImage_Vehicle_VehicleId",
				table: "VehicleImage");

			migrationBuilder.RenameColumn(
				name: "ImageId",
				table: "VehicleImage",
				newName: "PK_FK_image_id");

			migrationBuilder.RenameColumn(
				name: "VehicleId",
				table: "VehicleImage",
				newName: "PK_FK_vehicle_id");

			migrationBuilder.RenameIndex(
				name: "IX_VehicleImage_ImageId",
				table: "VehicleImage",
				newName: "IX_VehicleImage_PK_FK_image_id");

			migrationBuilder.AlterColumn<string>(
				name: "year",
				table: "Model",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "integer");

			migrationBuilder.AlterColumn<string>(
				name: "capacity",
				table: "Model",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "integer");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "3a788471-0108-43bb-aa6b-1e18728c5a1b");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "f3d0099d-38a4-4668-8002-b15468bbab07");

			migrationBuilder.AddForeignKey(
				name: "FK_VehicleImage_Image_PK_FK_image_id",
				table: "VehicleImage",
				column: "PK_FK_image_id",
				principalTable: "Image",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_VehicleImage_Vehicle_PK_FK_vehicle_id",
				table: "VehicleImage",
				column: "PK_FK_vehicle_id",
				principalTable: "Vehicle",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_VehicleImage_Image_PK_FK_image_id",
				table: "VehicleImage");

			migrationBuilder.DropForeignKey(
				name: "FK_VehicleImage_Vehicle_PK_FK_vehicle_id",
				table: "VehicleImage");

			migrationBuilder.RenameColumn(
				name: "PK_FK_image_id",
				table: "VehicleImage",
				newName: "ImageId");

			migrationBuilder.RenameColumn(
				name: "PK_FK_vehicle_id",
				table: "VehicleImage",
				newName: "VehicleId");

			migrationBuilder.RenameIndex(
				name: "IX_VehicleImage_PK_FK_image_id",
				table: "VehicleImage",
				newName: "IX_VehicleImage_ImageId");

			migrationBuilder.AlterColumn<int>(
				name: "year",
				table: "Model",
				type: "integer",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(string),
				oldType: "text",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "capacity",
				table: "Model",
				type: "integer",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(string),
				oldType: "text",
				oldNullable: true);

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "eaa8bc0f-869f-401f-a621-c70958ada8ef");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "c912d085-0501-459e-8dff-ddef3bb7d19a");

			migrationBuilder.AddForeignKey(
				name: "FK_VehicleImage_Image_ImageId",
				table: "VehicleImage",
				column: "ImageId",
				principalTable: "Image",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_VehicleImage_Vehicle_VehicleId",
				table: "VehicleImage",
				column: "VehicleId",
				principalTable: "Vehicle",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
