﻿// <auto-generated />
using System;
using IATechProcessoSeletivoCadastroPessoas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IATechProcessoSeletivoCadastroPessoas.Migrations
{
    [DbContext(typeof(PeopleRegistrationDBContext))]
    [Migration("20230607193410_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IATechProcessoSeletivoCadastroPessoas.Models.PersonModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("People");
                });

            modelBuilder.Entity("IATechProcessoSeletivoCadastroPessoas.Models.PhoneModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Number")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PersonModelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PersonModelId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("IATechProcessoSeletivoCadastroPessoas.Models.PhoneModel", b =>
                {
                    b.HasOne("IATechProcessoSeletivoCadastroPessoas.Models.PersonModel", null)
                        .WithMany("Phones")
                        .HasForeignKey("PersonModelId");
                });

            modelBuilder.Entity("IATechProcessoSeletivoCadastroPessoas.Models.PersonModel", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}
