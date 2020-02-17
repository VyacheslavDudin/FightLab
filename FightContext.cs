using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication2
{
    public partial class FightContext : DbContext
    {
        public FightContext()
        {
        }

        public FightContext(DbContextOptions<FightContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Fight> Fight { get; set; }
        public virtual DbSet<Fighter> Fighter { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<TitleHolders> TitleHolders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UM1S0IF;Database=Fight; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(40);
            });

            modelBuilder.Entity<Fight>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Fighter1Id).HasColumnName("Fighter1ID");

                entity.Property(e => e.Fighter2Id).HasColumnName("Fighter2ID");

                entity.Property(e => e.TypeOfWin)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Winner)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Fighter1)
                    .WithMany(p => p.FightFighter1)
                    .HasForeignKey(d => d.Fighter1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fight_Fighter");

                entity.HasOne(d => d.Fighter2)
                    .WithMany(p => p.FightFighter2)
                    .HasForeignKey(d => d.Fighter2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fight_Fighter1");
            });

            modelBuilder.Entity<Fighter>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Debut).HasColumnType("date");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Fighter)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fighter_Country");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Fighter)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fighter_Division");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Fighter)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fighter_Status");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<TitleHolders>(entity =>
            {
                entity.HasKey(e => e.TitleId)
                    .HasName("PK_TitleHolders_1");

                entity.Property(e => e.TitleId)
                    .HasColumnName("TitleID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOfGettingTitle).HasColumnType("datetime");

                entity.Property(e => e.FighterId).HasColumnName("FighterID");

                entity.HasOne(d => d.Fighter)
                    .WithMany(p => p.TitleHolders)
                    .HasForeignKey(d => d.FighterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TitleHolders_Fighter");

                entity.HasOne(d => d.Title)
                    .WithOne(p => p.TitleHolders)
                    .HasForeignKey<TitleHolders>(d => d.TitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TitleHolders_Title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
