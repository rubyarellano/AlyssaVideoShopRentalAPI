using AlyssaVideoShopRentalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlyssaVideoShopRentalAPI.Data
{
    public class VideoShopDbContext : DbContext
    {
        public VideoShopDbContext(DbContextOptions<VideoShopDbContext> options) : base(options) { }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
        public DbSet<RentalHeader> RentalHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Title)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(m => m.Genre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(m => m.Director)
                    .IsRequired()
                    .HasMaxLength(100);

            });

            // Customer Configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.LastName);
                entity.Property(c => c.FirstName)
                   .IsRequired()
                   .HasMaxLength(100)
                   .UseCollation("Latin1_General_CI_AS");
                entity.Property(c => c.BirthDate)
                   .IsRequired();
                entity.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(c => c.Address)
                    .IsRequired()
                   .HasMaxLength(100)
                    .UseCollation(" Latin1_General_CI_AS");
                
            });

            // RentalHeader Configuration
            modelBuilder.Entity<RentalHeader>(entity =>
            {
                entity.HasKey(rh => rh.RentalHeaderId);
                entity.HasOne(rh => rh.Customer)
                    .WithMany(c => c.RentalHeaders)
                    .HasForeignKey(rh => rh.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(rh => rh.RentalDate)
                    .HasDefaultValueSql("GETDATE()");
            });

            // RentalDetail Configuration
            modelBuilder.Entity<RentalDetail>(entity =>
            {
                entity.HasKey(rd => rd.RentalDetailsId);
                entity.Property(rd => rd.RentalHeadersId)
                    .ValueGeneratedOnAdd();

                // Correct foreign key for RentalHeader
                entity.HasOne(rd => rd.RentalHeaders)
                    .WithMany(rh => rh.RentalDetails)
                    .HasForeignKey(rd => rd.RentalHeadersId)  // Changed to use RentalHeaderId
                    .OnDelete(DeleteBehavior.Cascade);

                // Correct foreign key for Movie
                entity.HasOne(rd => rd.Movies)
                    .WithMany()
                    .HasForeignKey(rd => rd.MovieId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}







