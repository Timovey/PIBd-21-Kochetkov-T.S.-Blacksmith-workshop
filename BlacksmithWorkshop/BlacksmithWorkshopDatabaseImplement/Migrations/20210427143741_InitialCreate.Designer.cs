﻿// <auto-generated />
using System;
using BlacksmithWorkshopDatabaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlacksmithWorkshopDatabaseImplement.Migrations
{
    [DbContext(typeof(BlacksmithWorkshopDatabase))]
    [Migration("20210427143741_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ComponentName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.Manufacture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ManufactureName")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Manufactures");
                });

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.ManufactureComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComponentId");

                    b.Property<int>("Count");

                    b.Property<int>("ManufactureId");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("ManufactureId");

                    b.ToTable("ManufactureComponents");
                });

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime?>("DateImplement");

                    b.Property<int>("ManufactureId");

                    b.Property<int>("Status");

                    b.Property<decimal>("Sum");

                    b.HasKey("Id");

                    b.HasIndex("ManufactureId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.WarehouseComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComponentId");

                    b.Property<int>("Count");

                    b.Property<int>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseComponents");
                });

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.ManufactureComponent", b =>
                {
                    b.HasOne("BlacksmithWorkshopDatabaseImplement.Models.Component", "Component")
                        .WithMany("ManufactureComponent")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlacksmithWorkshopDatabaseImplement.Models.Manufacture", "Manufacture")
                        .WithMany("ManufactureComponents")
                        .HasForeignKey("ManufactureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.Order", b =>
                {
                    b.HasOne("BlacksmithWorkshopDatabaseImplement.Models.Manufacture", "Manufacture")
                        .WithMany("Orders")
                        .HasForeignKey("ManufactureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlacksmithWorkshopDatabaseImplement.Models.WarehouseComponent", b =>
                {
                    b.HasOne("BlacksmithWorkshopDatabaseImplement.Models.Component", "Component")
                        .WithMany("WarehouseComponents")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlacksmithWorkshopDatabaseImplement.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseComponents")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
