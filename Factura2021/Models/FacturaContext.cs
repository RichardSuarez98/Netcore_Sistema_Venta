using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Factura2021.Models
{
    public partial class FacturaContext : DbContext
    {
        public FacturaContext()
        {
        }

        public FacturaContext(DbContextOptions<FacturaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCategorium> TblCategoria { get; set; }
        public virtual DbSet<TblDetalle> TblDetalles { get; set; }
        public virtual DbSet<TblEstado> TblEstados { get; set; }
        public virtual DbSet<TblFactura> TblFacturas { get; set; }
        public virtual DbSet<TblInventario> TblInventarios { get; set; }
        public virtual DbSet<TblMarca> TblMarcas { get; set; }
        public virtual DbSet<TblPersona> TblPersonas { get; set; }
        public virtual DbSet<TblProducto> TblProductos { get; set; }
        public virtual DbSet<TblTipoPersona> TblTipoPersonas { get; set; }
        public virtual DbSet<TblUsuario> TblUsuarios { get; set; }
        public object TblProducto { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-0BRL7LM\\SQLEXPRESS; Initial Catalog=Factura; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<TblCategorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.ToTable("tbl_Categoria");

                entity.Property(e => e.DescripcionCategoria)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCategoria)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblCategoria)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_Table_EstadoCategoria");
            });

            modelBuilder.Entity<TblDetalle>(entity =>
            {
                entity.HasKey(e => e.IdDetalle);

                entity.ToTable("tbl_Detalle");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblDetalles)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Estado_Detalle");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.TblDetalles)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Factura_Detalle");

                entity.HasOne(d => d.IdInventarioNavigation)
                    .WithMany(p => p.TblDetalles)
                    .HasForeignKey(d => d.IdInventario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Inventario_Detalle");
            });

            modelBuilder.Entity<TblEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("tbl_Estado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreEstado)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFactura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.ToTable("tbl_Factura");

                entity.Property(e => e.FechaEmision).HasColumnType("date");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblFacturas)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_EstadoFactura");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TblFacturas)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Persona_Factura");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TblFacturas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Usuario_Factura");
            });

            modelBuilder.Entity<TblInventario>(entity =>
            {
                entity.HasKey(e => e.IdInventario);

                entity.ToTable("tbl_Inventario");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblInventarios)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_1_EstadoInventario");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TblInventarios)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_ProductoInventario");
            });

            modelBuilder.Entity<TblMarca>(entity =>
            {
                entity.HasKey(e => e.IdMarca);

                entity.ToTable("tbl_Marca");

                entity.Property(e => e.DescripcionMarca)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NombreMarca)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblMarcas)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_Table_EstadoMarca");
            });

            modelBuilder.Entity<TblPersona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("tbl_Persona");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePersona)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblPersonas)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_EstadoPersona");

                entity.HasOne(d => d.IdTipoPersonaNavigation)
                    .WithMany(p => p.TblPersonas)
                    .HasForeignKey(d => d.IdTipoPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_TipoPersona_Persona");
            });

            modelBuilder.Entity<TblProducto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("tbl_Producto");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.TblProductos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_tbl_CategoriaProducto");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblProductos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_tbl_EstadoProducto");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.TblProductos)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_tbl_MarcaProducto");
            });

            modelBuilder.Entity<TblTipoPersona>(entity =>
            {
                entity.HasKey(e => e.IdTipoPersona);

                entity.ToTable("tbl_TipoPersona");

                entity.Property(e => e.NombreTipoPersona)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblTipoPersonas)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_EstadoTipoPersona");
            });

            modelBuilder.Entity<TblUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("tbl_Usuarios");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblUsuarios)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Estado_Usuario");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TblUsuarios)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Persona_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
