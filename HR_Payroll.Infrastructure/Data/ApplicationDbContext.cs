using Microsoft.EntityFrameworkCore;
using HR_Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PayrollRecord> PayrollRecords { get; set; }
        public DbSet<PayrollDeduction> PayrollDeductions { get; set; }
        public DbSet<PayrollBenefit> PayrollBenefits { get; set; }
        public DbSet<TimeOffRequest> TimeOffRequests { get; set; }
        public DbSet<TimeOffBalance> TimeOffBalances { get; set; }
        public DbSet<SalaryAdjustment> SalaryAdjustments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EmployeeNumber).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.EmployeeNumber).IsUnique();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.State).HasMaxLength(50);
                entity.Property(e => e.PostalCode).HasMaxLength(20);
                entity.Property(e => e.Country).HasMaxLength(50);
                entity.Property(e => e.EmergencyContactName).HasMaxLength(100);
                entity.Property(e => e.EmergencyContactPhone).HasMaxLength(20);
                entity.Property(e => e.Position).HasMaxLength(100);
                entity.Property(e => e.EmploymentStatus).HasMaxLength(50);
                entity.Property(e => e.CurrentSalary).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Description).HasMaxLength(500);
            });

            // TimeOffRequest configurations
            modelBuilder.Entity<TimeOffRequest>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.TimeOffType).IsRequired().HasMaxLength(50);
                entity.Property(t => t.Status).IsRequired().HasMaxLength(20);
                entity.Property(t => t.Reason).HasMaxLength(500);
                entity.Property(t => t.ApprovedBy).HasMaxLength(100);
                entity.Property(t => t.Comments).HasMaxLength(500);

                entity.HasOne(t => t.Employee)
                    .WithMany(e => e.TimeOffRequests)
                    .HasForeignKey(t => t.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TimeOffBalance configurations
            modelBuilder.Entity<TimeOffBalance>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.VacationDays).HasPrecision(5, 2);
                entity.Property(t => t.SickDays).HasPrecision(5, 2);
                entity.Property(t => t.PersonalDays).HasPrecision(5, 2);
                entity.Property(t => t.UnpaidLeavedays).HasPrecision(5, 2);  // Added this line

                entity.HasOne(t => t.Employee)
                    .WithOne(e => e.TimeOffBalance)
                    .HasForeignKey<TimeOffBalance>(t => t.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PayrollRecord configurations
            modelBuilder.Entity<PayrollRecord>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.GrossPay).HasPrecision(18, 2);
                entity.Property(p => p.NetPay).HasPrecision(18, 2);
                entity.Property(p => p.TaxWithheld).HasPrecision(18, 2);
                entity.Property(p => p.OtherDeductions).HasPrecision(18, 2);
                entity.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(50);
                entity.Property(p => p.PaymentStatus).IsRequired().HasMaxLength(20);

                entity.HasOne(p => p.Employee)
                    .WithMany(e => e.PayrollRecords)
                    .HasForeignKey(p => p.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PayrollDeduction configurations
            modelBuilder.Entity<PayrollDeduction>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.DeductionType).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Amount).HasPrecision(18, 2);
                entity.Property(p => p.Description).HasMaxLength(200);

                entity.HasOne(p => p.PayrollRecord)
                    .WithMany(r => r.Deductions)
                    .HasForeignKey(p => p.PayrollRecordId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PayrollBenefit configurations
            modelBuilder.Entity<PayrollBenefit>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.BenefitType).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Amount).HasPrecision(18, 2);
                entity.Property(p => p.Description).HasMaxLength(200);

                entity.HasOne(p => p.PayrollRecord)
                    .WithMany(r => r.Benefits)
                    .HasForeignKey(p => p.PayrollRecordId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // SalaryAdjustment configurations
            modelBuilder.Entity<SalaryAdjustment>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.PreviousSalary).HasPrecision(18, 2);
                entity.Property(s => s.NewSalary).HasPrecision(18, 2);
                entity.Property(s => s.AdjustmentAmount).HasPrecision(18, 2);
                entity.Property(s => s.AdjustmentType).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Reason).HasMaxLength(500);
                entity.Property(s => s.ApprovedBy).HasMaxLength(100);

                entity.HasOne(s => s.Employee)
                    .WithMany(e => e.SalaryAdjustments)
                    .HasForeignKey(s => s.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
