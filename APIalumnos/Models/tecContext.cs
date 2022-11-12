using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace APIalumnos.Models
{
    public partial class tecContext : DbContext
    {
        public tecContext()
        {
        }

        public tecContext(DbContextOptions<tecContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumnostable> Alumnostable { get; set; }
        public virtual DbSet<Alumnoxmateriatable> Alumnoxmateriatable { get; set; }
        public virtual DbSet<Avisostable> Avisostable { get; set; }
        public virtual DbSet<Docentestable> Docentestable { get; set; }
        public virtual DbSet<Materiastable> Materiastable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;database=tec;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.10.1-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1");

            modelBuilder.Entity<Alumnostable>(entity =>
            {
                entity.ToTable("alumnostable");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("contrasena")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.NombreAlumno)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("nombreAlumno")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("usuario")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Alumnoxmateriatable>(entity =>
            {
                entity.ToTable("alumnoxmateriatable");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.IdAlumno, "FK_alumnoxmateriatable_alumnostable");

                entity.HasIndex(e => e.IdMateria, "FK_alumnoxmateriatable_materiastable");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdAlumno)
                    .HasColumnType("int(11)")
                    .HasColumnName("idAlumno");

                entity.Property(e => e.IdMateria)
                    .HasColumnType("int(11)")
                    .HasColumnName("idMateria");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Alumnoxmateriatable)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_alumnoxmateriatable_alumnostable");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Alumnoxmateriatable)
                    .HasForeignKey(d => d.IdMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_alumnoxmateriatable_materiastable");
            });

            modelBuilder.Entity<Avisostable>(entity =>
            {
                entity.ToTable("avisostable");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.IdMateriaAviso, "FK_avisostable_materiastable");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.IdMateriaAviso)
                    .HasColumnType("int(11)")
                    .HasColumnName("idMateriaAviso");

                entity.Property(e => e.MensajeAviso)
                    .IsRequired()
                    .HasMaxLength(360)
                    .HasColumnName("mensajeAviso")
                    .HasDefaultValueSql("''");

                entity.HasOne(d => d.IdMateriaAvisoNavigation)
                    .WithMany(p => p.Avisostable)
                    .HasForeignKey(d => d.IdMateriaAviso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_avisostable_materiastable");
            });

            modelBuilder.Entity<Docentestable>(entity =>
            {
                entity.ToTable("docentestable");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("contrasena")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.NombreDocente)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("nombreDocente")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("usuario")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Materiastable>(entity =>
            {
                entity.ToTable("materiastable");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.IdProfesorMateria, "FK_materiastable_docentestable");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdProfesorMateria)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProfesorMateria");

                entity.Property(e => e.NombreMateria)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnName("nombreMateria")
                    .HasDefaultValueSql("''");

                entity.HasOne(d => d.IdProfesorMateriaNavigation)
                    .WithMany(p => p.Materiastable)
                    .HasForeignKey(d => d.IdProfesorMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_materiastable_docentestable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
