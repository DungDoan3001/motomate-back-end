using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class updatecontext2 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Models_table_collection_FK_collection_id",
				table: "Models");

			migrationBuilder.DropForeignKey(
				name: "FK_table_brand_image_Images_PK_FK_image_id",
				table: "table_brand_image");

			migrationBuilder.DropForeignKey(
				name: "FK_table_model_color_Colors_PK_FK_color_id",
				table: "table_model_color");

			migrationBuilder.DropForeignKey(
				name: "FK_table_model_color_Models_PK_FK_model_id",
				table: "table_model_color");

			migrationBuilder.DropForeignKey(
				name: "FK_table_vehicle_Colors_FK_color_id",
				table: "table_vehicle");

			migrationBuilder.DropForeignKey(
				name: "FK_table_vehicle_Models_FK_model_id",
				table: "table_vehicle");

			migrationBuilder.DropForeignKey(
				name: "FK_table_vehicle_image_Images_PK_FK_image_id",
				table: "table_vehicle_image");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Models",
				table: "Models");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Images",
				table: "Images");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Colors",
				table: "Colors");

			migrationBuilder.RenameTable(
				name: "Models",
				newName: "table_model");

			migrationBuilder.RenameTable(
				name: "Images",
				newName: "table_image");

			migrationBuilder.RenameTable(
				name: "Colors",
				newName: "table_color");

			migrationBuilder.RenameIndex(
				name: "IX_Models_FK_collection_id",
				table: "table_model",
				newName: "IX_table_model_FK_collection_id");

			migrationBuilder.AddColumn<string>(
				name: "hex_code",
				table: "table_color",
				type: "text",
				nullable: true);

			migrationBuilder.AddPrimaryKey(
				name: "PK_table_model",
				table: "table_model",
				column: "id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_table_image",
				table: "table_image",
				column: "id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_table_color",
				table: "table_color",
				column: "id");

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

			migrationBuilder.AddForeignKey(
				name: "FK_table_brand_image_table_image_PK_FK_image_id",
				table: "table_brand_image",
				column: "PK_FK_image_id",
				principalTable: "table_image",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_model_table_collection_FK_collection_id",
				table: "table_model",
				column: "FK_collection_id",
				principalTable: "table_collection",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_model_color_table_color_PK_FK_color_id",
				table: "table_model_color",
				column: "PK_FK_color_id",
				principalTable: "table_color",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_model_color_table_model_PK_FK_model_id",
				table: "table_model_color",
				column: "PK_FK_model_id",
				principalTable: "table_model",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_vehicle_table_color_FK_color_id",
				table: "table_vehicle",
				column: "FK_color_id",
				principalTable: "table_color",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_vehicle_table_model_FK_model_id",
				table: "table_vehicle",
				column: "FK_model_id",
				principalTable: "table_model",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_vehicle_image_table_image_PK_FK_image_id",
				table: "table_vehicle_image",
				column: "PK_FK_image_id",
				principalTable: "table_image",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_table_brand_image_table_image_PK_FK_image_id",
				table: "table_brand_image");

			migrationBuilder.DropForeignKey(
				name: "FK_table_model_table_collection_FK_collection_id",
				table: "table_model");

			migrationBuilder.DropForeignKey(
				name: "FK_table_model_color_table_color_PK_FK_color_id",
				table: "table_model_color");

			migrationBuilder.DropForeignKey(
				name: "FK_table_model_color_table_model_PK_FK_model_id",
				table: "table_model_color");

			migrationBuilder.DropForeignKey(
				name: "FK_table_vehicle_table_color_FK_color_id",
				table: "table_vehicle");

			migrationBuilder.DropForeignKey(
				name: "FK_table_vehicle_table_model_FK_model_id",
				table: "table_vehicle");

			migrationBuilder.DropForeignKey(
				name: "FK_table_vehicle_image_table_image_PK_FK_image_id",
				table: "table_vehicle_image");

			migrationBuilder.DropPrimaryKey(
				name: "PK_table_model",
				table: "table_model");

			migrationBuilder.DropPrimaryKey(
				name: "PK_table_image",
				table: "table_image");

			migrationBuilder.DropPrimaryKey(
				name: "PK_table_color",
				table: "table_color");

			migrationBuilder.DropColumn(
				name: "hex_code",
				table: "table_color");

			migrationBuilder.RenameTable(
				name: "table_model",
				newName: "Models");

			migrationBuilder.RenameTable(
				name: "table_image",
				newName: "Images");

			migrationBuilder.RenameTable(
				name: "table_color",
				newName: "Colors");

			migrationBuilder.RenameIndex(
				name: "IX_table_model_FK_collection_id",
				table: "Models",
				newName: "IX_Models_FK_collection_id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Models",
				table: "Models",
				column: "id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Images",
				table: "Images",
				column: "id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Colors",
				table: "Colors",
				column: "id");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "18c22dce-ea7f-444c-ab16-2520dd09ff31");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "d2004087-67ae-4068-83a3-747832750a88");

			migrationBuilder.AddForeignKey(
				name: "FK_Models_table_collection_FK_collection_id",
				table: "Models",
				column: "FK_collection_id",
				principalTable: "table_collection",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_brand_image_Images_PK_FK_image_id",
				table: "table_brand_image",
				column: "PK_FK_image_id",
				principalTable: "Images",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_model_color_Colors_PK_FK_color_id",
				table: "table_model_color",
				column: "PK_FK_color_id",
				principalTable: "Colors",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_model_color_Models_PK_FK_model_id",
				table: "table_model_color",
				column: "PK_FK_model_id",
				principalTable: "Models",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_vehicle_Colors_FK_color_id",
				table: "table_vehicle",
				column: "FK_color_id",
				principalTable: "Colors",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_vehicle_Models_FK_model_id",
				table: "table_vehicle",
				column: "FK_model_id",
				principalTable: "Models",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_table_vehicle_image_Images_PK_FK_image_id",
				table: "table_vehicle_image",
				column: "PK_FK_image_id",
				principalTable: "Images",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
