using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class UpdatedPersonEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "People",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "LicenceNumber",
                table: "People",
                newName: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EMail",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "People");

            migrationBuilder.DropColumn(
                name: "City",
                table: "People");

            migrationBuilder.DropColumn(
                name: "EMail",
                table: "People");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "People",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "People",
                newName: "LicenceNumber");
        }
    }
}
