﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AstralTest.ContextDb;

namespace AstralTest.Migrations
{
    [DbContext(typeof(AstralContext))]
    partial class AstralContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("AstralTest.DataDb.Note", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid?>("MasterId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MasterId");

                    b.ToTable("notes");
                });

            modelBuilder.Entity("AstralTest.DataDb.User", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("AstralTest.DataDb.Note", b =>
                {
                    b.HasOne("AstralTest.DataDb.User", "Master")
                        .WithMany("Notes")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
