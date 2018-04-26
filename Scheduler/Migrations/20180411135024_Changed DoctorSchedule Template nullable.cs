using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class ChangedDoctorScheduleTemplatenullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_VisitScheduleTemplates_VisitScheduleTemplateID",
                table: "DoctorSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "VisitScheduleTemplateID",
                table: "DoctorSchedules",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_VisitScheduleTemplates_VisitScheduleTemplateID",
                table: "DoctorSchedules",
                column: "VisitScheduleTemplateID",
                principalTable: "VisitScheduleTemplates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_VisitScheduleTemplates_VisitScheduleTemplateID",
                table: "DoctorSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "VisitScheduleTemplateID",
                table: "DoctorSchedules",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_VisitScheduleTemplates_VisitScheduleTemplateID",
                table: "DoctorSchedules",
                column: "VisitScheduleTemplateID",
                principalTable: "VisitScheduleTemplates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
