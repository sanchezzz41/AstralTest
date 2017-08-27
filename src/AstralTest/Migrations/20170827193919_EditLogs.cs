using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class EditLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoAboutAction");

            migrationBuilder.CreateTable(
                name: "ParametrsActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdAction = table.Column<Guid>(nullable: false),
                    JsonParametrs = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrsActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParametrsActions_ActionsLogs_IdAction",
                        column: x => x.IdAction,
                        principalTable: "ActionsLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParametrsActions_IdAction",
                table: "ParametrsActions",
                column: "IdAction",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParametrsActions");

            migrationBuilder.CreateTable(
                name: "InfoAboutAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdAction = table.Column<Guid>(nullable: false),
                    JsonParametrs = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoAboutAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoAboutAction_ActionsLogs_IdAction",
                        column: x => x.IdAction,
                        principalTable: "ActionsLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoAboutAction_IdAction",
                table: "InfoAboutAction",
                column: "IdAction");
        }
    }
}
