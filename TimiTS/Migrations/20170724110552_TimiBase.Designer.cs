using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TimiTS.Models;

namespace TimiTS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170724110552_TimiBase")]
    partial class TimiBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimiTS.Models.Contractor", b =>
                {
                    b.Property<int>("CId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CEmail")
                        .HasMaxLength(255);

                    b.Property<string>("CName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("COrgNr")
                        .HasMaxLength(255);

                    b.Property<string>("CPostalAddress")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CPostalCode")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CStreetAddress")
                        .HasMaxLength(255);

                    b.HasKey("CId");

                    b.ToTable("Contractors");
                });

            modelBuilder.Entity("TimiTS.Models.Feedback", b =>
                {
                    b.Property<int>("BId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BComment");

                    b.Property<int>("BRating");

                    b.Property<string>("BUser");

                    b.Property<int?>("BetaCategoryId");

                    b.HasKey("BId");

                    b.HasIndex("BetaCategoryId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("TimiTS.Models.FeedbackCategory", b =>
                {
                    b.Property<int>("BCId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BCCategory");

                    b.HasKey("BCId");

                    b.ToTable("FeedbackCategories");
                });

            modelBuilder.Entity("TimiTS.Models.Project", b =>
                {
                    b.Property<int>("PId");

                    b.Property<string>("PEndDate");

                    b.Property<double>("PEstimateAssembly");

                    b.Property<double>("PEstimateExternal");

                    b.Property<double>("PEstimateFinalWork");

                    b.Property<double>("PEstimateGarage");

                    b.Property<double>("PEstimateMasonry");

                    b.Property<double>("PEstimateOther");

                    b.Property<double>("PEstimatePlating");

                    b.Property<double>("PEstimateStender");

                    b.Property<double>("PEstimateStructural");

                    b.Property<double>("PEstimateTile");

                    b.Property<string>("PName")
                        .HasMaxLength(255);

                    b.Property<string>("PStartDate");

                    b.HasKey("PId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TimiTS.Models.WorkCategory", b =>
                {
                    b.Property<int>("WCId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WCPerformed");

                    b.HasKey("WCId");

                    b.ToTable("WorkCategories");
                });

            modelBuilder.Entity("TimiTS.Models.WorkParticipation", b =>
                {
                    b.Property<int>("WPId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasMaxLength(255);

                    b.Property<int?>("ContracterId");

                    b.Property<DateTime?>("DateTimeIn");

                    b.Property<DateTime?>("DateTimeOut");

                    b.Property<double>("Hours");

                    b.Property<int?>("ProjectId");

                    b.Property<double>("WPBreak");

                    b.Property<int?>("WorkCategoryId");

                    b.Property<int?>("WorkTypeId");

                    b.HasKey("WPId");

                    b.HasIndex("ContracterId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("WorkCategoryId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("WorkParticipations");
                });

            modelBuilder.Entity("TimiTS.Models.WorkType", b =>
                {
                    b.Property<int>("WTId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WTType");

                    b.HasKey("WTId");

                    b.ToTable("WorkTypes");
                });

            modelBuilder.Entity("TimiTS.Models.Feedback", b =>
                {
                    b.HasOne("TimiTS.Models.FeedbackCategory", "BCId")
                        .WithMany()
                        .HasForeignKey("BetaCategoryId");
                });

            modelBuilder.Entity("TimiTS.Models.WorkParticipation", b =>
                {
                    b.HasOne("TimiTS.Models.Contractor", "CId")
                        .WithMany()
                        .HasForeignKey("ContracterId");

                    b.HasOne("TimiTS.Models.Project", "PId")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("TimiTS.Models.WorkCategory", "WCId")
                        .WithMany()
                        .HasForeignKey("WorkCategoryId");

                    b.HasOne("TimiTS.Models.WorkType", "WTId")
                        .WithMany()
                        .HasForeignKey("WorkTypeId");
                });
        }
    }
}
