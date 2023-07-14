using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.InfoModels;

public partial class VehiclesDbContext : DbContext
{
    public VehiclesDbContext()
    {
    }

    public VehiclesDbContext(DbContextOptions<VehiclesDbContext> options)
        : base(options)
    {
    }

    public DbSet<AgriculturalMachineryInfo> AgriculturalMachineryInfo { get; set; }

    public DbSet<BusesInfo> BusesInfo { get; set; }

    public DbSet<CarsInfo> CarsInfo { get; set; }

    public DbSet<LorriesInfo> LorriesInfo { get; set; }

    public DbSet<MotorbikesInfo> MotorbikesInfo { get; set; }

    public DbSet<ScootersInfo> ScootersInfo { get; set; }

    public DbSet<SnowmobilesInfo> SnowmobilesInfo { get; set; }

    public DbSet<TrailersInfo> TrailersInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgriculturalMachineryInfo>(entity =>
        {
            entity.HasKey(e => new { e.Brand, e.Model }).HasName("AgriculturalMachineryInfo_pkey");

            entity.ToTable("AgriculturalMachineryInfo");
        });

        modelBuilder.Entity<BusesInfo>(entity =>
        {
            entity.HasKey(e => new { e.Brand, e.Model });

            entity.ToTable("BusesInfo");
        });

        modelBuilder.Entity<CarsInfo>(entity =>
        {
            entity.HasKey(e => new { e.Brand, e.Model }).HasName("PK_Brand_Model");

            entity.ToTable("CarsInfo");
        });

        modelBuilder.Entity<LorriesInfo>(entity =>
        {
            entity.HasKey(e => new { e.Brand, e.Model }).HasName("LorriesInfo_pkey");

            entity.ToTable("LorriesInfo");
        });

        modelBuilder.Entity<MotorbikesInfo>(entity =>
        {
            entity.HasKey(e => new { e.Brand, e.Model }).HasName("PK_Brand_Model_MotorbikesInfo");

            entity.ToTable("MotorbikesInfo");
        });

        modelBuilder.Entity<ScootersInfo>(entity =>
        {
            entity.HasKey(e => new { e.Brand, e.Model }).HasName("PK_SccotersInfo");

            entity.ToTable("ScootersInfo");
        });

        modelBuilder.Entity<SnowmobilesInfo>(entity =>
        {
            entity.HasKey(e => new { e.Brand, e.Model }).HasName("SnowmobilesInfo_pkey");

            entity.ToTable("SnowmobilesInfo");
        });

        modelBuilder.Entity<TrailersInfo>(entity =>
        {
            entity.HasKey(e => new { e.Brand, e.Model });

            entity.ToTable("TrailersInfo");
        });
        base.OnModelCreating(modelBuilder);
    }
}
