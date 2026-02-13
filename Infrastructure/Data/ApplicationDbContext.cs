using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Epp> Epps { get; set; }

        public DbSet<EppType> EppTypes { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<ReasonRequest> ReasonRequests { get; set; }

        public DbSet<PreviousCondition> PreviousConditions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Epp>(entity =>
            {
                entity.ToTable("Epp");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(60);
                entity.Property(e => e.Area).HasColumnName("area").HasMaxLength(30);
                entity.Property(e => e.Position).HasColumnName("position").HasMaxLength(70);
                entity.Property(e => e.Shift).HasColumnName("shift").HasMaxLength(40);

                entity.HasOne(d => d.EppType)
                    .WithMany(p => p.Epps)
                    .HasForeignKey(d => d.EppTypeId);
            });

            modelBuilder.Entity<EppType>().ToTable("EppType");
            modelBuilder.Entity<Size>().ToTable("Size");
            modelBuilder.Entity<ReasonRequest>().ToTable("ReasonRequest");
            modelBuilder.Entity<PreviousCondition>().ToTable("PreviousCondition");


            modelBuilder.Entity<EppType>().Property(e => e.NameType).HasColumnName("nameType");
            modelBuilder.Entity<Size>().Property(e => e.NameSize).HasColumnName("nameSize");
            modelBuilder.Entity<ReasonRequest>().Property(e => e.NameReason).HasColumnName("nameReason");
            modelBuilder.Entity<PreviousCondition>().Property(e => e.NameCondition).HasColumnName("nameCondition");
        }
    }
}