using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Migrations
{
    public partial class IsDisabledaddedtodoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "People");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "People",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "People");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "People",
                nullable: false,
                defaultValue: false);
        }
    }
}
