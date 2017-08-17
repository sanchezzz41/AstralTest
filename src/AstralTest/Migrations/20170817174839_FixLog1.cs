using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class FixLog1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoAboutAction_ActionsLogs_IdEnteredUser",
                table: "InfoAboutAction");

            migrationBuilder.DropColumn(
                name: "EnteredTime",
                table: "InfoAboutAction");

            migrationBuilder.RenameColumn(
                name: "IdEnteredUser",
                table: "InfoAboutAction",
                newName: "IdAction");

            migrationBuilder.RenameIndex(
                name: "IX_InfoAboutAction_IdEnteredUser",
                table: "InfoAboutAction",
                newName: "IX_InfoAboutAction_IdAction");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnteredTime",
                table: "ActionsLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_InfoAboutAction_ActionsLogs_IdAction",
                table: "InfoAboutAction",
                column: "IdAction",
                principalTable: "ActionsLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoAboutAction_ActionsLogs_IdAction",
                table: "InfoAboutAction");

            migrationBuilder.DropColumn(
                name: "EnteredTime",
                table: "ActionsLogs");

            migrationBuilder.RenameColumn(
                name: "IdAction",
                table: "InfoAboutAction",
                newName: "IdEnteredUser");

            migrationBuilder.RenameIndex(
                name: "IX_InfoAboutAction_IdAction",
                table: "InfoAboutAction",
                newName: "IX_InfoAboutAction_IdEnteredUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnteredTime",
                table: "InfoAboutAction",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_InfoAboutAction_ActionsLogs_IdEnteredUser",
                table: "InfoAboutAction",
                column: "IdEnteredUser",
                principalTable: "ActionsLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
