using Microsoft.EntityFrameworkCore;
using RethinkFirstCodingChallenge.Data.Model;

namespace RethinkFirstCodingChallenge.Data
{
    public class RethinkFirstContext : DbContext
    {
        public DbSet<Client> Client { get; set; }

        public RethinkFirstContext(DbContextOptions<RethinkFirstContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                 .IsRequired()
                 .HasMaxLength(255)
                 .IsUnicode(false);

                entity.Property(e => e.LastName)
                 .IsRequired()
                 .HasMaxLength(255)
                 .IsUnicode(false);

                entity.Property(e => e.Birthdate)
                 .IsRequired()
                 .HasMaxLength(10)
                 .IsUnicode(false);

                entity.Property(e => e.Gender)
                 .IsRequired()
                 .HasMaxLength(1)
                 .IsUnicode(false);

                entity.Property(e => e.Removed);
            });
        }

    }
}
