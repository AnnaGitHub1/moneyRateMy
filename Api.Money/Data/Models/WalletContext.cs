using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Data.Models
{
    public partial class WalletContext : DbContext
    {
        public WalletContext()
        {
        }

        public WalletContext(DbContextOptions<WalletContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Money> Money { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CurrencyName).HasMaxLength(50);
            });

            modelBuilder.Entity<Money>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Sum).HasColumnType("decimal(19, 2)");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Money)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Money_To_Currencies");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Money)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Money_To_Statuses");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Fio).HasMaxLength(100);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(32)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
