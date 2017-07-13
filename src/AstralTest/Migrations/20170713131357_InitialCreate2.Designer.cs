using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AstralTest.Domain.Context;

namespace AstralTest.Migrations
{
    [DbContext(typeof(AstralContext))]
    [Migration("20170713131357_InitialCreate2")]
    partial class InitialCreate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("AstralTest.Domain.Model.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<Guid?>("MasterId")
                        .HasColumnName("iduser");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.HasIndex("MasterId");

                    b.ToTable("notes");
                });

            modelBuilder.Entity("AstralTest.Domain.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("AstralTest.Domain.Model.Note", b =>
                {
                    b.HasOne("AstralTest.Domain.Model.User", "Master")
                        .WithMany("Notes")
                        .HasForeignKey("MasterId");
                });
        }
    }
}
