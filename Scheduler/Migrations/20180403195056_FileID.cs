using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class FileID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Files_FileID",
                table: "People");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Files_FileID",
                table: "People",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Files_FileID",
                table: "People");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Files_FileID",
                table: "People",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
