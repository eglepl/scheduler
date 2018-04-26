using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class FileDoctorRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_FileID",
                table: "People");

            migrationBuilder.CreateIndex(
                name: "IX_People_FileID",
                table: "People",
                column: "FileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_FileID",
                table: "People");

            migrationBuilder.CreateIndex(
                name: "IX_People_FileID",
                table: "People",
                column: "FileID",
                unique: true);
        }
    }
}
