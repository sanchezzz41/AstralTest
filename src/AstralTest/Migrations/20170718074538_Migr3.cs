using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class Migr3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "UserName");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ApplicationRoleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_ApplicationRoleId",
                table: "UserRoles",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "users");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "users");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "Name");
        }
    }
}
