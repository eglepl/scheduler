﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Scheduler.Data;
using Scheduler.Models.HealthCenter;
using System;

namespace Scheduler.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180411130843_Changed DoctorSchedule")]
    partial class ChangedDoctorSchedule
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Scheduler.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

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
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.DoctorSchedule", b =>
                {
                    b.Property<int>("DoctorID");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("date");

                    b.Property<int>("VisitScheduleTemplateID");

                    b.HasKey("DoctorID", "VisitDate");

                    b.HasIndex("VisitScheduleTemplateID");

                    b.ToTable("DoctorSchedules");
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.File", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content");

                    b.Property<string>("ContentDisposition")
                        .HasMaxLength(200);

                    b.Property<string>("ContentType")
                        .HasMaxLength(100);

                    b.Property<string>("FileName")
                        .HasMaxLength(255);

                    b.Property<long>("Length");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("City");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("EMail");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Notes");

                    b.Property<string>("PersonalCode");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Sex");

                    b.HasKey("ID");

                    b.ToTable("People");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.VisitScheduleTemplate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("VisitScheduleTemplates");
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.VisitScheduleTemplateTime", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Start");

                    b.Property<int>("VisitScheduleTemplateID");

                    b.HasKey("ID");

                    b.HasIndex("VisitScheduleTemplateID");

                    b.ToTable("VisitScheduleTemplateTimes");
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.Doctor", b =>
                {
                    b.HasBaseType("Scheduler.Models.HealthCenter.Person");

                    b.Property<string>("ApplicationUserID");

                    b.Property<int?>("FileID");

                    b.Property<string>("Position");

                    b.HasIndex("ApplicationUserID")
                        .IsUnique()
                        .HasFilter("[ApplicationUserID] IS NOT NULL");

                    b.HasIndex("FileID");

                    b.ToTable("Doctor");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.Patient", b =>
                {
                    b.HasBaseType("Scheduler.Models.HealthCenter.Person");

                    b.Property<string>("ApplicationUserID")
                        .HasColumnName("Patient_ApplicationUserID");

                    b.HasIndex("ApplicationUserID");

                    b.ToTable("Patient");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Scheduler.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Scheduler.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Scheduler.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Scheduler.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.DoctorSchedule", b =>
                {
                    b.HasOne("Scheduler.Models.HealthCenter.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Scheduler.Models.HealthCenter.VisitScheduleTemplate", "VisitScheduleTemplate")
                        .WithMany()
                        .HasForeignKey("VisitScheduleTemplateID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.VisitScheduleTemplateTime", b =>
                {
                    b.HasOne("Scheduler.Models.HealthCenter.VisitScheduleTemplate", "VisitScheduleTemplate")
                        .WithMany("Times")
                        .HasForeignKey("VisitScheduleTemplateID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.Doctor", b =>
                {
                    b.HasOne("Scheduler.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("Doctor")
                        .HasForeignKey("Scheduler.Models.HealthCenter.Doctor", "ApplicationUserID");

                    b.HasOne("Scheduler.Models.HealthCenter.File", "Avatar")
                        .WithMany()
                        .HasForeignKey("FileID");
                });

            modelBuilder.Entity("Scheduler.Models.HealthCenter.Patient", b =>
                {
                    b.HasOne("Scheduler.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Patients")
                        .HasForeignKey("ApplicationUserID");
                });
#pragma warning restore 612, 618
        }
    }
}
