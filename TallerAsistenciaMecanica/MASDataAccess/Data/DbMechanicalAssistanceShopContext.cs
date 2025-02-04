using System;
using System.Collections.Generic;
using MASDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MASDataAccess.Data;

public partial class DbMechanicalAssistanceShopContext : DbContext
{
    public DbMechanicalAssistanceShopContext()
    {
    }

    public DbMechanicalAssistanceShopContext(DbContextOptions<DbMechanicalAssistanceShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<RenderedService> RenderedServices { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceByRendServ> ServiceByRendServs { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:MyDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brand__3214EC073E9370EB");

            entity.ToTable("Brand");

            entity.HasIndex(e => e.Name, "UQ__Brand__737584F684D07188").IsUnique();

            entity.Property(e => e.DeletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Valid).HasDefaultValue(true);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Color__3214EC07A8CAD58D");

            entity.ToTable("Color");

            entity.HasIndex(e => e.Name, "UQ__Color__737584F6AA683CA6").IsUnique();

            entity.Property(e => e.DeletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Valid).HasDefaultValue(true);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Model__3214EC072AA615CB");

            entity.ToTable("Model");

            entity.HasIndex(e => new { e.BrandId, e.Name }, "UQ_Model").IsUnique();

            entity.Property(e => e.DeletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Valid).HasDefaultValue(true);

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Model_Brand");
        });

        modelBuilder.Entity<RenderedService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rendered__3214EC070E3C4A51");

            entity.ToTable("RenderedService");

            entity.Property(e => e.DeletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Valid).HasDefaultValue(true);

            entity.HasOne(d => d.Vehicle).WithMany(p => p.RenderedServices)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicle_RendServ");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC078424D323");

            entity.ToTable("Service");

            entity.HasIndex(e => e.Title, "UQ__Service__2CB664DC4C4BF000").IsUnique();

            entity.Property(e => e.DeletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Valid).HasDefaultValue(true);
        });

        modelBuilder.Entity<ServiceByRendServ>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceB__3214EC07B35E877A");

            entity.ToTable("ServiceByRendServ");

            entity.Property(e => e.Annotation)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Valid).HasDefaultValue(true);

            entity.HasOne(d => d.RenderedService).WithMany(p => p.ServiceByRendServs)
                .HasForeignKey(d => d.RenderedServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RenderedService_ServiceByRendServ");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceByRendServs)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_ServiceByRendServ");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicle__3214EC07DEC20C33");

            entity.ToTable("Vehicle");

            entity.HasIndex(e => e.LicensePlate, "UQ__Vehicle__026BC15C67D5A644").IsUnique();

            entity.HasIndex(e => e.ChassisNumber, "UQ__Vehicle__59753907711909FF").IsUnique();

            entity.Property(e => e.ChassisNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.EngineNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Valid).HasDefaultValue(true);

            entity.HasOne(d => d.Color).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Color_Vehicle");

            entity.HasOne(d => d.Model).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Model_Vehicle");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
