using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_iduser",
                table: "notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Testusers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Testusers",
                table: "Testusers",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_Testusers_iduser",
                table: "notes",
                column: "iduser",
                principalTable: "Testusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_Testusers_iduser",
                table: "notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Testusers",
                table: "Testusers");

            migrationBuilder.RenameTable(
                name: "Testusers",
                newName: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_iduser",
                table: "notes",
                column: "iduser",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
