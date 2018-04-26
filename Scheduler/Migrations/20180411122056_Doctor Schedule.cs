using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class DoctorSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VisitDate = table.Column<DateTime>(type: "date", nullable: false),
                    VisitScheduleTemplateID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_VisitScheduleTemplates_VisitScheduleTemplateID",
                        column: x => x.VisitScheduleTemplateID,
                        principalTable: "VisitScheduleTemplates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_VisitDate",
                table: "DoctorSchedules",
                column: "VisitDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_VisitScheduleTemplateID",
                table: "DoctorSchedules",
                column: "VisitScheduleTemplateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSchedules");
        }
    }
}
