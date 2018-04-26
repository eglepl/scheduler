using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class PatientaddedAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Files_FileID",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "FileID",
                table: "People",
                newName: "Patient_FileID");

            migrationBuilder.RenameIndex(
                name: "IX_People_FileID",
                table: "People",
                newName: "IX_People_Patient_FileID");

            migrationBuilder.AddColumn<int>(
                name: "FileID",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_FileID",
                table: "People",
                column: "FileID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Files_FileID",
                table: "People",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Files_Patient_FileID",
                table: "People",
                column: "Patient_FileID",
                principalTable: "Files",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Files_FileID",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Files_Patient_FileID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_FileID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "FileID",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "Patient_FileID",
                table: "People",
                newName: "FileID");

            migrationBuilder.RenameIndex(
                name: "IX_People_Patient_FileID",
                table: "People",
                newName: "IX_People_FileID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Files_FileID",
                table: "People",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
