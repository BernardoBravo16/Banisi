using Banisi.Domain.Entities.Affiliations;
using Microsoft.EntityFrameworkCore;

namespace Banisi.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options)
        {
        }

        #region DbSet Properties
        public virtual DbSet<Affiliation> Affiliations { get; set; }
        #endregion

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        #region Entites configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Affiliation>(entity =>
            {
                entity.ToTable("Affiliation");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("Id");

                entity.Property(e => e.ClientId)
                    .HasColumnName("ClientId");

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountId");

                entity.Property(e => e.Otp)
                    .HasColumnName("Otp");

                entity.Property(e => e.Seed)
                    .HasColumnName("Seed");

                entity.Property(e => e.CognitoUsername)
                    .HasColumnName("CognitoUsername");

                entity.Property(e => e.CognitoPassword)
                    .HasColumnName("CognitoPassword");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CreateDate");
            });

        }
        #endregion
    }
}