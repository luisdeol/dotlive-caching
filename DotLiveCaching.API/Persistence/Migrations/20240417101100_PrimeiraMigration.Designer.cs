﻿// <auto-generated />
using DotLiveCaching.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotLiveCaching.API.Persistence.Migrations
{
    [DbContext(typeof(EcommerceDbContext))]
    [Migration("20240417101100_PrimeiraMigration")]
    partial class PrimeiraMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DotLiveCaching.API.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DotLiveCaching.API.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "BR",
                            Name = "São Paulo"
                        },
                        new
                        {
                            Id = 2,
                            Country = "BR",
                            Name = "Rio de Janeiro"
                        },
                        new
                        {
                            Id = 3,
                            Country = "BR",
                            Name = "Minas Gerais"
                        },
                        new
                        {
                            Id = 4,
                            Country = "BR",
                            Name = "Rio Grande do Sul"
                        },
                        new
                        {
                            Id = 5,
                            Country = "BR",
                            Name = "Fortaleza"
                        });
                });

            modelBuilder.Entity("DotLiveCaching.API.Entities.Product", b =>
                {
                    b.HasOne("DotLiveCaching.API.Entities.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });
#pragma warning restore 612, 618
        }
    }
}