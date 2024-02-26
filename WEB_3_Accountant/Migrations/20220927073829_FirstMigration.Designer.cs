﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEB_3_Accountant.Data.Entities;

#nullable disable

namespace WEB_3_Accountant.Migrations
{
    [DbContext(typeof(AccountingContext))]
    [Migration("20220927073829_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TaxPercentage")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.EmployeeTask", b =>
                {
                    b.Property<int>("EmployeeTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeTaskID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeTaskId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<int>("TaskId")
                        .HasColumnType("int")
                        .HasColumnName("TaskID");

                    b.Property<int>("WeeklyCostId")
                        .HasColumnType("int")
                        .HasColumnName("WeeklyCostID");

                    b.Property<string>("WorkedHours")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("EmployeeTaskId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TaskId");

                    b.HasIndex("WeeklyCostId");

                    b.ToTable("EmployeeTask", (string)null);
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProjectID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"), 1L, 1);

                    b.Property<int>("CurrentBudget")
                        .HasColumnType("int");

                    b.Property<int>("InitialBudget")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ProjectId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TaskID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"), 1L, 1);

                    b.Property<decimal?>("Income")
                        .HasColumnType("money");

                    b.Property<bool>("IsSpecial")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectID");

                    b.HasKey("TaskId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Week", b =>
                {
                    b.Property<int>("WeekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("WeekID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WeekId"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<decimal>("TotalGrossAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalNetAmount")
                        .HasColumnType("money");

                    b.HasKey("WeekId");

                    b.ToTable("Week", (string)null);
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.WeeklyCost", b =>
                {
                    b.Property<int>("WeeklyCostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("WeeklyCostID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WeeklyCostId"), 1L, 1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<decimal>("GrossAmmount")
                        .HasColumnType("money");

                    b.Property<int>("TotalHours")
                        .HasColumnType("int");

                    b.Property<int>("WeekId")
                        .HasColumnType("int")
                        .HasColumnName("WeekID");

                    b.HasKey("WeeklyCostId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("WeekId");

                    b.ToTable("WeeklyCost", (string)null);
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.EmployeeTask", b =>
                {
                    b.HasOne("WEB_3_Accountant.Data.Entities.Employee", "Employee")
                        .WithMany("EmployeeTasks")
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("FK_EmployeeTask_Employee");

                    b.HasOne("WEB_3_Accountant.Data.Entities.Task", "Task")
                        .WithMany("EmployeeTasks")
                        .HasForeignKey("TaskId")
                        .IsRequired()
                        .HasConstraintName("FK_EmployeeTask_Task");

                    b.HasOne("WEB_3_Accountant.Data.Entities.WeeklyCost", "WeeklyCost")
                        .WithMany("EmployeeTasks")
                        .HasForeignKey("WeeklyCostId")
                        .IsRequired()
                        .HasConstraintName("FK_EmployeeTask_WeeklyCost");

                    b.Navigation("Employee");

                    b.Navigation("Task");

                    b.Navigation("WeeklyCost");
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Task", b =>
                {
                    b.HasOne("WEB_3_Accountant.Data.Entities.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .IsRequired()
                        .HasConstraintName("FK_Task_Project");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.WeeklyCost", b =>
                {
                    b.HasOne("WEB_3_Accountant.Data.Entities.Employee", "Employee")
                        .WithMany("WeeklyCosts")
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("FK_WeeklyCost_Employee");

                    b.HasOne("WEB_3_Accountant.Data.Entities.Week", "Week")
                        .WithMany("WeeklyCosts")
                        .HasForeignKey("WeekId")
                        .IsRequired()
                        .HasConstraintName("FK_WeeklyCost_Week");

                    b.Navigation("Employee");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Employee", b =>
                {
                    b.Navigation("EmployeeTasks");

                    b.Navigation("WeeklyCosts");
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Task", b =>
                {
                    b.Navigation("EmployeeTasks");
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.Week", b =>
                {
                    b.Navigation("WeeklyCosts");
                });

            modelBuilder.Entity("WEB_3_Accountant.Data.Entities.WeeklyCost", b =>
                {
                    b.Navigation("EmployeeTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
