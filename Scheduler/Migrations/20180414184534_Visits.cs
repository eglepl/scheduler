using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class Visits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorID = table.Column<int>(nullable: false),
                    DoctorNotes = table.Column<string>(nullable: true),
                    PatientID = table.Column<int>(nullable: false),
                    PatientNotes = table.Column<string>(nullable: true),
                    VisitDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Visits_People_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_People_PatientID",
                        column: x => x.PatientID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorID",
                table: "Visits",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientID",
                table: "Visits",
                column: "PatientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");
        }
    }
}
