using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class updateblogmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "table_blog_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_blog_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "table_blog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    short_description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FK_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_blog", x => x.id);
                    table.ForeignKey(
                        name: "FK_table_blog_table_blog_category_FK_category_id",
                        column: x => x.FK_category_id,
                        principalTable: "table_blog_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_blog_table_image_FK_image_id",
                        column: x => x.FK_image_id,
                        principalTable: "table_image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_blog_table_users_FK_author_id",
                        column: x => x.FK_author_id,
                        principalTable: "table_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "ceaf3739-2f2a-4d73-bfdd-051b80c31a7f");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "5468eced-f76e-47d1-8314-f9b1d4d08eac");

            migrationBuilder.CreateIndex(
                name: "IX_table_blog_FK_author_id",
                table: "table_blog",
                column: "FK_author_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_blog_FK_category_id",
                table: "table_blog",
                column: "FK_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_blog_FK_image_id",
                table: "table_blog",
                column: "FK_image_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "table_blog");

            migrationBuilder.DropTable(
                name: "table_blog_category");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "cdc4ccee-6838-4be0-8475-b135eb5d1dbf");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "abc5920b-4336-4872-8a31-ddfc632544bf");
        }
    }
}
