﻿
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Data
{
    public class ApplicationDbContext : IdentityDbContext<Student>
    {
        public DbSet<Exam> exams { get; set; }
        public DbSet<Question> questions
        { get; set; }
        public DbSet<Lecture> lectures { get; set; }
        public DbSet<Grade> grades { get; set; }

        public DbSet<Book> books { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<BookCart> bookCarts
        { get; set; }
        public DbSet<EnrollmentCode> enrollmentCodes { get; set; }
        public DbSet<EnrollmentCodeBook> enrollmentCodeBooks { get; set; }

        public DbSet<Contactus> contactus { get; set; }
       // public DbSet<StudentVM> studentVMs { get; set; } = default!;


        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Laptop>()
            //    .HasOne(l => l.brand)
            //    .WithMany()
            //    .HasForeignKey(l => l.BrandId);
            modelBuilder.Entity<Lecture>()
    .HasOne(p => p.grade)
    .WithMany(v => v.lectures)
    .HasForeignKey(p => p.GradeId).OnDelete(DeleteBehavior.Restrict)
   ; // or DeleteBehavior.NoAction




            modelBuilder.Entity<Exam>()
            .HasOne(e => e.Lecture)
            .WithOne(l => l.Exam)
            .HasForeignKey<Exam>(e => e.LectureId);

            base.OnModelCreating(modelBuilder);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(builder
                );
        }
        public DbSet<Educational_platform.ViewModel.UserRoleVM> UserRoleVM { get; set; } = default!;
      //  public DbSet<Educational_platform.ViewModel.BookVM> BookVM { get; set; } = default!;
      //  public DbSet<Educational_platform.ViewModel.LectureVM> LectureVM { get; set; } = default!;

      //  public DbSet<Educational_platform.ViewModel.LectureVM> LectureVM { get; set; } = default!;
     //   public DbSet<Educational_platform.ViewModel.BookVM> BookVM { get; set; } = default!;
      //  public DbSet<Educational_platform.ViewModel.GradeVM> GradeVM { get; set; } = default!;
      //  public DbSet<Educational_platform.ViewModel.UserLoginVM> UserLoginVM { get; set; } = default!;
    }
}
