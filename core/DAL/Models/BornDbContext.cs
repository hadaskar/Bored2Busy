using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;
using IDAL;

public partial class BornDbContext : DbContext
{
    public BornDbContext()
    {
    }

    public BornDbContext(DbContextOptions<BornDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS; Database=BORN DB; Trusted_Connection=True; MultipleActiveResultSets=True; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B34C75210");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF138181AFE");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.Task).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_Employee_Task");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D111030537");

            entity.Property(e => e.TaskId)
                .ValueGeneratedNever()
                .HasColumnName("TaskID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.TaskName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Task_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
