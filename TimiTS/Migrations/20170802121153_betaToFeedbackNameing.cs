using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimiTS.Migrations
{
    public partial class betaToFeedbackNameing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FeedbackCategories_BetaCategoryId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_BetaCategoryId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "BCCategory",
                table: "FeedbackCategories",
                newName: "FCCategory");

            migrationBuilder.RenameColumn(
                name: "BCId",
                table: "FeedbackCategories",
                newName: "FCId");

            migrationBuilder.RenameColumn(
                name: "BetaCategoryId",
                table: "Feedbacks",
                newName: "FeebbackCategoryId");

            migrationBuilder.RenameColumn(
                name: "BUser",
                table: "Feedbacks",
                newName: "FUser");

            migrationBuilder.RenameColumn(
                name: "BRating",
                table: "Feedbacks",
                newName: "FRating");

            migrationBuilder.RenameColumn(
                name: "BComment",
                table: "Feedbacks",
                newName: "FComment");

            migrationBuilder.RenameColumn(
                name: "BId",
                table: "Feedbacks",
                newName: "FId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FCCategory",
                table: "FeedbackCategories",
                newName: "BCCategory");

            migrationBuilder.RenameColumn(
                name: "FCId",
                table: "FeedbackCategories",
                newName: "BCId");

            migrationBuilder.RenameColumn(
                name: "FeebbackCategoryId",
                table: "Feedbacks",
                newName: "BetaCategoryId");

            migrationBuilder.RenameColumn(
                name: "FUser",
                table: "Feedbacks",
                newName: "BUser");

            migrationBuilder.RenameColumn(
                name: "FRating",
                table: "Feedbacks",
                newName: "BRating");

            migrationBuilder.RenameColumn(
                name: "FComment",
                table: "Feedbacks",
                newName: "BComment");

            migrationBuilder.RenameColumn(
                name: "FId",
                table: "Feedbacks",
                newName: "BId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_BetaCategoryId",
                table: "Feedbacks",
                column: "BetaCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FeedbackCategories_BetaCategoryId",
                table: "Feedbacks",
                column: "BetaCategoryId",
                principalTable: "FeedbackCategories",
                principalColumn: "BCId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
