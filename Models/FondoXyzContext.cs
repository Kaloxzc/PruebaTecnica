using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Models;


public partial class FondoXyzContext : IdentityDbContext<ApplicationUser>
{
    public FondoXyzContext(DbContextOptions<FondoXyzContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Alojamiento> Alojamientos { get; set; }

    public virtual DbSet<AlojamientosCapacidadTotal> AlojamientosCapacidadTotals { get; set; }

    public virtual DbSet<Apartamento> Apartamentoes { get; set; }

    public virtual DbSet<ApartamentoBano> ApartamentoBanos { get; set; }

    public virtual DbSet<ApartamentoCama> ApartamentoCamas { get; set; }

    public virtual DbSet<ApartamentoCiudad> ApartamentoCiudads { get; set; }

    public virtual DbSet<Cama> Camas { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<ServicioSede> ServicioSedes { get; set; }

    public virtual DbSet<TarifaAlojamiento> TarifaAlojamientos { get; set; }

    public virtual DbSet<TarifaApartamento> TarifaApartamentos { get; set; }

    public virtual DbSet<TarifaLavanderium> TarifaLavanderia { get; set; }

    public virtual DbSet<TarifaPersonaAdicional> TarifaPersonaAdicionals { get; set; }

    public virtual DbSet<TarifaPersonaAdicionalTemporadum> TarifaPersonaAdicionalTemporada { get; set; }

    public virtual DbSet<TarifaVisitaDium> TarifaVisitaDia { get; set; }

    public virtual DbSet<Temporadum> Temporada { get; set; }

    public virtual DbSet<ReservaApartamento> ReservaApartamentos { get; set; }

    public virtual DbSet<RecoveryCode> RecoveryCode { get; set; }

    public virtual DbSet<MisReservasApt> MisReservasApt { get; set; }

    public virtual DbSet<MisReservasAloj> MisReservasAloj { get; set; }


    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    ////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    ////        => optionsBuilder.UseSqlServer("Server=DESKTOP-CCD40NB\\SQLKAL;Initial Catalog=FondoXYZ;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
        modelBuilder.Entity<Alojamiento>(entity =>
        {
            entity.HasKey(e => e.IdAlojamiento).HasName("PK__Alojamie__EF77F57275F8EE02");

            entity.ToTable("Alojamiento");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Observaciones).HasMaxLength(250);
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.Alojamientos)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Alojamien__IdSed__5629CD9C");
        });

        modelBuilder.Entity<AlojamientosCapacidadTotal>(entity =>
        {
            entity.HasKey(e => e.IdAlojamientosCapacidadTotal).HasName("PK__Alojamie__6827F5EDB2C1EC28");

            entity.ToTable("AlojamientosCapacidadTotal");
        });
        modelBuilder.Entity<RecoveryCode>(entity =>
        {
            entity.HasIndex(e => new { e.UserId, e.Code }).IsUnique();
            entity.Property(e => e.Expiration).HasColumnType("datetime2");
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
        });
        modelBuilder.Entity<Apartamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Apartame__3214EC07B3C5E9FD");

            entity.ToTable("Apartamento");

            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Apartamentos)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK__Apartamen__IdCiu__4BAC3F29");
        });

        modelBuilder.Entity<ApartamentoBano>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Apartame__3214EC07A1AF1EDA");

            entity.HasOne(d => d.IdApartamentoNavigation).WithMany(p => p.ApartamentoBanos)
                .HasForeignKey(d => d.IdApartamento)
                .HasConstraintName("FK__Apartamen__IdApa__5165187F");
        });

        modelBuilder.Entity<ApartamentoCama>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Apartame__3214EC07064A1CB5");

            entity.Property(e => e.TipoCama).HasMaxLength(50);

            entity.HasOne(d => d.IdApartamentoNavigation).WithMany(p => p.ApartamentoCamas)
                .HasForeignKey(d => d.IdApartamento)
                .HasConstraintName("FK__Apartamen__IdApa__4E88ABD4");
        });

        modelBuilder.Entity<ApartamentoCiudad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Apartame__3214EC073BE357A5");

            entity.ToTable("ApartamentoCiudad");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Cama>(entity =>
        {
            entity.HasKey(e => e.IdCama).HasName("PK__Cama__3B7B1B619E80F075");

            entity.ToTable("Cama");

            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Camas)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK__Cama__IdHabitaci__5DCAEF64");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__8BBBF901B69ECC42");

            entity.ToTable("Habitacion");

            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Observaciones).HasMaxLength(200);

            entity.HasOne(d => d.IdAlojamientoNavigation).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.IdAlojamiento)
                .HasConstraintName("FK__Habitacio__IdAlo__5AEE82B9");
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.IdSede).HasName("PK__Sede__A7780DFF310ACB83");

            entity.ToTable("Sede");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<ServicioSede>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__2DCCF9A29A0CCA8E");

            entity.ToTable("ServicioSede");

            entity.Property(e => e.Servicio).HasMaxLength(100);

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.ServicioSedes)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__ServicioS__IdSed__60A75C0F");
        });

        modelBuilder.Entity<TarifaAlojamiento>(entity =>
        {
            entity.HasKey(e => e.IdTarifa).HasName("PK__TarifaAl__78F1A91D42C6EF78");

            entity.ToTable("TarifaAlojamiento");

            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdAlojamientoNavigation).WithMany(p => p.TarifaAlojamientos)
                .HasForeignKey(d => d.IdAlojamiento)
                .HasConstraintName("FK__TarifaAlo__IdAlo__693CA210");

            entity.HasOne(d => d.IdTemporadaNavigation).WithMany(p => p.TarifaAlojamientos)
                .HasForeignKey(d => d.IdTemporada)
                .HasConstraintName("FK__TarifaAlo__IdTem__6A30C649");
        });

        modelBuilder.Entity<TarifaApartamento>(entity =>
        {
            entity.HasKey(e => e.IdTarifa).HasName("PK__TarifaAp__78F1A91DA8173760");

            entity.ToTable("TarifaApartamento");

            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.TarifaApartamentos)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK__TarifaAparta__Id__656C112C");

            entity.HasOne(d => d.IdTemporadaNavigation).WithMany(p => p.TarifaApartamentos)
                .HasForeignKey(d => d.IdTemporada)
                .HasConstraintName("FK__TarifaApa__IdTem__66603565");
        });

        modelBuilder.Entity<TarifaLavanderium>(entity =>
        {
            entity.HasKey(e => e.IdTarifaLavanderia).HasName("PK__TarifaLa__0924F76CBAC003F7");

            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("money");
        });

        modelBuilder.Entity<TarifaPersonaAdicional>(entity =>
        {
            entity.HasKey(e => e.IdTarifaPersonaAdicional).HasName("PK__TarifaPe__8C301A86B84D09F3");

            entity.ToTable("TarifaPersonaAdicional");

            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("money");
        });

        modelBuilder.Entity<TarifaPersonaAdicionalTemporadum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TarifaPe__3214EC0727863DBC");

            entity.ToTable("TarifaPersonaAdicional_Temporada");

            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdTarifaPersonaAdicionalNavigation).WithMany(p => p.TarifaPersonaAdicionalTemporada)
                .HasForeignKey(d => d.IdTarifaPersonaAdicional)
                .HasConstraintName("FK__TarifaPer__IdTar__70DDC3D8");

            entity.HasOne(d => d.IdTemporadaNavigation).WithMany(p => p.TarifaPersonaAdicionalTemporada)
                .HasForeignKey(d => d.IdTemporada)
                .HasConstraintName("FK__TarifaPer__IdTem__71D1E811");
        });

        modelBuilder.Entity<TarifaVisitaDium>(entity =>
        {
            entity.HasKey(e => e.IdTarifaVisitaDia).HasName("PK__TarifaVi__1F619C9F90B3051B");

            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("money");
        });

        modelBuilder.Entity<Temporadum>(entity =>
        {
            entity.HasKey(e => e.IdTemporada).HasName("PK__Temporad__80F418213553B873");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReservaApartamento>(entity =>
        {
            entity.HasKey(e => e.IdReserva);

            entity.ToTable("ReservaApartamento");

            entity.Property(e => e.FechaLlegada).HasColumnType("date");
            entity.Property(e => e.FechaSalida).HasColumnType("date");
            entity.Property(e => e.PrecioTotal).HasColumnType("money");

            entity.Property(e => e.IncluyeLavanderia).HasDefaultValue(false);

            entity.HasOne(e => e.Apartamento)
                .WithMany() // o .WithMany(a => a.Reservas) si agregás la colección en Apartamento
                .HasForeignKey(e => e.IdApartamento)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReservaApartamento_Apartamento");

            entity.HasOne(e => e.Temporada)
                .WithMany() // o .WithMany(t => t.Reservas) si agregás la colección en Temporadum
                .HasForeignKey(e => e.IdTemporada)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReservaApartamento_Temporada");
        });

        modelBuilder.Entity<MisReservasApt>(entity =>
        {
            // Configuración de la clave primaria
            entity.HasKey(e => e.IdReserva)
                  .HasName("PK_MisReservasApt");

            // Configuración de las propiedades
            entity.Property(e => e.FechaLlegada)
                  .IsRequired()
                  .HasColumnType("date");

            entity.Property(e => e.FechaSalida)
                  .IsRequired()
                  .HasColumnType("date");

            entity.Property(e => e.FechaCreacion)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");

            // Configuración de las relaciones
            entity.HasOne(e => e.Apartamento)
                  .WithMany()
                  .HasForeignKey(e => e.IdApartamento)
                  .OnDelete(DeleteBehavior.Restrict) // O ClientCascade si prefieres
                  .HasConstraintName("FK_MisReservasApt_Apartamento");

            entity.HasOne(e => e.Tarifa)
                  .WithMany()
                  .HasForeignKey(e => e.IdTarifa)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_MisReservasApt_TarifaApartamento");

            // Configuración de índices
            entity.HasIndex(e => new { e.IdApartamento, e.FechaLlegada, e.FechaSalida })
                  .HasDatabaseName("IX_MisReservasApt_ApartamentoFechas");


            // Restricciones CHECK
            entity.ToTable(tb => tb.HasCheckConstraint(
                "CHK_MisReservasApt_FechasValidas",
                "FechaLlegada < FechaSalida"));

            entity.ToTable(tb => tb.HasCheckConstraint(
                "CHK_MisReservasApt_EstadoValido",
                "Estado IN ('Pendiente', 'Confirmada', 'Cancelada')"));
        });
        modelBuilder.Entity<MisReservasAloj>(entity =>
        {
            entity.HasKey(e => e.IdReserva);

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(e => e.Alojamiento)
                .WithMany()
                .HasForeignKey(e => e.IdAlojamiento)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MisReservasAloj_Alojamiento");

            entity.HasOne(e => e.Tarifa)
                .WithMany()
                .HasForeignKey(e => e.IdTarifa)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MisReservasApt_TarifaAlojamiento");

            entity.HasOne(e => e.Temporada)
                .WithMany()
                .HasForeignKey(e => e.IdTemporada)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MisReservasApt_Temporada");

        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



}

