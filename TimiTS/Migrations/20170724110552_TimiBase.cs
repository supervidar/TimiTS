using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TimiTS.Migrations
{
    public partial class TimiBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    CId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CName = table.Column<string>(maxLength: 255, nullable: false),
                    CStreetAddress = table.Column<string>(maxLength: 255, nullable: true),
                    CPostalCode = table.Column<string>(maxLength: 255, nullable: false),
                    CPostalAddress = table.Column<string>(maxLength: 255, nullable: false),
                    CEmail = table.Column<string>(maxLength: 255, nullable: true),                    
                    COrgNr = table.Column<string>(maxLength: 255, nullable: true)                    
                   
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.CId);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackCategories",
                columns: table => new
                {
                    BCId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BCCategory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackCategories", x => x.BCId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    PId = table.Column<int>(nullable: false),
                    PName = table.Column<string>(maxLength: 255, nullable: true),
                    PStartDate = table.Column<string>(nullable: true),
                    PEndDate = table.Column<string>(nullable: true),
                    PEstimateMasonry = table.Column<double>(nullable: false),
                    PEstimateTile = table.Column<double>(nullable: false),
                    PEstimateStructural = table.Column<double>(nullable: false),
                    PEstimateExternal = table.Column<double>(nullable: false),
                    PEstimatePlating = table.Column<double>(nullable: false),
                    PEstimateStender = table.Column<double>(nullable: false),
                    PEstimateFinalWork = table.Column<double>(nullable: false),
                    PEstimateGarage = table.Column<double>(nullable: false),
                    PEstimateAssembly = table.Column<double>(nullable: false),
                    PEstimateOther = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.PId);
                });

            migrationBuilder.CreateTable(
                name: "WorkCategories",
                columns: table => new
                {
                    WCId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WCPerformed = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCategories", x => x.WCId);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    WTId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WTType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.WTId);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    BId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BUser = table.Column<string>(nullable: true),
                    BetaCategoryId = table.Column<int>(nullable: true),
                    BComment = table.Column<string>(nullable: true),
                    BRating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.BId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_FeedbackCategories_BetaCategoryId",
                        column: x => x.BetaCategoryId,
                        principalTable: "FeedbackCategories",
                        principalColumn: "BCId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkParticipations",
                columns: table => new
                {
                    WPId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),                    
                    DateTimeIn = table.Column<DateTime>(nullable: true),
                    DateTimeOut = table.Column<DateTime>(nullable: true),
                    Hours = table.Column<double>(nullable: false),
                    WPBreak = table.Column<double>(nullable: false),
                    Comment = table.Column<string>(maxLength: 255, nullable: true),
                    ContracterId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),                    
                    WorkCategoryId = table.Column<int>(nullable: true),
                    WorkTypeId = table.Column<int>(nullable: true)
                   
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkParticipations", x => x.WPId);
                    table.ForeignKey(
                        name: "FK_WorkParticipations_Contractors_ContracterId",
                        column: x => x.ContracterId,
                        principalTable: "Contractors",
                        principalColumn: "CId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkParticipations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "PId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkParticipations_WorkCategories_WorkCategoryId",
                        column: x => x.WorkCategoryId,
                        principalTable: "WorkCategories",
                        principalColumn: "WCId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkParticipations_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "WTId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_BetaCategoryId",
                table: "Feedbacks",
                column: "BetaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkParticipations_ContracterId",
                table: "WorkParticipations",
                column: "ContracterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkParticipations_ProjectId",
                table: "WorkParticipations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkParticipations_WorkCategoryId",
                table: "WorkParticipations",
                column: "WorkCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkParticipations_WorkTypeId",
                table: "WorkParticipations",
                column: "WorkTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "WorkParticipations");

            migrationBuilder.DropTable(
                name: "FeedbackCategories");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "WorkCategories");

            migrationBuilder.DropTable(
                name: "WorkTypes");
        }
    }
}
