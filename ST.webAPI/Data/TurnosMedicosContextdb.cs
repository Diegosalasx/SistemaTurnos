using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ST.webAPI.Data.Entities;

namespace ST.webAPI.Data;

public partial class TurnosMedicosContextdb : DbContext
{
    public TurnosMedicosContextdb()
    {
    }

    public TurnosMedicosContextdb(DbContextOptions<TurnosMedicosContextdb> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Dia> Dias { get; set; }

    public virtual DbSet<EspecialidadesMedica> EspecialidadesMedicas { get; set; }

    public virtual DbSet<EspecialidadesMedicasHorario> EspecialidadesMedicasHorarios { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SUNWELL-5;Database=TurnosMedicos;User Id=sa;Password=Cocoloco;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Dia>(entity =>
        {
            entity.Property(e => e.DiaId).HasColumnName("DiaID");
            entity.Property(e => e.NombreCorto)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.NombreDia)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EspecialidadesMedica>(entity =>
        {
            entity.HasKey(e => e.EspecialidadMedicaId);

            entity.Property(e => e.EspecialidadMedicaId).HasColumnName("EspecialidadMedicaID");
            entity.Property(e => e.Creacion).HasColumnType("datetime");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EspecialidadesMedicasHorario>(entity =>
        {
            entity.HasKey(e => e.EspecialidadMedicaHorarioId);

            entity.Property(e => e.EspecialidadMedicaHorarioId).HasColumnName("EspecialidadMedicaHorarioID");
            entity.Property(e => e.EspecialidadMedicaId).HasColumnName("EspecialidadMedicaID");
            entity.Property(e => e.HorarioId).HasColumnName("HorarioID");

            entity.HasOne(d => d.EspecialidadMedica).WithMany(p => p.EspecialidadesMedicasHorarios)
                .HasForeignKey(d => d.EspecialidadMedicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EspecialidadesMedicasHorarios_EspecialidadesMedicas");

            entity.HasOne(d => d.Horario).WithMany(p => p.EspecialidadesMedicasHorarios)
                .HasForeignKey(d => d.HorarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EspecialidadesMedicasHorarios_Horarios");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.Property(e => e.HorarioId).HasColumnName("HorarioID");
            entity.Property(e => e.DiaId).HasColumnName("DiaID");

            entity.HasOne(d => d.Dia).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.DiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Horarios_Dias1");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.Property(e => e.TurnoId).HasColumnName("TurnoID");
            entity.Property(e => e.DiaId).HasColumnName("DiaID");
            entity.Property(e => e.EspecialidadMedicaId).HasColumnName("EspecialidadMedicaID");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.PacienteNombre).HasMaxLength(150);

            entity.HasOne(d => d.Dia).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.DiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turnos_Dias");

            entity.HasOne(d => d.EspecialidadMedica).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.EspecialidadMedicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turnos_EspecialidadesMedicas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
