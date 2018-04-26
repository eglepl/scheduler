using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class VisitTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitScheduleTemplates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitScheduleTemplates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VisitScheduleTemplateTimes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    VisitScheduleTemplateID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitScheduleTemplateTimes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitScheduleTemplateTimes_VisitScheduleTemplates_VisitScheduleTemplateID",
                        column: x => x.VisitScheduleTemplateID,
                        principalTable: "VisitScheduleTemplates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitScheduleTemplateTimes_VisitScheduleTemplateID",
                table: "VisitScheduleTemplateTimes",
                column: "VisitScheduleTemplateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitScheduleTemplateTimes");

            migrationBuilder.DropTable(
                name: "VisitScheduleTemplates");
        }
    }
}
