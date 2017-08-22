using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstrelTestWithToken.Migrations
{
    public partial class FixLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfAction",
                table: "InfoAboutEnteredUsers");

            migrationBuilder.DropColumn(
                name: "NameOfController",
                table: "InfoAboutEnteredUsers");

            migrationBuilder.RenameColumn(
                name: "ParametrsToAction",
                table: "InfoAboutEnteredUsers",
                newName: "JsonParametrs");

            migrationBuilder.AddColumn<string>(
                name: "NameOfAction",
                table: "EnteredUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameOfController",
                table: "EnteredUsers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfAction",
                table: "EnteredUsers");

            migrationBuilder.DropColumn(
                name: "NameOfController",
                table: "EnteredUsers");

            migrationBuilder.RenameColumn(
                name: "JsonParametrs",
                table: "InfoAboutEnteredUsers",
                newName: "ParametrsToAction");

            migrationBuilder.AddColumn<string>(
                name: "NameOfAction",
                table: "InfoAboutEnteredUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameOfController",
                table: "InfoAboutEnteredUsers",
                nullable: false,
                defaultValue: "");
        }
    }
}
