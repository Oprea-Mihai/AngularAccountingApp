using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WEB_3_Accountant.Data.Entities
{
    public partial class AccountingContext : DbContext
    {

        public AccountingContext()
        {
        }

        public AccountingContext(DbContextOptions<AccountingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeTask> EmployeeTasks { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<Week> Weeks { get; set; } = null!;
        public virtual DbSet<WeeklyCost> WeeklyCosts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionStringFor = Environment.UserName;

                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString(connectionStringFor));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.HourlyRate).HasColumnType("money");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeTask>(entity =>
            {
                entity.ToTable("EmployeeTask");

                entity.Property(e => e.EmployeeTaskId).HasColumnName("EmployeeTaskID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.WeeklyCostId).HasColumnName("WeeklyCostID");

                entity.Property(e => e.WorkedHours)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTasks)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTask_Employee");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.EmployeeTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTask_Task");

                entity.HasOne(d => d.WeeklyCost)
                    .WithMany(p => p.EmployeeTasks)
                    .HasForeignKey(d => d.WeeklyCostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTask_WeeklyCost");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.CurrentBudget).HasColumnType("money");

                entity.Property(e => e.InitialBudget).HasColumnType("money");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.Income).HasColumnType("money");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Project");
            });

            modelBuilder.Entity<Week>(entity =>
            {
                entity.ToTable("Week");

                entity.Property(e => e.WeekId).HasColumnName("WeekID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TotalGrossAmount).HasColumnType("money");

                entity.Property(e => e.TotalNetAmount).HasColumnType("money");
            });

            modelBuilder.Entity<WeeklyCost>(entity =>
            {
                entity.ToTable("WeeklyCost");

                entity.Property(e => e.WeeklyCostId).HasColumnName("WeeklyCostID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.GrossAmmount).HasColumnType("money");

                entity.Property(e => e.WeekId).HasColumnName("WeekID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WeeklyCosts)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeeklyCost_Employee");

                entity.HasOne(d => d.Week)
                    .WithMany(p => p.WeeklyCosts)
                    .HasForeignKey(d => d.WeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeeklyCost_Week");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
