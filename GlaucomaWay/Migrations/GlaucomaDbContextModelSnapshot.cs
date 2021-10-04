﻿// <auto-generated />
using System;
using GlaucomaWay;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GlaucomaWay.Migrations
{
    [DbContext(typeof(GlaucomaDbContext))]
    partial class GlaucomaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GlaucomaWay.Models.Vf14ResultModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Q10Score")
                        .HasColumnType("int");

                    b.Property<int>("Q11Score")
                        .HasColumnType("int");

                    b.Property<int>("Q12Score")
                        .HasColumnType("int");

                    b.Property<int>("Q13Score")
                        .HasColumnType("int");

                    b.Property<int>("Q14Score")
                        .HasColumnType("int");

                    b.Property<int>("Q1Score")
                        .HasColumnType("int");

                    b.Property<int>("Q2Score")
                        .HasColumnType("int");

                    b.Property<int>("Q3Score")
                        .HasColumnType("int");

                    b.Property<int>("Q4Score")
                        .HasColumnType("int");

                    b.Property<int>("Q51Score")
                        .HasColumnType("int");

                    b.Property<int>("Q6Score")
                        .HasColumnType("int");

                    b.Property<int>("Q7Score")
                        .HasColumnType("int");

                    b.Property<int>("Q8Score")
                        .HasColumnType("int");

                    b.Property<int>("Q9Score")
                        .HasColumnType("int");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vf14Results");
                });
#pragma warning restore 612, 618
        }
    }
}
