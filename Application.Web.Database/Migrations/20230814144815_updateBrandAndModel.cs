using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class updateBrandAndModel : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_UserRoles_Roles_RoleId1",
				table: "UserRoles");

			migrationBuilder.DropForeignKey(
				name: "FK_UserRoles_Users_UserId1",
				table: "UserRoles");

			migrationBuilder.CreateTable(
				name: "Brand",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					Logo = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Brand", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Color",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Color", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Collection",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					BrandId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Collection", x => x.Id);
					table.ForeignKey(
						name: "FK_Collection_Brand_BrandId",
						column: x => x.BrandId,
						principalTable: "Brand",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Model",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					Year = table.Column<string>(type: "text", nullable: true),
					Capacity = table.Column<string>(type: "text", nullable: true),
					CollectionId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Model", x => x.Id);
					table.ForeignKey(
						name: "FK_Model_Collection_CollectionId",
						column: x => x.CollectionId,
						principalTable: "Collection",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ModelColor",
				columns: table => new
				{
					ModelId = table.Column<Guid>(type: "uuid", nullable: false),
					ColorId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ModelColor", x => new { x.ModelId, x.ColorId });
					table.ForeignKey(
						name: "FK_ModelColor_Color_ColorId",
						column: x => x.ColorId,
						principalTable: "Color",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ModelColor_Model_ModelId",
						column: x => x.ModelId,
						principalTable: "Model",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "c4bf4683-1ba0-4c19-bb68-a2a824fc01f6");

			migrationBuilder.CreateIndex(
				name: "IX_Collection_BrandId",
				table: "Collection",
				column: "BrandId");

			migrationBuilder.CreateIndex(
				name: "IX_Model_CollectionId",
				table: "Model",
				column: "CollectionId");

			migrationBuilder.CreateIndex(
				name: "IX_ModelColor_ColorId",
				table: "ModelColor",
				column: "ColorId");

			migrationBuilder.AddForeignKey(
				name: "FK_UserRoles_Roles_RoleId1",
				table: "UserRoles",
				column: "RoleId1",
				principalTable: "Roles",
				principalColumn: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_UserRoles_Users_UserId1",
				table: "UserRoles",
				column: "UserId1",
				principalTable: "Users",
				principalColumn: "Id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_UserRoles_Roles_RoleId1",
				table: "UserRoles");

			migrationBuilder.DropForeignKey(
				name: "FK_UserRoles_Users_UserId1",
				table: "UserRoles");

			migrationBuilder.DropTable(
				name: "ModelColor");

			migrationBuilder.DropTable(
				name: "Color");

			migrationBuilder.DropTable(
				name: "Model");

			migrationBuilder.DropTable(
				name: "Collection");

			migrationBuilder.DropTable(
				name: "Brand");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "ae05984b-ead7-4314-a187-6cd6ac519c50");

			migrationBuilder.AddForeignKey(
				name: "FK_UserRoles_Roles_RoleId1",
				table: "UserRoles",
				column: "RoleId1",
				principalTable: "Roles",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserRoles_Users_UserId1",
				table: "UserRoles",
				column: "UserId1",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
