using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class AddedChatMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserIDFrom = table.Column<string>(nullable: true),
                    ApplicationUserIDTo = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_ApplicationUserIDFrom",
                        column: x => x.ApplicationUserIDFrom,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_ApplicationUserIDTo",
                        column: x => x.ApplicationUserIDTo,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ApplicationUserIDFrom",
                table: "ChatMessages",
                column: "ApplicationUserIDFrom");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ApplicationUserIDTo",
                table: "ChatMessages",
                column: "ApplicationUserIDTo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");
        }
    }
}
