using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class Migr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_MasterId",
                table: "notes");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_MasterId",
                table: "notes",
                column: "MasterId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_MasterId",
                table: "notes");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_MasterId",
                table: "notes",
                column: "MasterId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
