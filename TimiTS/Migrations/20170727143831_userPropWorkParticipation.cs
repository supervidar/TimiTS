using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimiTS.Migrations
{
    public partial class userPropWorkParticipation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WorkParticipations",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkParticipations_UserId",
                table: "WorkParticipations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkParticipations_AspNetUsers_UserId",
                table: "WorkParticipations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkParticipations_AspNetUsers_UserId",
                table: "WorkParticipations");

            migrationBuilder.DropIndex(
                name: "IX_WorkParticipations_UserId",
                table: "WorkParticipations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkParticipations");
        }
    }
}
