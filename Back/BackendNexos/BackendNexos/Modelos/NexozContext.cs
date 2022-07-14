using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackendNexos.Modelos
{
    public partial class NexozContext : DbContext
    {
        //Controlde modulos de la base de datos
        public NexozContext()
        {
        }

        public NexozContext(DbContextOptions<NexozContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autors { get; set; } = null!;
        public virtual DbSet<Genero> Generos { get; set; } = null!;
        public virtual DbSet<Libro> Libros { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=usuario;Database=Nexoz;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.HasKey(e => e.IdAutor)
                    .HasName("PK__Autor__DD33B031A3642C08");

                entity.ToTable("Autor");

                entity.Property(e => e.CiudadProcedencia)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Ciudad_Procedencia");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Nacimiento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PK__Genero__0F8349889E907893");

                entity.ToTable("Genero");

                entity.Property(e => e.Descipcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasKey(e => e.IdLibros)
                    .HasName("PK__Libros__153221F3DCD383DE");

                entity.Property(e => e.Año).IsUnicode(false);

                entity.Property(e => e.NumPaginas).IsUnicode(false);

                entity.Property(e => e.Titulo).IsUnicode(false);

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.IdAutor)
                    .HasConstraintName("FK__Libros__IdAutor__29572725");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.IdGenero)
                    .HasConstraintName("FK__Libros__IdGenero__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
