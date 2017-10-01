using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimiTS.Migrations
{
    public partial class spellingErrorFeedbackFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FeedbackCategories_FCId1",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_FCId1",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "FCId1",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "FeebbackCategoryId",
                table: "Feedbacks",
                newName: "FeedbackCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FeedbackCategoryId",
                table: "Feedbacks",
                column: "FeedbackCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FeedbackCategories_FeedbackCategoryId",
                table: "Feedbacks",
                column: "FeedbackCategoryId",
                principalTable: "FeedbackCategories",
                principalColumn: "FCId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FeedbackCategories_FeedbackCategoryId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_FeedbackCategoryId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "FeedbackCategoryId",
                table: "Feedbacks",
                newName: "FeebbackCategoryId");

            migrationBuilder.AddColumn<int>(
                name: "FCId1",
                table: "Feedbacks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FCId1",
                table: "Feedbacks",
                column: "FCId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FeedbackCategories_FCId1",
                table: "Feedbacks",
                column: "FCId1",
                principalTable: "FeedbackCategories",
                principalColumn: "FCId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
