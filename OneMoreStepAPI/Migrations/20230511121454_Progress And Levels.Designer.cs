﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneMoreStepAPI.Data;

namespace OneMoreStepAPI.Migrations
{
    [DbContext(typeof(OneMoreStepAPIDbContext))]
    [Migration("20230511121454_Progress And Levels")]
    partial class ProgressAndLevels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OneMoreStepAPI.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Lvl")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.Progress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UsersProgress")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Progress");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoordinatesJSON")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.RoutesLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LikeDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RoutesLikes");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.RoutesPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RoutesPictures");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.Sticker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stickers");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.UsersPinnedSticker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StickerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StickerId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UsersPinnedStickers");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.UsersStepsCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("StepsCount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UsersStepsCount");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.UsersStickers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("StickerId")
                        .HasColumnType("int");

                    b.Property<int>("StikerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StickerId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersStickers");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.Level", b =>
                {
                    b.HasOne("OneMoreStepAPI.Models.User", "User")
                        .WithMany("Levels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.Progress", b =>
                {
                    b.HasOne("OneMoreStepAPI.Models.User", "User")
                        .WithMany("Progress")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.Route", b =>
                {
                    b.HasOne("OneMoreStepAPI.Models.User", "User")
                        .WithMany("Routes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.RoutesLikes", b =>
                {
                    b.HasOne("OneMoreStepAPI.Models.Route", "Route")
                        .WithMany("RoutesLikes")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.RoutesPicture", b =>
                {
                    b.HasOne("OneMoreStepAPI.Models.Route", "Route")
                        .WithMany("RoutesPictures")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.UsersPinnedSticker", b =>
                {
                    b.HasOne("OneMoreStepAPI.Models.Sticker", "Sticker")
                        .WithMany()
                        .HasForeignKey("StickerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneMoreStepAPI.Models.User", "User")
                        .WithOne("UserPinnedSticker")
                        .HasForeignKey("OneMoreStepAPI.Models.UsersPinnedSticker", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sticker");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.UsersStepsCount", b =>
                {
                    b.HasOne("OneMoreStepAPI.Models.User", "User")
                        .WithMany("UsersStepsCounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.UsersStickers", b =>
                {
                    b.HasOne("OneMoreStepAPI.Models.Sticker", "Sticker")
                        .WithMany("UsersStickers")
                        .HasForeignKey("StickerId");

                    b.HasOne("OneMoreStepAPI.Models.User", "User")
                        .WithMany("UsersStickers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sticker");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.Route", b =>
                {
                    b.Navigation("RoutesLikes");

                    b.Navigation("RoutesPictures");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.Sticker", b =>
                {
                    b.Navigation("UsersStickers");
                });

            modelBuilder.Entity("OneMoreStepAPI.Models.User", b =>
                {
                    b.Navigation("Levels");

                    b.Navigation("Progress");

                    b.Navigation("Routes");

                    b.Navigation("UserPinnedSticker");

                    b.Navigation("UsersStepsCounts");

                    b.Navigation("UsersStickers");
                });
#pragma warning restore 612, 618
        }
    }
}
