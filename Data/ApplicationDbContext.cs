using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BookEater.Models;

namespace BookEater.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyUser, MyRol, string>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<LibraryEntry> UserBooks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ClubPick> ClubPicks { get; set; }
        public DbSet<ClubComment> ClubComments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LibraryEntry>()
                .HasKey(ub => new { ub.UserId, ub.BookId });

            modelBuilder.Entity<LibraryEntry>()
                .HasOne(ub => ub.User)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<LibraryEntry>()
                .HasOne(ub => ub.Book)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.BookId);
        }
    }
}