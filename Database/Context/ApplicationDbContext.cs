using CenterEnglishManagement.Models;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.RelateTable;
using CenterEnglishManagement.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace CenterEnglishManagement.Context
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }
        /*public DbSet<YourEntity> YourEntities { get; set; }*/
        /*user*/
        public DbSet<User> Users { get; set; }
        // relative table
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<UserClass> UserClasses { get; set; }
        /*other*/
        public DbSet<Class> Classes { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<TuitionFee> TuitionFees { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentParent>()
                .HasOne(sp => sp.Student)
                .WithMany()
                .HasForeignKey(sp => sp.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentParent>()
                .HasOne(sp => sp.Parent)
                .WithMany()
                .HasForeignKey(sp => sp.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
            /*
                        modelBuilder.Entity<UserClass>()
                            .HasOne(sp => sp.User)
                            .WithMany()
                            .HasForeignKey(sp => sp.UserId)
                            .OnDelete(DeleteBehavior.Restrict);

                        modelBuilder.Entity<UserClass>()
                            .HasOne(sp => sp.Class)
                            .WithMany()
                            .HasForeignKey(sp => sp.ClassId)
                            .OnDelete(DeleteBehavior.Restrict);

                        modelBuilder.Entity<Class>()
                            .HasOne(sp => sp.Teacher)
                            .WithMany()
                            .HasForeignKey(sp => sp.TeacherId)
                            .OnDelete(DeleteBehavior.Restrict);*/
        }

    }
}
