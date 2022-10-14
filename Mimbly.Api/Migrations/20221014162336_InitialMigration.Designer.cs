﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mimbly.Infrastructure.Identity.Context;

#nullable disable

namespace Mimbly.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221014162336_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mimbly.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Name");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Parent_Id");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.CompanyContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Company_Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("First_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Last_name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("Varchar(15)")
                        .HasColumnName("Phone_number");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Company_Contact");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("Nvarchar(100)")
                        .HasColumnName("City");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("Nvarchar(100)")
                        .HasColumnName("Country");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("Varchar(5)")
                        .HasColumnName("Postal_code");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("Nvarchar(100)")
                        .HasColumnName("Region");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("Nvarchar(100)")
                        .HasColumnName("Street_Address");

                    b.HasKey("Id");

                    b.ToTable("Mimbox_Location");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Mimbox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<double>("Carbon")
                        .HasColumnType("float")
                        .HasColumnName("Carbon");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Company_Id");

                    b.Property<double>("Economy")
                        .HasColumnType("float")
                        .HasColumnName("Economy");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Location_Id");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Model_Id");

                    b.Property<double>("Plastic")
                        .HasColumnType("float")
                        .HasColumnName("Plastic");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Status_Id");

                    b.Property<double>("Water")
                        .HasColumnType("float")
                        .HasColumnName("Water");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ModelId");

                    b.HasIndex("StatusId");

                    b.ToTable("Mimbox");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("Date")
                        .HasColumnName("Created_At");

                    b.Property<string>("Log")
                        .IsRequired()
                        .HasColumnType("Nvarchar(max)")
                        .HasColumnName("Log");

                    b.Property<Guid>("MimboxId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Id");

                    b.HasKey("Id");

                    b.HasIndex("MimboxId");

                    b.ToTable("Mimbox_Log");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Mimbox_Model");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Mimbox_Status");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.CompanyContact", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Company", "Company")
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Mimbox", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("Mimbly.Domain.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("Mimbly.Domain.Entities.MimboxModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mimbly.Domain.Entities.MimboxStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Location");

                    b.Navigation("Model");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxLog", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Mimbox", "Mimbox")
                        .WithMany("MimboxLogs")
                        .HasForeignKey("MimboxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mimbox");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Company", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Mimbox", b =>
                {
                    b.Navigation("MimboxLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
