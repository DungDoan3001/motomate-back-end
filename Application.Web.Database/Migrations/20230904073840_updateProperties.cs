using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class updateProperties : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_BrandImage_Brand_BrandId",
				table: "BrandImage");

			migrationBuilder.DropForeignKey(
				name: "FK_BrandImage_Image_ImageId",
				table: "BrandImage");

			migrationBuilder.DropForeignKey(
				name: "FK_Collection_Brand_BrandId",
				table: "Collection");

			migrationBuilder.DropForeignKey(
				name: "FK_Model_Collection_CollectionId",
				table: "Model");

			migrationBuilder.DropForeignKey(
				name: "FK_ModelColor_Color_ColorId",
				table: "ModelColor");

			migrationBuilder.DropForeignKey(
				name: "FK_ModelColor_Model_ModelId",
				table: "ModelColor");

			migrationBuilder.DropForeignKey(
				name: "FK_ResetPassword_Users_UserId",
				table: "ResetPassword");

			migrationBuilder.RenameColumn(
				name: "Picture",
				table: "Users",
				newName: "picture");

			migrationBuilder.RenameColumn(
				name: "LastName",
				table: "Users",
				newName: "last_name");

			migrationBuilder.RenameColumn(
				name: "FirstName",
				table: "Users",
				newName: "first_name");

			migrationBuilder.RenameColumn(
				name: "Token",
				table: "ResetPassword",
				newName: "token");

			migrationBuilder.RenameColumn(
				name: "Id",
				table: "ResetPassword",
				newName: "id");

			migrationBuilder.RenameColumn(
				name: "UserId",
				table: "ResetPassword",
				newName: "FK_user_id");

			migrationBuilder.RenameColumn(
				name: "CreatedDate",
				table: "ResetPassword",
				newName: "created_date");

			migrationBuilder.RenameIndex(
				name: "IX_ResetPassword_UserId",
				table: "ResetPassword",
				newName: "IX_ResetPassword_FK_user_id");

			migrationBuilder.RenameColumn(
				name: "ColorId",
				table: "ModelColor",
				newName: "PK_FK_color_id");

			migrationBuilder.RenameColumn(
				name: "ModelId",
				table: "ModelColor",
				newName: "PK_FK_model_id");

			migrationBuilder.RenameIndex(
				name: "IX_ModelColor_ColorId",
				table: "ModelColor",
				newName: "IX_ModelColor_PK_FK_color_id");

			migrationBuilder.RenameColumn(
				name: "Year",
				table: "Model",
				newName: "year");

			migrationBuilder.RenameColumn(
				name: "Name",
				table: "Model",
				newName: "name");

			migrationBuilder.RenameColumn(
				name: "Capacity",
				table: "Model",
				newName: "capacity");

			migrationBuilder.RenameColumn(
				name: "Id",
				table: "Model",
				newName: "id");

			migrationBuilder.RenameColumn(
				name: "CollectionId",
				table: "Model",
				newName: "FK_collection_id");

			migrationBuilder.RenameIndex(
				name: "IX_Model_CollectionId",
				table: "Model",
				newName: "IX_Model_FK_collection_id");

			migrationBuilder.RenameColumn(
				name: "Id",
				table: "Image",
				newName: "id");

			migrationBuilder.RenameColumn(
				name: "PublicId",
				table: "Image",
				newName: "public_id");

			migrationBuilder.RenameColumn(
				name: "ImageUrl",
				table: "Image",
				newName: "image_url");

			migrationBuilder.RenameColumn(
				name: "Name",
				table: "Color",
				newName: "name");

			migrationBuilder.RenameColumn(
				name: "Id",
				table: "Color",
				newName: "id");

			migrationBuilder.RenameColumn(
				name: "Name",
				table: "Collection",
				newName: "name");

			migrationBuilder.RenameColumn(
				name: "Id",
				table: "Collection",
				newName: "id");

			migrationBuilder.RenameColumn(
				name: "BrandId",
				table: "Collection",
				newName: "FK_brand_id");

			migrationBuilder.RenameIndex(
				name: "IX_Collection_BrandId",
				table: "Collection",
				newName: "IX_Collection_FK_brand_id");

			migrationBuilder.RenameColumn(
				name: "ImageId",
				table: "BrandImage",
				newName: "PK_FK_image_id");

			migrationBuilder.RenameColumn(
				name: "BrandId",
				table: "BrandImage",
				newName: "PK_FK_brand_id");

			migrationBuilder.RenameIndex(
				name: "IX_BrandImage_ImageId",
				table: "BrandImage",
				newName: "IX_BrandImage_PK_FK_image_id");

			migrationBuilder.RenameColumn(
				name: "Name",
				table: "Brand",
				newName: "name");

			migrationBuilder.RenameColumn(
				name: "Id",
				table: "Brand",
				newName: "id");

			migrationBuilder.RenameColumn(
				name: "ImageId",
				table: "Brand",
				newName: "FK_image_id");

			migrationBuilder.AddColumn<string>(
				name: "address",
				table: "Users",
				type: "text",
				nullable: true);

			migrationBuilder.AddColumn<DateTime>(
				name: "date_of_birth",
				table: "Users",
				type: "timestamp with time zone",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "bde8c009-0509-403f-86b7-404d53c6bb39");

			migrationBuilder.AddForeignKey(
				name: "FK_BrandImage_Brand_PK_FK_brand_id",
				table: "BrandImage",
				column: "PK_FK_brand_id",
				principalTable: "Brand",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_BrandImage_Image_PK_FK_image_id",
				table: "BrandImage",
				column: "PK_FK_image_id",
				principalTable: "Image",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Collection_Brand_FK_brand_id",
				table: "Collection",
				column: "FK_brand_id",
				principalTable: "Brand",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Model_Collection_FK_collection_id",
				table: "Model",
				column: "FK_collection_id",
				principalTable: "Collection",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_ModelColor_Color_PK_FK_color_id",
				table: "ModelColor",
				column: "PK_FK_color_id",
				principalTable: "Color",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_ModelColor_Model_PK_FK_model_id",
				table: "ModelColor",
				column: "PK_FK_model_id",
				principalTable: "Model",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_ResetPassword_Users_FK_user_id",
				table: "ResetPassword",
				column: "FK_user_id",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_BrandImage_Brand_PK_FK_brand_id",
				table: "BrandImage");

			migrationBuilder.DropForeignKey(
				name: "FK_BrandImage_Image_PK_FK_image_id",
				table: "BrandImage");

			migrationBuilder.DropForeignKey(
				name: "FK_Collection_Brand_FK_brand_id",
				table: "Collection");

			migrationBuilder.DropForeignKey(
				name: "FK_Model_Collection_FK_collection_id",
				table: "Model");

			migrationBuilder.DropForeignKey(
				name: "FK_ModelColor_Color_PK_FK_color_id",
				table: "ModelColor");

			migrationBuilder.DropForeignKey(
				name: "FK_ModelColor_Model_PK_FK_model_id",
				table: "ModelColor");

			migrationBuilder.DropForeignKey(
				name: "FK_ResetPassword_Users_FK_user_id",
				table: "ResetPassword");

			migrationBuilder.DropColumn(
				name: "address",
				table: "Users");

			migrationBuilder.DropColumn(
				name: "date_of_birth",
				table: "Users");

			migrationBuilder.RenameColumn(
				name: "picture",
				table: "Users",
				newName: "Picture");

			migrationBuilder.RenameColumn(
				name: "last_name",
				table: "Users",
				newName: "LastName");

			migrationBuilder.RenameColumn(
				name: "first_name",
				table: "Users",
				newName: "FirstName");

			migrationBuilder.RenameColumn(
				name: "token",
				table: "ResetPassword",
				newName: "Token");

			migrationBuilder.RenameColumn(
				name: "id",
				table: "ResetPassword",
				newName: "Id");

			migrationBuilder.RenameColumn(
				name: "created_date",
				table: "ResetPassword",
				newName: "CreatedDate");

			migrationBuilder.RenameColumn(
				name: "FK_user_id",
				table: "ResetPassword",
				newName: "UserId");

			migrationBuilder.RenameIndex(
				name: "IX_ResetPassword_FK_user_id",
				table: "ResetPassword",
				newName: "IX_ResetPassword_UserId");

			migrationBuilder.RenameColumn(
				name: "PK_FK_color_id",
				table: "ModelColor",
				newName: "ColorId");

			migrationBuilder.RenameColumn(
				name: "PK_FK_model_id",
				table: "ModelColor",
				newName: "ModelId");

			migrationBuilder.RenameIndex(
				name: "IX_ModelColor_PK_FK_color_id",
				table: "ModelColor",
				newName: "IX_ModelColor_ColorId");

			migrationBuilder.RenameColumn(
				name: "year",
				table: "Model",
				newName: "Year");

			migrationBuilder.RenameColumn(
				name: "name",
				table: "Model",
				newName: "Name");

			migrationBuilder.RenameColumn(
				name: "capacity",
				table: "Model",
				newName: "Capacity");

			migrationBuilder.RenameColumn(
				name: "id",
				table: "Model",
				newName: "Id");

			migrationBuilder.RenameColumn(
				name: "FK_collection_id",
				table: "Model",
				newName: "CollectionId");

			migrationBuilder.RenameIndex(
				name: "IX_Model_FK_collection_id",
				table: "Model",
				newName: "IX_Model_CollectionId");

			migrationBuilder.RenameColumn(
				name: "id",
				table: "Image",
				newName: "Id");

			migrationBuilder.RenameColumn(
				name: "public_id",
				table: "Image",
				newName: "PublicId");

			migrationBuilder.RenameColumn(
				name: "image_url",
				table: "Image",
				newName: "ImageUrl");

			migrationBuilder.RenameColumn(
				name: "name",
				table: "Color",
				newName: "Name");

			migrationBuilder.RenameColumn(
				name: "id",
				table: "Color",
				newName: "Id");

			migrationBuilder.RenameColumn(
				name: "name",
				table: "Collection",
				newName: "Name");

			migrationBuilder.RenameColumn(
				name: "id",
				table: "Collection",
				newName: "Id");

			migrationBuilder.RenameColumn(
				name: "FK_brand_id",
				table: "Collection",
				newName: "BrandId");

			migrationBuilder.RenameIndex(
				name: "IX_Collection_FK_brand_id",
				table: "Collection",
				newName: "IX_Collection_BrandId");

			migrationBuilder.RenameColumn(
				name: "PK_FK_image_id",
				table: "BrandImage",
				newName: "ImageId");

			migrationBuilder.RenameColumn(
				name: "PK_FK_brand_id",
				table: "BrandImage",
				newName: "BrandId");

			migrationBuilder.RenameIndex(
				name: "IX_BrandImage_PK_FK_image_id",
				table: "BrandImage",
				newName: "IX_BrandImage_ImageId");

			migrationBuilder.RenameColumn(
				name: "name",
				table: "Brand",
				newName: "Name");

			migrationBuilder.RenameColumn(
				name: "id",
				table: "Brand",
				newName: "Id");

			migrationBuilder.RenameColumn(
				name: "FK_image_id",
				table: "Brand",
				newName: "ImageId");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "e09c3ea0-6f88-4b15-a7ac-be4f3adf9936");

			migrationBuilder.AddForeignKey(
				name: "FK_BrandImage_Brand_BrandId",
				table: "BrandImage",
				column: "BrandId",
				principalTable: "Brand",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_BrandImage_Image_ImageId",
				table: "BrandImage",
				column: "ImageId",
				principalTable: "Image",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Collection_Brand_BrandId",
				table: "Collection",
				column: "BrandId",
				principalTable: "Brand",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Model_Collection_CollectionId",
				table: "Model",
				column: "CollectionId",
				principalTable: "Collection",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_ModelColor_Color_ColorId",
				table: "ModelColor",
				column: "ColorId",
				principalTable: "Color",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_ModelColor_Model_ModelId",
				table: "ModelColor",
				column: "ModelId",
				principalTable: "Model",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_ResetPassword_Users_UserId",
				table: "ResetPassword",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
