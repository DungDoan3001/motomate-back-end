using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
	public partial class addblogcomment : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<int>(
				name: "rating",
				table: "table_vehicle_review",
				type: "integer",
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "numeric");

			migrationBuilder.CreateTable(
				name: "table_blog_comment",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_user_id = table.Column<Guid>(type: "uuid", nullable: false),
					FK_blog_id = table.Column<Guid>(type: "uuid", nullable: false),
					comment = table.Column<string>(type: "text", nullable: true),
					created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_table_blog_comment", x => x.id);
					table.ForeignKey(
						name: "FK_table_blog_comment_table_blog_FK_blog_id",
						column: x => x.FK_blog_id,
						principalTable: "table_blog",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_table_blog_comment_table_users_FK_user_id",
						column: x => x.FK_user_id,
						principalTable: "table_users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
				column: "ConcurrencyStamp",
				value: "5f5bc496-12c3-49a3-a79d-ae700803e289");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
				column: "ConcurrencyStamp",
				value: "a8728e3c-b95b-4b63-ba49-8295eda1e3cd");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "85393ae4-44bd-4090-a698-c00777297869");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "1cd34644-0e35-45b6-bce3-c9f011bdb7e1");

			migrationBuilder.CreateIndex(
				name: "IX_table_blog_comment_FK_blog_id",
				table: "table_blog_comment",
				column: "FK_blog_id");

			migrationBuilder.CreateIndex(
				name: "IX_table_blog_comment_FK_user_id",
				table: "table_blog_comment",
				column: "FK_user_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "table_blog_comment");

			migrationBuilder.AlterColumn<decimal>(
				name: "rating",
				table: "table_vehicle_review",
				type: "numeric",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "integer");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
				column: "ConcurrencyStamp",
				value: "8170e8f6-132c-4b99-bf05-b880978b2d76");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
				column: "ConcurrencyStamp",
				value: "4ea9cfa0-05d0-49a7-aa57-7e213bf54b00");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
				column: "ConcurrencyStamp",
				value: "1137c1d1-3b8e-416d-a45f-85e1b46f00af");

			migrationBuilder.UpdateData(
				table: "table_roles",
				keyColumn: "Id",
				keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
				column: "ConcurrencyStamp",
				value: "a2d701e2-525e-4e86-a072-0be2cb3d3d93");
		}
	}
}
