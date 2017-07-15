using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class InitalCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_iduser",
                table: "notes");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "notes",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "iduser",
                table: "notes",
                newName: "MasterId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "notes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_notes_iduser",
                table: "notes",
                newName: "IX_notes_MasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_MasterId",
                table: "notes",
                column: "MasterId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_MasterId",
                table: "notes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "notes",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "MasterId",
                table: "notes",
                newName: "iduser");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "notes",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_notes_MasterId",
                table: "notes",
                newName: "IX_notes_iduser");

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
