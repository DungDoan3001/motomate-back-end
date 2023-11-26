using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class updatevehiclereview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "table_vehicle_review",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_vehicle_review", x => x.id);
                    table.ForeignKey(
                        name: "FK_table_vehicle_review_table_vehicle_FK_vehicle_id",
                        column: x => x.FK_vehicle_id,
                        principalTable: "table_vehicle",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_vehicle_review_image",
                columns: table => new
                {
                    PK_FK_vehicle_review_id = table.Column<Guid>(type: "uuid", nullable: false),
                    PK_FK_image_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_vehicle_review_image", x => new { x.PK_FK_image_id, x.PK_FK_vehicle_review_id });
                    table.ForeignKey(
                        name: "FK_table_vehicle_review_image_table_image_PK_FK_image_id",
                        column: x => x.PK_FK_image_id,
                        principalTable: "table_image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_vehicle_review_image_table_vehicle_review_PK_FK_vehic~",
                        column: x => x.PK_FK_vehicle_review_id,
                        principalTable: "table_vehicle_review",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "63064f3d-8f6e-4d0b-a571-b9dc88bc9a29");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "58553b3e-0c05-4a69-91df-5aa28a0ba23d");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "5b80bd5b-e4c1-41b2-8470-8d80ae10a746");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "537a4f16-3c42-4705-aabf-2b5bb20f2ff1");

            migrationBuilder.CreateIndex(
                name: "IX_table_vehicle_review_FK_vehicle_id",
                table: "table_vehicle_review",
                column: "FK_vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_vehicle_review_image_PK_FK_vehicle_review_id",
                table: "table_vehicle_review_image",
                column: "PK_FK_vehicle_review_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "table_vehicle_review_image");

            migrationBuilder.DropTable(
                name: "table_vehicle_review");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("19021360-14c6-49ab-a91b-79c388115f4e"),
                column: "ConcurrencyStamp",
                value: "6786e95b-6963-4265-801e-3695719ace84");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("20308981-486a-456e-833c-1a8821121b3e"),
                column: "ConcurrencyStamp",
                value: "d81b1e51-3514-4c91-80bc-91c7643f2162");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "810f12c5-7a6e-4477-aa10-c8bec6d4cbd9");

            migrationBuilder.UpdateData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "3bc3a4d9-e6cf-46d9-976c-5c830ce972eb");
        }
    }
}
