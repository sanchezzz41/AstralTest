using System;
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
