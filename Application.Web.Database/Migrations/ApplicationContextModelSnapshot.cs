﻿// <auto-generated />
using System;
using Application.Web.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Application.Web.Database.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Application.Web.Database.Models.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_image_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("table_brand", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.BrandImage", b =>
                {
                    b.Property<Guid>("BrandId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_brand_id");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_image_id");

                    b.HasKey("BrandId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("table_brand_image", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("table_cart", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.CartVehicle", b =>
                {
                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_cart_id");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_vehicle_id");

                    b.HasKey("CartId", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("table_cart_vehicle", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_at");

                    b.HasKey("Id");

                    b.ToTable("table_chat", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.ChatMember", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_user_id");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_chat_id");

                    b.HasKey("UserId", "ChatId");

                    b.HasIndex("ChatId");

                    b.ToTable("table_chat_member", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Collection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_brand_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("table_collection", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Color", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("HexCode")
                        .HasColumnType("text")
                        .HasColumnName("hex_code");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("table_color", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("PublicId")
                        .HasColumnType("text")
                        .HasColumnName("public_id");

                    b.HasKey("Id");

                    b.ToTable("table_image", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_chat_id");

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_sender_id");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("table_message", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Capacity")
                        .HasColumnType("text")
                        .HasColumnName("capacity");

                    b.Property<Guid>("CollectionId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_collection_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Year")
                        .HasColumnType("text")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.ToTable("table_model", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.ModelColor", b =>
                {
                    b.Property<Guid>("ModelId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_model_id");

                    b.Property<Guid>("ColorId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_color_id");

                    b.HasKey("ModelId", "ColorId");

                    b.HasIndex("ColorId");

                    b.ToTable("table_model_color", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.ResetPassword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Token")
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("table_reset_password", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("table_roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("60929087-1227-4efd-af43-e9ae2524eb0e"),
                            ConcurrencyStamp = "c0ae3d56-863e-4afc-9d18-0e8d1f99d4a0",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                            ConcurrencyStamp = "41fc447b-86fd-424d-a38b-63b47c3e1665",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Application.Web.Database.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Picture")
                        .HasColumnType("text")
                        .HasColumnName("picture");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("table_users", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<Guid>("ColorId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_color_id");

                    b.Property<int>("ConditionPercentage")
                        .HasColumnType("integer")
                        .HasColumnName("condition_percentage");

                    b.Property<string>("District")
                        .HasColumnType("text")
                        .HasColumnName("district");

                    b.Property<DateTime>("InsuranceExpiry")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("insurance_expiry");

                    b.Property<string>("InsuranceNumber")
                        .HasColumnType("text")
                        .HasColumnName("insurance_number");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean")
                        .HasColumnName("is_available");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean")
                        .HasColumnName("is_lock");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("text")
                        .HasColumnName("license_plate");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_model_id");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_owner_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("purchase_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Ward")
                        .HasColumnType("text")
                        .HasColumnName("ward");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("ModelId");

                    b.HasIndex("OwnerId");

                    b.ToTable("table_vehicle", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.VehicleImage", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_vehicle_id");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_image_id");

                    b.HasKey("VehicleId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("table_vehicle_image", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("table_roleclaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("table_userclaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("table_userlogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("table_userroles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<Guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("table_usertokens", (string)null);
                });

            modelBuilder.Entity("Application.Web.Database.Models.UserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>");

                    b.Property<Guid?>("RoleId1")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uuid");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("table_userroles");

                    b.HasDiscriminator().HasValue("UserRole");
                });

            modelBuilder.Entity("Application.Web.Database.Models.BrandImage", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Brand", "Brand")
                        .WithMany("BrandImages")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.Image", "Image")
                        .WithMany("BrandImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Cart", b =>
                {
                    b.HasOne("Application.Web.Database.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Web.Database.Models.CartVehicle", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Cart", "Cart")
                        .WithMany("CartVehicles")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.Vehicle", "Vehicle")
                        .WithMany("CartVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Application.Web.Database.Models.ChatMember", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Chat", "Chat")
                        .WithMany("ChatMembers")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.User", "User")
                        .WithMany("ChatMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Collection", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Brand", "Brand")
                        .WithMany("Collections")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Message", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Model", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Collection", "Collection")
                        .WithMany("Models")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("Application.Web.Database.Models.ModelColor", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Color", "Color")
                        .WithMany("ModelColors")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.Model", "Model")
                        .WithMany("ModelColors")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Application.Web.Database.Models.ResetPassword", b =>
                {
                    b.HasOne("Application.Web.Database.Models.User", "User")
                        .WithOne("ResetPassword")
                        .HasForeignKey("Application.Web.Database.Models.ResetPassword", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Vehicle", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Color", "Color")
                        .WithMany("Vehicles")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.Model", "Model")
                        .WithMany("Vehicles")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.User", "Owner")
                        .WithMany("Vehicles")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Model");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Application.Web.Database.Models.VehicleImage", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Image", "Image")
                        .WithMany("VehicleImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.Vehicle", "Vehicle")
                        .WithMany("VehicleImages")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Application.Web.Database.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Application.Web.Database.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Web.Database.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Application.Web.Database.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Web.Database.Models.UserRole", b =>
                {
                    b.HasOne("Application.Web.Database.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId1");

                    b.HasOne("Application.Web.Database.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId1");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Brand", b =>
                {
                    b.Navigation("BrandImages");

                    b.Navigation("Collections");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Cart", b =>
                {
                    b.Navigation("CartVehicles");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Chat", b =>
                {
                    b.Navigation("ChatMembers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Collection", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Color", b =>
                {
                    b.Navigation("ModelColors");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Image", b =>
                {
                    b.Navigation("BrandImages");

                    b.Navigation("VehicleImages");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Model", b =>
                {
                    b.Navigation("ModelColors");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Application.Web.Database.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("ChatMembers");

                    b.Navigation("Messages");

                    b.Navigation("ResetPassword");

                    b.Navigation("UserRoles");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Application.Web.Database.Models.Vehicle", b =>
                {
                    b.Navigation("CartVehicles");

                    b.Navigation("VehicleImages");
                });
#pragma warning restore 612, 618
        }
    }
}
