using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using Scheduler.Models.HealthCenter;

namespace Scheduler.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<DoctorSchedule>()
                .HasKey(d => new { d.DoctorID, d.VisitDate });

            builder.Entity<DoctorScheduleTime>()
                .HasKey(dst => new { dst.DoctorID, dst.VisitDateTime });
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        // Domain model
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<File> Files { get; set; }

        public DbSet<PatientBan> PatientBans { get; set; }

        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        public DbSet<DoctorScheduleTime> DoctorScheduleFutureTimes { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }


        public DbSet<VisitScheduleTemplate> VisitScheduleTemplates { get; set; }
        public DbSet<VisitScheduleTemplateTime> VisitScheduleTemplateTimes { get; set; }

        

    }
}
