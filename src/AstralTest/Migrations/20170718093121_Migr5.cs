using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AstralTest.Migrations
{
    public partial class Migr5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_ApplicationRoleId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_ApplicationRoleId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "UserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "MasterId",
                table: "Notes",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRoles",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "UserRoles",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleId",
                table: "UserRoles",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Roles",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    IdentityRoleId = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_IdentityRoleId",
                        column: x => x.IdentityRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_IdentityRoleId",
                table: "UserRoles",
                column: "IdentityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_IdentityRoleId",
                table: "RoleClaims",
                column: "IdentityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_IdentityRoleId",
                table: "UserRoles",
                column: "IdentityRoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_IdentityRoleId",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_IdentityRoleId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "IdentityRoleId",
                table: "UserRoles");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterId",
                table: "Notes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserRoles",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "UserRoles",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationRoleId",
                table: "UserRoles",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Roles",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_ApplicationRoleId",
                table: "UserRoles",
                column: "ApplicationRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_ApplicationRoleId",
                table: "UserRoles",
                column: "ApplicationRoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
