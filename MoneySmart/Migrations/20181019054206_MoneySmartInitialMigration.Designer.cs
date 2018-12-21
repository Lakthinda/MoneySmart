﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneySmart.API.Entities;

namespace MoneySmart.API.Migrations
{
    [DbContext(typeof(MoneySmartDbContext))]
    [Migration("20181019054206_MoneySmartInitialMigration")]
    partial class MoneySmartInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoneySmart.API.Entities.SavingAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("OnHold");

                    b.Property<double>("Percentage");

                    b.HasKey("Id");

                    b.ToTable("SavingAccounts");
                });

            modelBuilder.Entity("MoneySmart.API.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<double>("OriginalAmount");

                    b.Property<int>("SavingAcountId");

                    b.Property<int>("TransactionType");

                    b.HasKey("Id");

                    b.HasIndex("SavingAcountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("MoneySmart.API.Entities.Transaction", b =>
                {
                    b.HasOne("MoneySmart.API.Entities.SavingAccount", "SavingAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("SavingAcountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}