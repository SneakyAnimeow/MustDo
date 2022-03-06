using Microsoft.EntityFrameworkCore;

using static MustDo.Setup;

namespace MustDo.Data {
    public partial class MustDoContext : DbContext {
        public MustDoContext() {
        }

        public MustDoContext(DbContextOptions<MustDoContext> options)
            : base(options) {
        }

        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(
                    ($"Server={AppConfig.MssqlConfig.Server}{(AppConfig.MssqlConfig.UseDefaultPort ? "" : $",{AppConfig.MssqlConfig.Port}")};" +
                    $"Database={AppConfig.MssqlConfig.Database};" +
                    (AppConfig.MssqlConfig.IntegratedSecurity
                    ? $"Integrated Security={AppConfig.MssqlConfig.IntegratedSecurity};"
                    : $"User={AppConfig.MssqlConfig.User};" +
                      $"Password={AppConfig.MssqlConfig.Password};")
                    )
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Note>(entity => {
                entity.ToTable("Note");

                entity.Property(e => e.Content).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(32);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Note_User");
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("User");

                entity.HasIndex(e => e.Name, "IX_User")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(64);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
