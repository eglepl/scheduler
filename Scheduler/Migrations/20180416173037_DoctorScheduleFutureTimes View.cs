using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class DoctorScheduleFutureTimesView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW [dbo].[DoctorScheduleFutureTimes]
            AS
            SELECT DATETIMEFROMPARTS(DATEPART(YEAR, DS.VisitDate), DATEPART(MONTH, DS.VisitDate), DATEPART(DAY, DS.VisitDate), DATEPART(HOUR, TT.Start), DATEPART(MINUTE, TT.Start), 0, 0) AS VisitDateTime, 
              DS.DoctorID
            FROM dbo.VisitScheduleTemplates AS T 
            INNER JOIN dbo.VisitScheduleTemplateTimes AS TT ON T.ID = TT.VisitScheduleTemplateID 
            INNER JOIN dbo.DoctorSchedules AS DS ON T.ID = DS.VisitScheduleTemplateID
            WHERE (DS.VisitDate >= CONVERT(date, SYSDATETIME()));
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS [dbo].[DoctorScheduleFutureTimes];
            ");
        }
    }
}
