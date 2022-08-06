using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DBfirst_2106.Models;

namespace DBfirst_2106.Data
{
    public partial class DbfirstContext : DbContext
    {
        public DbfirstContext()
        {
        }

        public DbfirstContext(DbContextOptions<DbfirstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Professor> Professors { get; set; } = null!;
        public virtual DbSet<StuEnrollCourse> StuEnrollCourses { get; set; } = null!;
        public virtual DbSet<Student1> Student1s { get; set; } = null!;
        public virtual DbSet<TauCourse> TauCourses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\Local; Database = University;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StuEnrollCourse>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StuEnrollCourses)
                    .HasForeignKey(d => d.Courseid)
                    .HasConstraintName("FK_StuEnrollCourses_Courses");

                entity.HasOne(d => d.SIdNavigation)
                    .WithMany(p => p.StuEnrollCourses)
                    .HasForeignKey(d => d.SId)
                    .HasConstraintName("FK_StuEnrollCourses_Student1");
            });

            modelBuilder.Entity<TauCourse>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TauCourses)
                    .HasForeignKey(d => d.Courseid)
                    .HasConstraintName("FK_TauCourses_Courses");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.TauCourses)
                    .HasForeignKey(d => d.PId)
                    .HasConstraintName("FK_TauCourses_Professor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
