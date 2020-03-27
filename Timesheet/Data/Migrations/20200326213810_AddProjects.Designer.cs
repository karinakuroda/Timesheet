﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Timesheet.Data;

namespace Timesheet.Data.Migrations
{
    [DbContext(typeof(TimesheetContext))]
    [Migration("20200326213810_AddProjects")]
    partial class AddProjects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Timesheet.Domain.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("End");

                    b.Property<int?>("ProjectId");

                    b.Property<DateTime>("Start");

                    b.Property<Guid>("TimesheetId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TimesheetId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Timesheet.Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "New Timesheet for Partners",
                            Name = "Timesheet"
                        },
                        new
                        {
                            Id = 2,
                            Name = "DirReduce"
                        });
                });

            modelBuilder.Entity("Timesheet.Domain.Timesheet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Timesheets");
                });

            modelBuilder.Entity("Timesheet.Domain.Appointment", b =>
                {
                    b.HasOne("Timesheet.Domain.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("Timesheet.Domain.Timesheet")
                        .WithMany("Appointments")
                        .HasForeignKey("TimesheetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
