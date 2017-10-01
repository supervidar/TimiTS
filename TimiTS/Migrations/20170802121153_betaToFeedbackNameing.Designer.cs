using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TimiTS.Models;

namespace TimiTS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170802121153_betaToFeedbackNameing")]
    partial class betaToFeedbackNameing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TimiTS.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<string>("RoleDescription");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("TimiTS.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int>("EId");

                    b.Property<string>("EJobTitle")
                        .HasMaxLength(255);

                    b.Property<string>("EName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("EPostalAddress")
                        .HasMaxLength(255);

                    b.Property<string>("EPostalCode")
                        .HasMaxLength(255);

                    b.Property<string>("EStreetAddress")
                        .HasMaxLength(255);

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

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
                    b.Property<int>("FId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FCId1");

                    b.Property<string>("FComment");

                    b.Property<int>("FRating");

                    b.Property<string>("FUser");

                    b.Property<int?>("FeebbackCategoryId");

                    b.HasKey("FId");

                    b.HasIndex("FCId1");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("TimiTS.Models.FeedbackCategory", b =>
                {
                    b.Property<int>("FCId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FCCategory");

                    b.HasKey("FCId");

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

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<double>("WPBreak");

                    b.Property<int?>("WorkCategoryId");

                    b.Property<int?>("WorkTypeId");

                    b.HasKey("WPId");

                    b.HasIndex("ContracterId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("TimiTS.Models.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TimiTS.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TimiTS.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("TimiTS.Models.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimiTS.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimiTS.Models.Feedback", b =>
                {
                    b.HasOne("TimiTS.Models.FeedbackCategory", "FCId")
                        .WithMany()
                        .HasForeignKey("FCId1");
                });

            modelBuilder.Entity("TimiTS.Models.WorkParticipation", b =>
                {
                    b.HasOne("TimiTS.Models.Contractor", "CId")
                        .WithMany()
                        .HasForeignKey("ContracterId");

                    b.HasOne("TimiTS.Models.Project", "PId")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("TimiTS.Models.ApplicationUser", "Id")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

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
