using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SoftwareApplication.EfCore.Api.Models
{
    public partial class SoftwareApplicationsContext : DbContext
    {
        public SoftwareApplicationsContext(DbContextOptions<SoftwareApplicationsContext> options):base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gsr>(entity =>
            {
                entity.Property(e => e.GsrRef)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.SotwareAutomation)
                    .WithMany(p => p.Gsr)
                    .HasForeignKey(d => d.SotwareAutomationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Gsr_SoftwareAutomation");
            });

            modelBuilder.Entity<PackageDetails>(entity =>
            {
                entity.Property(e => e.LicenceType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageDetails)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PackageDetails_Packages");
            });

            modelBuilder.Entity<Packages>(entity =>
            {
                entity.Property(e => e.PackageIdentifier)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PackageManufacturer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PackageVersion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SoftwareAutomation>(entity =>
            {
                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.SoftwareAutomation)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SoftwareAutomation_Packages");
            });
        }

        public virtual DbSet<Gsr> Gsr { get; set; }
        public virtual DbSet<PackageDetails> PackageDetails { get; set; }
        public virtual DbSet<Packages> Packages { get; set; }
        public virtual DbSet<SoftwareAutomation> SoftwareAutomation { get; set; }
        public virtual DbSet<BroadSearch> BroadSearch { get; set; }
    }
}