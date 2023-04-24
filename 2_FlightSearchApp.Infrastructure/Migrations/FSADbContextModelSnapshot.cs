﻿// <auto-generated />
using System;
using FlightSearchApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightSearchApp.Infrastructure.Migrations
{
    [DbContext(typeof(FSADbContext))]
    partial class FSADbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlightSearchApp.Domain.CodeList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Entity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("CodeLists");
                });

            modelBuilder.Entity("FlightSearchApp.Domain.FlightOffer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("DepartureAirportCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinationAirportCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassengersNumber")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("TransferNumbersDeparture")
                        .HasColumnType("int");

                    b.Property<int>("TransferNumbersReturn")
                        .HasColumnType("int");

                    b.Property<int?>("ValueID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ValueID");

                    b.ToTable("FlightOffers");
                });

            modelBuilder.Entity("FlightSearchApp.Domain.FlightOffer", b =>
                {
                    b.HasOne("FlightSearchApp.Domain.CodeList", "Value")
                        .WithMany()
                        .HasForeignKey("ValueID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Value");
                });
#pragma warning restore 612, 618
        }
    }
}
