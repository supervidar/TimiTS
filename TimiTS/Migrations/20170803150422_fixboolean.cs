using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimiTS.Migrations
{
    public partial class fixboolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveSession",
                table: "WorkParticipations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActiveSession",
                table: "WorkParticipations",
                nullable: false,
                defaultValue: false);
        }
    }
}
