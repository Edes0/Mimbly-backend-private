﻿// <auto-generated />
using System;
using Mimbly.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mimbly.Infrastructure.Identity.Context;

#nullable disable

namespace Mimbly.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Mimbly.Domain.Models.Mimbly", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<sbyte>("Age")
                        .HasColumnType("TINYINT")
                        .HasColumnName("age");

                    b.Property<string>("FirstName")
                        .HasColumnType("Char(108)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("Char(108)")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.HasIndex("Age");

                    b.ToTable("Mimbly");

                    b.HasData(
                        new
                        {
                            Id = new Guid("70a8cb1e-9fca-42d7-8310-b78188655509"),
                            Age = (sbyte)31,
                            FirstName = "Daniel",
                            LastName = "Persson"
                        },
                        new
                        {
                            Id = new Guid("938019c3-a144-4ea3-b702-9af8d0655201"),
                            Age = (sbyte)33,
                            FirstName = "Rundberg",
                            LastName = "Rundbergsson"
                        });
                });

            modelBuilder.Entity("Mimbly.Domain.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("Char(255)")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime>("TokenSetAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("token_set_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("Token", "UserId");

                    b.ToTable("refresh_token");
                });

            modelBuilder.Entity("Mimbly.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("CHAR(255)")
                        .HasColumnName("password");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("user");
                });

            modelBuilder.Entity("Mimbly.Domain.Models.RefreshToken", b =>
                {
                    b.HasOne("Mimbly.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
