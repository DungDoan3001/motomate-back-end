using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Web.Database.Migrations
{
    public partial class updateContextConfigurtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId1",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId1",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Color_FK_color_id",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Model_FK_model_id",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Users_FK_owner_id",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleImage_Image_PK_FK_image_id",
                table: "VehicleImage");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleImage_Vehicle_PK_FK_vehicle_id",
                table: "VehicleImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleImage",
                table: "VehicleImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResetPassword",
                table: "ResetPassword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModelColor",
                table: "ModelColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Model",
                table: "Model");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Color",
                table: "Color");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collection",
                table: "Collection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrandImage",
                table: "BrandImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "VehicleImage",
                newName: "table_vehicle_image");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "table_vehicle");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                newName: "table_usertokens");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "table_users");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "table_userroles");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                newName: "table_userlogins");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                newName: "table_userclaims");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "table_roles");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                newName: "table_roleclaims");

            migrationBuilder.RenameTable(
                name: "ResetPassword",
                newName: "table_reset_password");

            migrationBuilder.RenameTable(
                name: "ModelColor",
                newName: "table_model_color");

            migrationBuilder.RenameTable(
                name: "Model",
                newName: "Models");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Color",
                newName: "Colors");

            migrationBuilder.RenameTable(
                name: "Collection",
                newName: "table_collection");

            migrationBuilder.RenameTable(
                name: "BrandImage",
                newName: "table_brand_image");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "table_brand");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleImage_PK_FK_image_id",
                table: "table_vehicle_image",
                newName: "IX_table_vehicle_image_PK_FK_image_id");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_FK_owner_id",
                table: "table_vehicle",
                newName: "IX_table_vehicle_FK_owner_id");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_FK_model_id",
                table: "table_vehicle",
                newName: "IX_table_vehicle_FK_model_id");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_FK_color_id",
                table: "table_vehicle",
                newName: "IX_table_vehicle_FK_color_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserId1",
                table: "table_userroles",
                newName: "IX_table_userroles_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId1",
                table: "table_userroles",
                newName: "IX_table_userroles_RoleId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "table_userroles",
                newName: "IX_table_userroles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                table: "table_userlogins",
                newName: "IX_table_userlogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                table: "table_userclaims",
                newName: "IX_table_userclaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaims_RoleId",
                table: "table_roleclaims",
                newName: "IX_table_roleclaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ResetPassword_FK_user_id",
                table: "table_reset_password",
                newName: "IX_table_reset_password_FK_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_ModelColor_PK_FK_color_id",
                table: "table_model_color",
                newName: "IX_table_model_color_PK_FK_color_id");

            migrationBuilder.RenameIndex(
                name: "IX_Model_FK_collection_id",
                table: "Models",
                newName: "IX_Models_FK_collection_id");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_FK_brand_id",
                table: "table_collection",
                newName: "IX_table_collection_FK_brand_id");

            migrationBuilder.RenameIndex(
                name: "IX_BrandImage_PK_FK_image_id",
                table: "table_brand_image",
                newName: "IX_table_brand_image_PK_FK_image_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_vehicle_image",
                table: "table_vehicle_image",
                columns: new[] { "PK_FK_vehicle_id", "PK_FK_image_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_vehicle",
                table: "table_vehicle",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_usertokens",
                table: "table_usertokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_users",
                table: "table_users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_userroles",
                table: "table_userroles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_userlogins",
                table: "table_userlogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_userclaims",
                table: "table_userclaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_roles",
                table: "table_roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_roleclaims",
                table: "table_roleclaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_reset_password",
                table: "table_reset_password",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_model_color",
                table: "table_model_color",
                columns: new[] { "PK_FK_model_id", "PK_FK_color_id" });

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_collection",
                table: "table_collection",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_brand_image",
                table: "table_brand_image",
                columns: new[] { "PK_FK_brand_id", "PK_FK_image_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_brand",
                table: "table_brand",
                column: "id");

            migrationBuilder.CreateTable(
                name: "table_chat",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_chat", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "table_chat_member",
                columns: table => new
                {
                    PK_FK_chat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    PK_FK_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_chat_member", x => new { x.PK_FK_user_id, x.PK_FK_chat_id });
                    table.ForeignKey(
                        name: "FK_table_chat_member_table_chat_PK_FK_chat_id",
                        column: x => x.PK_FK_chat_id,
                        principalTable: "table_chat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_chat_member_table_users_PK_FK_user_id",
                        column: x => x.PK_FK_user_id,
                        principalTable: "table_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_message",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_chat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_message", x => x.id);
                    table.ForeignKey(
                        name: "FK_table_message_table_chat_FK_chat_id",
                        column: x => x.FK_chat_id,
                        principalTable: "table_chat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_message_table_users_FK_sender_id",
                        column: x => x.FK_sender_id,
                        principalTable: "table_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_table_chat_member_PK_FK_chat_id",
                table: "table_chat_member",
                column: "PK_FK_chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_message_FK_chat_id",
                table: "table_message",
                column: "FK_chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_message_FK_sender_id",
                table: "table_message",
                column: "FK_sender_id");

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
                name: "FK_table_brand_image_table_brand_PK_FK_brand_id",
                table: "table_brand_image",
                column: "PK_FK_brand_id",
                principalTable: "table_brand",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_collection_table_brand_FK_brand_id",
                table: "table_collection",
                column: "FK_brand_id",
                principalTable: "table_brand",
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
                name: "FK_table_reset_password_table_users_FK_user_id",
                table: "table_reset_password",
                column: "FK_user_id",
                principalTable: "table_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_roleclaims_table_roles_RoleId",
                table: "table_roleclaims",
                column: "RoleId",
                principalTable: "table_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_userclaims_table_users_UserId",
                table: "table_userclaims",
                column: "UserId",
                principalTable: "table_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_userlogins_table_users_UserId",
                table: "table_userlogins",
                column: "UserId",
                principalTable: "table_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_userroles_table_roles_RoleId",
                table: "table_userroles",
                column: "RoleId",
                principalTable: "table_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_userroles_table_roles_RoleId1",
                table: "table_userroles",
                column: "RoleId1",
                principalTable: "table_roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_table_userroles_table_users_UserId",
                table: "table_userroles",
                column: "UserId",
                principalTable: "table_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_userroles_table_users_UserId1",
                table: "table_userroles",
                column: "UserId1",
                principalTable: "table_users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_table_usertokens_table_users_UserId",
                table: "table_usertokens",
                column: "UserId",
                principalTable: "table_users",
                principalColumn: "Id",
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
                name: "FK_table_vehicle_table_users_FK_owner_id",
                table: "table_vehicle",
                column: "FK_owner_id",
                principalTable: "table_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_vehicle_image_Images_PK_FK_image_id",
                table: "table_vehicle_image",
                column: "PK_FK_image_id",
                principalTable: "Images",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_vehicle_image_table_vehicle_PK_FK_vehicle_id",
                table: "table_vehicle_image",
                column: "PK_FK_vehicle_id",
                principalTable: "table_vehicle",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_table_collection_FK_collection_id",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_table_brand_image_Images_PK_FK_image_id",
                table: "table_brand_image");

            migrationBuilder.DropForeignKey(
                name: "FK_table_brand_image_table_brand_PK_FK_brand_id",
                table: "table_brand_image");

            migrationBuilder.DropForeignKey(
                name: "FK_table_collection_table_brand_FK_brand_id",
                table: "table_collection");

            migrationBuilder.DropForeignKey(
                name: "FK_table_model_color_Colors_PK_FK_color_id",
                table: "table_model_color");

            migrationBuilder.DropForeignKey(
                name: "FK_table_model_color_Models_PK_FK_model_id",
                table: "table_model_color");

            migrationBuilder.DropForeignKey(
                name: "FK_table_reset_password_table_users_FK_user_id",
                table: "table_reset_password");

            migrationBuilder.DropForeignKey(
                name: "FK_table_roleclaims_table_roles_RoleId",
                table: "table_roleclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_table_userclaims_table_users_UserId",
                table: "table_userclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_table_userlogins_table_users_UserId",
                table: "table_userlogins");

            migrationBuilder.DropForeignKey(
                name: "FK_table_userroles_table_roles_RoleId",
                table: "table_userroles");

            migrationBuilder.DropForeignKey(
                name: "FK_table_userroles_table_roles_RoleId1",
                table: "table_userroles");

            migrationBuilder.DropForeignKey(
                name: "FK_table_userroles_table_users_UserId",
                table: "table_userroles");

            migrationBuilder.DropForeignKey(
                name: "FK_table_userroles_table_users_UserId1",
                table: "table_userroles");

            migrationBuilder.DropForeignKey(
                name: "FK_table_usertokens_table_users_UserId",
                table: "table_usertokens");

            migrationBuilder.DropForeignKey(
                name: "FK_table_vehicle_Colors_FK_color_id",
                table: "table_vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_table_vehicle_Models_FK_model_id",
                table: "table_vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_table_vehicle_table_users_FK_owner_id",
                table: "table_vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_table_vehicle_image_Images_PK_FK_image_id",
                table: "table_vehicle_image");

            migrationBuilder.DropForeignKey(
                name: "FK_table_vehicle_image_table_vehicle_PK_FK_vehicle_id",
                table: "table_vehicle_image");

            migrationBuilder.DropTable(
                name: "table_chat_member");

            migrationBuilder.DropTable(
                name: "table_message");

            migrationBuilder.DropTable(
                name: "table_chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_vehicle_image",
                table: "table_vehicle_image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_vehicle",
                table: "table_vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_usertokens",
                table: "table_usertokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_users",
                table: "table_users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_userroles",
                table: "table_userroles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_userlogins",
                table: "table_userlogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_userclaims",
                table: "table_userclaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_roles",
                table: "table_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_roleclaims",
                table: "table_roleclaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_reset_password",
                table: "table_reset_password");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_model_color",
                table: "table_model_color");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_collection",
                table: "table_collection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_brand_image",
                table: "table_brand_image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_brand",
                table: "table_brand");

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
                name: "table_vehicle_image",
                newName: "VehicleImage");

            migrationBuilder.RenameTable(
                name: "table_vehicle",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "table_usertokens",
                newName: "UserTokens");

            migrationBuilder.RenameTable(
                name: "table_users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "table_userroles",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "table_userlogins",
                newName: "UserLogins");

            migrationBuilder.RenameTable(
                name: "table_userclaims",
                newName: "UserClaims");

            migrationBuilder.RenameTable(
                name: "table_roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "table_roleclaims",
                newName: "RoleClaims");

            migrationBuilder.RenameTable(
                name: "table_reset_password",
                newName: "ResetPassword");

            migrationBuilder.RenameTable(
                name: "table_model_color",
                newName: "ModelColor");

            migrationBuilder.RenameTable(
                name: "table_collection",
                newName: "Collection");

            migrationBuilder.RenameTable(
                name: "table_brand_image",
                newName: "BrandImage");

            migrationBuilder.RenameTable(
                name: "table_brand",
                newName: "Brand");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "Model");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "Color");

            migrationBuilder.RenameIndex(
                name: "IX_table_vehicle_image_PK_FK_image_id",
                table: "VehicleImage",
                newName: "IX_VehicleImage_PK_FK_image_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_vehicle_FK_owner_id",
                table: "Vehicle",
                newName: "IX_Vehicle_FK_owner_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_vehicle_FK_model_id",
                table: "Vehicle",
                newName: "IX_Vehicle_FK_model_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_vehicle_FK_color_id",
                table: "Vehicle",
                newName: "IX_Vehicle_FK_color_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_userroles_UserId1",
                table: "UserRoles",
                newName: "IX_UserRoles_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_table_userroles_RoleId1",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId1");

            migrationBuilder.RenameIndex(
                name: "IX_table_userroles_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_table_userlogins_UserId",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_table_userclaims_UserId",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_table_roleclaims_RoleId",
                table: "RoleClaims",
                newName: "IX_RoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_table_reset_password_FK_user_id",
                table: "ResetPassword",
                newName: "IX_ResetPassword_FK_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_model_color_PK_FK_color_id",
                table: "ModelColor",
                newName: "IX_ModelColor_PK_FK_color_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_collection_FK_brand_id",
                table: "Collection",
                newName: "IX_Collection_FK_brand_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_brand_image_PK_FK_image_id",
                table: "BrandImage",
                newName: "IX_BrandImage_PK_FK_image_id");

            migrationBuilder.RenameIndex(
                name: "IX_Models_FK_collection_id",
                table: "Model",
                newName: "IX_Model_FK_collection_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleImage",
                table: "VehicleImage",
                columns: new[] { "PK_FK_vehicle_id", "PK_FK_image_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResetPassword",
                table: "ResetPassword",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModelColor",
                table: "ModelColor",
                columns: new[] { "PK_FK_model_id", "PK_FK_color_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collection",
                table: "Collection",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrandImage",
                table: "BrandImage",
                columns: new[] { "PK_FK_brand_id", "PK_FK_image_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Model",
                table: "Model",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Color",
                table: "Color",
                column: "id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                column: "ConcurrencyStamp",
                value: "13e663cb-b880-432f-ac1b-3089fb53c1ec");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                column: "ConcurrencyStamp",
                value: "9bef5a46-bb9f-4b69-be95-69cd3e2a42d9");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                table: "RoleClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId1",
                table: "UserRoles",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId1",
                table: "UserRoles",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Color_FK_color_id",
                table: "Vehicle",
                column: "FK_color_id",
                principalTable: "Color",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Model_FK_model_id",
                table: "Vehicle",
                column: "FK_model_id",
                principalTable: "Model",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Users_FK_owner_id",
                table: "Vehicle",
                column: "FK_owner_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
