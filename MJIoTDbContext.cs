using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MjIot.Storage.Models.EFCoreDb.Models;

namespace MjIot.Storage.Models.EFCoreDb
{
    public partial class MJIoTDbContext : DbContext
    {
        public virtual DbSet<Connections> Connections { get; set; }
        public virtual DbSet<DeviceProperties> DeviceProperties { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<DeviceTypes> DeviceTypes { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<PropertyTypes> PropertyTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            return configuration["MJIoTDbConnectionString"];
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connections>(entity =>
            {
                entity.HasIndex(e => e.ListenerDeviceId)
                    .HasName("IX_ListenerDevice_Id");

                entity.HasIndex(e => e.ListenerPropertyId)
                    .HasName("IX_ListenerProperty_Id");

                entity.HasIndex(e => e.SenderDeviceId)
                    .HasName("IX_SenderDevice_Id");

                entity.HasIndex(e => e.SenderPropertyId)
                    .HasName("IX_SenderProperty_Id");

                entity.Property(e => e.ListenerDeviceId).HasColumnName("ListenerDevice_Id");

                entity.Property(e => e.ListenerPropertyId).HasColumnName("ListenerProperty_Id");

                entity.Property(e => e.SenderDeviceId).HasColumnName("SenderDevice_Id");

                entity.Property(e => e.SenderPropertyId).HasColumnName("SenderProperty_Id");

                entity.HasOne(d => d.ListenerDevice)
                    .WithMany(p => p.ConnectionsListenerDevice)
                    .HasForeignKey(d => d.ListenerDeviceId)
                    .HasConstraintName("FK_dbo.Connections_dbo.Devices_ListenerDevice_Id");

                entity.HasOne(d => d.ListenerProperty)
                    .WithMany(p => p.ConnectionsListenerProperty)
                    .HasForeignKey(d => d.ListenerPropertyId)
                    .HasConstraintName("FK_dbo.Connections_dbo.PropertyTypes_ListenerProperty_Id");

                entity.HasOne(d => d.SenderDevice)
                    .WithMany(p => p.ConnectionsSenderDevice)
                    .HasForeignKey(d => d.SenderDeviceId)
                    .HasConstraintName("FK_dbo.Connections_dbo.Devices_SenderDevice_Id");

                entity.HasOne(d => d.SenderProperty)
                    .WithMany(p => p.ConnectionsSenderProperty)
                    .HasForeignKey(d => d.SenderPropertyId)
                    .HasConstraintName("FK_dbo.Connections_dbo.PropertyTypes_SenderProperty_Id");
            });

            modelBuilder.Entity<DeviceProperties>(entity =>
            {
                entity.HasIndex(e => e.DeviceId)
                    .HasName("IX_Device_Id");

                entity.HasIndex(e => e.PropertyTypeId)
                    .HasName("IX_PropertyType_Id");

                entity.Property(e => e.DeviceId).HasColumnName("Device_Id");

                entity.Property(e => e.PropertyTypeId).HasColumnName("PropertyType_Id");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.DeviceProperties)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_dbo.DeviceProperties_dbo.Devices_Device_Id");

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.DeviceProperties)
                    .HasForeignKey(d => d.PropertyTypeId)
                    .HasConstraintName("FK_dbo.DeviceProperties_dbo.PropertyTypes_PropertyType_Id");
            });

            modelBuilder.Entity<Devices>(entity =>
            {
                entity.HasIndex(e => e.DeviceTypeId)
                    .HasName("IX_DeviceType_Id");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_User_Id");

                entity.Property(e => e.DeviceTypeId).HasColumnName("DeviceType_Id");

                entity.Property(e => e.IoThubKey).HasColumnName("IoTHubKey");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.DeviceType)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.DeviceTypeId)
                    .HasConstraintName("FK_dbo.Devices_dbo.DeviceTypes_DeviceType_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.Devices_dbo.Users_User_Id");
            });

            modelBuilder.Entity<DeviceTypes>(entity =>
            {
                entity.HasIndex(e => e.BaseDeviceTypeId)
                    .HasName("IX_BaseDeviceType_Id");

                entity.Property(e => e.BaseDeviceTypeId).HasColumnName("BaseDeviceType_Id");

                entity.HasOne(d => d.BaseDeviceType)
                    .WithMany(p => p.InverseBaseDeviceType)
                    .HasForeignKey(d => d.BaseDeviceTypeId)
                    .HasConstraintName("FK_dbo.DeviceTypes_dbo.DeviceTypes_BaseDeviceType_Id");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<PropertyTypes>(entity =>
            {
                entity.HasIndex(e => e.DeviceTypeId)
                    .HasName("IX_DeviceType_Id");

                entity.Property(e => e.DeviceTypeId).HasColumnName("DeviceType_Id");

                entity.Property(e => e.Format).HasDefaultValueSql("((0))");

                entity.Property(e => e.Uiconfigurable).HasColumnName("UIConfigurable");

                entity.HasOne(d => d.DeviceType)
                    .WithMany(p => p.PropertyTypes)
                    .HasForeignKey(d => d.DeviceTypeId)
                    .HasConstraintName("FK_dbo.PropertyTypes_dbo.DeviceTypes_DeviceType_Id");
            });
        }
    }
}
