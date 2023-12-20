using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class addColorFieldToProduct : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<Guid>(
				name: "FK_color_id",
				table: "Vehicle",
				type: "uuid",
				nullable: false,
				defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "d5790af2-07d4-4637-ba45-53a9dc4a6ffe");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "09ae8032-7de1-4999-b84b-176efede3035");

			migrationBuilder.CreateIndex(
				name: "IX_Vehicle_FK_color_id",
				table: "Vehicle",
				column: "FK_color_id");

			migrationBuilder.AddForeignKey(
				name: "FK_Vehicle_Color_FK_color_id",
				table: "Vehicle",
				column: "FK_color_id",
				principalTable: "Color",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Vehicle_Color_FK_color_id",
				table: "Vehicle");

			migrationBuilder.DropIndex(
				name: "IX_Vehicle_FK_color_id",
				table: "Vehicle");

			migrationBuilder.DropColumn(
				name: "FK_color_id",
				table: "Vehicle");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "2036c1ca-da7e-44ec-a29f-d07ef1258347");

			migrationBuilder.UpdateData(
				table: "Roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "b8a968d5-e642-46d2-addb-c2cb07c3c485");
		}
	}
}
