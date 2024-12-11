﻿// <auto-generated />
using System;
using HotelSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelSystem.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    [Migration("20241211141614_UpdateRoomTypeColumn")]
    partial class UpdateRoomTypeColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelSystem.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GuestEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuestName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CheckInDate = new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CheckOutDate = new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestEmail = "johndoe@example.com",
                            GuestName = "John Doe",
                            RoomId = 1,
                            TotalPrice = 500.00m
                        },
                        new
                        {
                            Id = 2,
                            CheckInDate = new DateTime(2024, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CheckOutDate = new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestEmail = "janesmith@example.com",
                            GuestName = "Jane Smith",
                            RoomId = 2,
                            TotalPrice = 750.00m
                        });
                });

            modelBuilder.Entity("HotelSystem.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Beach Road",
                            Description = "A seaside resort with stunning ocean views.",
                            Name = "Oceanview Resort"
                        },
                        new
                        {
                            Id = 2,
                            Address = "456 Hilltop Ave",
                            Description = "A quiet retreat in the mountains.",
                            Name = "Mountain Escape"
                        });
                });

            modelBuilder.Entity("HotelSystem.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A cozy single room with a sea view.",
                            HotelId = 1,
                            IsAvailable = true,
                            Price = 100.00m,
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "A spacious double room with a balcony.",
                            HotelId = 1,
                            IsAvailable = true,
                            Price = 150.00m,
                            Type = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "A luxurious suite with mountain views.",
                            HotelId = 2,
                            IsAvailable = true,
                            Price = 250.00m,
                            Type = 2
                        });
                });

            modelBuilder.Entity("HotelSystem.Models.Booking", b =>
                {
                    b.HasOne("HotelSystem.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelSystem.Models.Room", b =>
                {
                    b.HasOne("HotelSystem.Models.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HotelSystem.Models.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
