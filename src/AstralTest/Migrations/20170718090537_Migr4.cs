using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class Migr4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_MasterId",
                table: "notes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notes",
                table: "notes");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "notes",
                newName: "Notes");

            migrationBuilder.RenameIndex(
                name: "IX_notes_MasterId",
                table: "Notes",
                newName: "IX_Notes_MasterId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_MasterId",
                table: "Notes",
                column: "MasterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_MasterId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "notes");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_MasterId",
                table: "notes",
                newName: "IX_notes_MasterId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notes",
                table: "notes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_MasterId",
                table: "notes",
                column: "MasterId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
