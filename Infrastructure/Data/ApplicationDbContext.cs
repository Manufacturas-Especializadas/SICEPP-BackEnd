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

        public DbSet<EppDetail> EppDetails { get; set; }

        public DbSet<EppType> EppTypes { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<ReasonRequest> ReasonRequests { get; set; }

        public DbSet<PreviousCondition> PreviousConditions { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }


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

            });

            modelBuilder.Entity<EppDetail>(entity =>
            {
                entity.ToTable("EppDetail");

                entity.Property(e => e.RequestedQuantity)
                    .HasColumnName("requestedQuantity");

                entity.HasOne(d => d.Epp)
                    .WithMany(p => p.EppDetails)
                    .HasForeignKey(d => d.EppId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.EppType)
                    .WithMany(p => p.EppDetails)
                    .HasForeignKey(d => d.EppTypeId);

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.EppDetails)
                    .HasForeignKey(d => d.SizeId)
                    .IsRequired(false);
            });


            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnName("deliveryDate");

                entity.Property(e => e.AuthorizedBy)
                    .HasColumnName("authorizedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.LastDelivery)
                    .HasColumnName("lastDelivery");

                entity.Property(e => e.ReplacementPolicy)
                    .HasColumnName("replacementPolicy");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId");

                entity.Property(e => e.DeliveryConfirmation)
                    .HasColumnName("deliveryConfirmation");

                entity.Property(e => e.EppId)
                    .HasColumnName("eppId");

                entity.HasOne(d => d.ApplicationStatus)
                    .WithMany(a => a.Stores)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Epp)
                    .WithOne(e => e.Store)
                    .HasForeignKey<Store>(d => d.EppId);
            });

            modelBuilder.Entity<EppType>().ToTable("EppType");
            modelBuilder.Entity<Size>().ToTable("Size");
            modelBuilder.Entity<ReasonRequest>().ToTable("ReasonRequest");
            modelBuilder.Entity<PreviousCondition>().ToTable("PreviousCondition");
            modelBuilder.Entity<ApplicationStatus>().ToTable("ApplicationStatus");


            modelBuilder.Entity<EppType>().Property(e => e.NameType).HasColumnName("nameType");
            modelBuilder.Entity<Size>().Property(e => e.NameSize).HasColumnName("nameSize");
            modelBuilder.Entity<ReasonRequest>().Property(e => e.NameReason).HasColumnName("nameReason");
            modelBuilder.Entity<PreviousCondition>().Property(e => e.NameCondition).HasColumnName("nameCondition");
        }
    }
}