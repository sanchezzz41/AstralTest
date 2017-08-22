using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstrelTestWithToken.Migrations
{
    public partial class MinifixLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnteredUsers_Users_IdUser",
                table: "EnteredUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoAboutEnteredUsers_EnteredUsers_IdEnteredUser",
                table: "InfoAboutEnteredUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InfoAboutEnteredUsers",
                table: "InfoAboutEnteredUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnteredUsers",
                table: "EnteredUsers");

            migrationBuilder.RenameTable(
                name: "InfoAboutEnteredUsers",
                newName: "InfoAboutAction");

            migrationBuilder.RenameTable(
                name: "EnteredUsers",
                newName: "ActionsLogs");

            migrationBuilder.RenameIndex(
                name: "IX_InfoAboutEnteredUsers_IdEnteredUser",
                table: "InfoAboutAction",
                newName: "IX_InfoAboutAction_IdEnteredUser");

            migrationBuilder.RenameIndex(
                name: "IX_EnteredUsers_IdUser",
                table: "ActionsLogs",
                newName: "IX_ActionsLogs_IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfoAboutAction",
                table: "InfoAboutAction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActionsLogs",
                table: "ActionsLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsLogs_Users_IdUser",
                table: "ActionsLogs",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoAboutAction_ActionsLogs_IdEnteredUser",
                table: "InfoAboutAction",
                column: "IdEnteredUser",
                principalTable: "ActionsLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionsLogs_Users_IdUser",
                table: "ActionsLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoAboutAction_ActionsLogs_IdEnteredUser",
                table: "InfoAboutAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InfoAboutAction",
                table: "InfoAboutAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActionsLogs",
                table: "ActionsLogs");

            migrationBuilder.RenameTable(
                name: "InfoAboutAction",
                newName: "InfoAboutEnteredUsers");

            migrationBuilder.RenameTable(
                name: "ActionsLogs",
                newName: "EnteredUsers");

            migrationBuilder.RenameIndex(
                name: "IX_InfoAboutAction_IdEnteredUser",
                table: "InfoAboutEnteredUsers",
                newName: "IX_InfoAboutEnteredUsers_IdEnteredUser");

            migrationBuilder.RenameIndex(
                name: "IX_ActionsLogs_IdUser",
                table: "EnteredUsers",
                newName: "IX_EnteredUsers_IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfoAboutEnteredUsers",
                table: "InfoAboutEnteredUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnteredUsers",
                table: "EnteredUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnteredUsers_Users_IdUser",
                table: "EnteredUsers",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoAboutEnteredUsers_EnteredUsers_IdEnteredUser",
                table: "InfoAboutEnteredUsers",
                column: "IdEnteredUser",
                principalTable: "EnteredUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
