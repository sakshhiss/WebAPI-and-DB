using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebApp.Models;

public partial class InsuranceDbContext : DbContext
{
    public InsuranceDbContext()
    {
    }

    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<PolicyDetail> PolicyDetails { get; set; }

    public virtual DbSet<UserLoginDetail> UserLoginDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LAPTOP-ARMAGEDD;Database=InsuranceDB;Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC27AE4B5676");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmpName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.EmployeeDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__EmployeeD__UserI__3A81B327");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PaymentD__9B556A58C69EA34B");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CardOwnerName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SecurityCode)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PaymentDe__UserI__403A8C7D");
        });

        modelBuilder.Entity<PolicyDetail>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__PolicyDe__2E133944D73DC324");

            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");
            entity.Property(e => e.CoverageDetails).HasColumnType("text");
            entity.Property(e => e.PolicyType)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PremiumAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.PolicyDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PolicyDet__UserI__3D5E1FD2");
        });

        modelBuilder.Entity<UserLoginDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserLogi__1788CCAC83E12DA0");

            entity.HasIndex(e => e.EmailId, "UQ__UserLogi__7ED91AEE6095B520").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.EmailId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EmailID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
