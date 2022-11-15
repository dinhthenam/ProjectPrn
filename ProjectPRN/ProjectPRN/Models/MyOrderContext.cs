using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace ProjectPRN.Models
{
    public partial class MyOrderContext : DbContext
    {
        public MyOrderContext()
        {
        }

        public MyOrderContext(DbContextOptions<MyOrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblChiTietHd> TblChiTietHds { get; set; } = null!;
        public virtual DbSet<TblHoadon> TblHoadons { get; set; } = null!;
        public virtual DbSet<TblKhachHang> TblKhachHangs { get; set; } = null!;
        public virtual DbSet<TblMatHang> TblMatHangs { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblChiTietHd>(entity =>
            {
                entity.HasKey(e => e.MaChiTietHd);

                entity.ToTable("tblChiTietHD");

                entity.Property(e => e.MaChiTietHd)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("MaChiTietHD");

                entity.Property(e => e.MaHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaHd)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("MaHD");

                entity.HasOne(d => d.MaHangNavigation)
                    .WithMany(p => p.TblChiTietHds)
                    .HasForeignKey(d => d.MaHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblChiTietHD_tblMatHang");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.TblChiTietHds)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblChiTietHD_tblHoadon");
            });

            modelBuilder.Entity<TblHoadon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("tblHoadon");

                entity.Property(e => e.MaHd)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("MaHD");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");

                entity.Property(e => e.NgayHd)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("NgayHD");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.TblHoadons)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblHoadon_tblKhachHang");
            });

            modelBuilder.Entity<TblKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("tblKhachHang");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");

                entity.Property(e => e.Diachi).HasMaxLength(50);

                entity.Property(e => e.Gt).HasColumnName("GT");

                entity.Property(e => e.NgaySinh).HasColumnType("smalldatetime");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<TblMatHang>(entity =>
            {
                entity.HasKey(e => e.MaHang);

                entity.ToTable("tblMatHang");

                entity.Property(e => e.MaHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dvt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DVT");

                entity.Property(e => e.TenHang).HasMaxLength(50);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("tblUser");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
