using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class ChangedDoctorSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSchedules",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_VisitDate",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "DoctorSchedules");

            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "DoctorSchedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSchedules",
                table: "DoctorSchedules",
                columns: new[] { "DoctorID", "VisitDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_People_DoctorID",
                table: "DoctorSchedules",
                column: "DoctorID",
                principalTable: "People",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_People_DoctorID",
                table: "DoctorSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSchedules",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "DoctorSchedules");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "DoctorSchedules",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSchedules",
                table: "DoctorSchedules",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_VisitDate",
                table: "DoctorSchedules",
                column: "VisitDate",
                unique: true);
        }
    }
}
