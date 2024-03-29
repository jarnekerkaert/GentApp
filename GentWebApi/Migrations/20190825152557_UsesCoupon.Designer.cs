﻿// <auto-generated />
using System;
using GentWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GentWebApi.Migrations
{
    [DbContext(typeof(GentDbContext))]
    [Migration("20190825152557_UsesCoupon")]
    partial class UsesCoupon
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GentApp.Models.Branch", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("CompanyId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OpeningHours");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("GentApp.Models.Company", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<string>("OpeningHours");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("GentApp.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyId");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GentWebApi.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BranchId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("GentWebApi.Models.Promotion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllBranches");

                    b.Property<string>("BranchId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.Property<bool>("UsesCoupon");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("GentWebApi.Models.Subscription", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountEvents");

                    b.Property<int>("AmountPromotions");

                    b.Property<string>("BranchId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("GentApp.Models.Branch", b =>
                {
                    b.HasOne("GentApp.Models.Company", "Company")
                        .WithMany("Branches")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("GentApp.Models.User", b =>
                {
                    b.HasOne("GentApp.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("GentWebApi.Models.Event", b =>
                {
                    b.HasOne("GentApp.Models.Branch", "Branch")
                        .WithMany("Events")
                        .HasForeignKey("BranchId");
                });

            modelBuilder.Entity("GentWebApi.Models.Promotion", b =>
                {
                    b.HasOne("GentApp.Models.Branch", "Branch")
                        .WithMany("Promotions")
                        .HasForeignKey("BranchId");
                });

            modelBuilder.Entity("GentWebApi.Models.Subscription", b =>
                {
                    b.HasOne("GentApp.Models.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId");
                });
#pragma warning restore 612, 618
        }
    }
}
