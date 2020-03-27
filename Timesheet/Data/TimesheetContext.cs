namespace Timesheet.Data
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Timesheet.Domain;

    public class TimesheetContext : DbContext
    {
        public TimesheetContext(DbContextOptions<TimesheetContext> options)
            : base(options)
        {
        }

        public DbSet<Timesheet> Timesheets { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var projects = new List<Project>();

            projects.Add(new Project { Id = 1, Name = "Timesheet", Description = "New Timesheet for Partners"});
            projects.Add(new Project { Id = 2, Name = "DirReduce" });

            modelBuilder.Entity<Project>().HasData(projects.ToArray());

            base.OnModelCreating(modelBuilder);
        }
    }
}
