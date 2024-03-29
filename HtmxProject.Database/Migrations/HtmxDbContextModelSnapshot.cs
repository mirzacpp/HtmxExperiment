﻿// <auto-generated />
using System;
using HtmxProject.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HtmxProject.Database.Migrations
{
    [DbContext(typeof(HtmxDbContext))]
    partial class HtmxDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HtmxProject.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_category");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("HtmxProject.Domain.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CompanyNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("company_number");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean")
                        .HasColumnName("is_verified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_company");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("HtmxProject.Domain.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid")
                        .HasColumnName("company_id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("ManufacturedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("manufactured_at");

                    b.Property<Guid>("ManufacturerId")
                        .HasColumnType("uuid")
                        .HasColumnName("manufacturer_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<Guid>("QuantityUnitId")
                        .HasColumnType("uuid")
                        .HasColumnName("quantity_unit_id");

                    b.Property<double>("StockQuantity")
                        .HasColumnType("double precision")
                        .HasColumnName("stock_quantity");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_item");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_item_category_id");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_item_company_id");

                    b.HasIndex("ManufacturerId")
                        .HasDatabaseName("ix_item_manufacturer_id");

                    b.HasIndex("QuantityUnitId")
                        .HasDatabaseName("ix_item_quantity_unit_id");

                    b.ToTable("item", (string)null);
                });

            modelBuilder.Entity("HtmxProject.Domain.Manufacturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("picture_url");

                    b.HasKey("Id")
                        .HasName("pk_manufacturer");

                    b.ToTable("manufacturer", (string)null);
                });

            modelBuilder.Entity("HtmxProject.Domain.MeasurementUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("name");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("short_name");

                    b.HasKey("Id")
                        .HasName("pk_measurement_unit");

                    b.ToTable("measurement_unit", (string)null);
                });

            modelBuilder.Entity("HtmxProject.Domain.Item", b =>
                {
                    b.HasOne("HtmxProject.Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_item_category_category_id");

                    b.HasOne("HtmxProject.Domain.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_item_company_company_id");

                    b.HasOne("HtmxProject.Domain.Manufacturer", null)
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_item_manufacturer_manufacturer_id");

                    b.HasOne("HtmxProject.Domain.MeasurementUnit", null)
                        .WithMany()
                        .HasForeignKey("QuantityUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_item_measurement_unit_measurement_unit_id");
                });
#pragma warning restore 612, 618
        }
    }
}
