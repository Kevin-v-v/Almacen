using System;
using System.Collections.Generic;
using Almacen.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Almacen.Data;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Restock> Restocks { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:dev");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__category__CD54BC5AF4F65418");
            
            entity.ToTable("category");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.CantidadSurtir).HasColumnName("cantidad_surtir");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PuntoSurtir).HasColumnName("punto_surtir");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__client__677F38F51C7F8D99");

            entity.ToTable("client");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__product__FF341C0D4D3B10DC");

            entity.ToTable("product");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.CantidadStock).HasColumnName("cantidad_stock");
            entity.Property(e => e.Categoria).HasColumnName("categoria");
            entity.Property(e => e.Descripción)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripción");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioCompra).HasColumnName("precio_compra");
            entity.Property(e => e.PrecioVenta).HasColumnName("precio_venta");

            entity.HasOne(d => d.CategoriaNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoria)
                .HasConstraintName("FK__product__categor__398D8EEE");
        });

        modelBuilder.Entity<Restock>(entity =>
        {
            entity.HasKey(e => e.IdResurtido).HasName("PK__restock__5BA21AAFFE379601");

            entity.ToTable("restock");

            entity.Property(e => e.IdResurtido).HasColumnName("id_resurtido");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Restocks)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__restock__id_prod__3F466844");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__sale__459533BF2D81D3C3");

            entity.ToTable("sale");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.PrecioVenta).HasColumnName("precio_venta");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sale__id_cliente__4316F928");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__sale__id_product__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
