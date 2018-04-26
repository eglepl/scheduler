using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class TimeSpantoDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Duration",
                table: "VisitScheduleTemplateTimes",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "VisitScheduleTemplateTimes",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
