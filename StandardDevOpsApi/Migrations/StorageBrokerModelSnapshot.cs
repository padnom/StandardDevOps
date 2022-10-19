﻿// <auto-generated />
using System;
using StandardDevOpsApi.Brokers.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace StandardDevOpsApi.Migrations
{
    [DbContext(typeof(StorageBroker))]
    partial class StorageBrokerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StandardDevOpsApi.Models.LibraryAccounts.LibraryAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("LibraryAccounts");
                });

            modelBuilder.Entity("StandardDevOpsApi.Models.LibraryCards.LibraryCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LibraryAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LibraryAccountId");

                    b.ToTable("LibraryCards");
                });

            modelBuilder.Entity("StandardDevOpsApi.Models.Students.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StandardDevOpsApi.Models.LibraryAccounts.LibraryAccount", b =>
                {
                    b.HasOne("StandardDevOpsApi.Models.Students.Student", "Student")
                        .WithOne("LibraryAccount")
                        .HasForeignKey("StandardDevOpsApi.Models.LibraryAccounts.LibraryAccount", "StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StandardDevOpsApi.Models.LibraryCards.LibraryCard", b =>
                {
                    b.HasOne("StandardDevOpsApi.Models.LibraryAccounts.LibraryAccount", "LibraryAccount")
                        .WithMany("LibraryCards")
                        .HasForeignKey("LibraryAccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("LibraryAccount");
                });

            modelBuilder.Entity("StandardDevOpsApi.Models.LibraryAccounts.LibraryAccount", b =>
                {
                    b.Navigation("LibraryCards");
                });

            modelBuilder.Entity("StandardDevOpsApi.Models.Students.Student", b =>
                {
                    b.Navigation("LibraryAccount");
                });
#pragma warning restore 612, 618
        }
    }
}
