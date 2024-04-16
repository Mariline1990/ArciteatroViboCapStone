using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ArciteatroVibo.Models;

public partial class ArciteatroViboValentiaContext : DbContext
{
    public ArciteatroViboValentiaContext()
    {
    }

    public ArciteatroViboValentiaContext(DbContextOptions<ArciteatroViboValentiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Commedie> Commedies { get; set; }

    public virtual DbSet<Contattaci> Contattacis { get; set; }

    public virtual DbSet<Contatti> Contattis { get; set; }

    public virtual DbSet<Eventi> Eventis { get; set; }

    public virtual DbSet<Home1> Home1s { get; set; }

    public virtual DbSet<Iscriviti> Iscrivitis { get; set; }

    public virtual DbSet<Laboratorio> Laboratorios { get; set; }

    public virtual DbSet<Progetti> Progettis { get; set; }

    public virtual DbSet<Rassegne> Rassegnes { get; set; }

    public virtual DbSet<Richieste> Richiestes { get; set; }

    public virtual DbSet<Utenti> Utentis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=KRAKEN\\SQLEXPRESS;Database=ArciteatroViboValentia;TrustServerCertificate=true;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.IdFoto);

            entity.ToTable("Album");

            entity.Property(e => e.IdFoto).HasColumnName("Id_Foto");
            entity.Property(e => e.Img).HasColumnName("img");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.IdCard);

            entity.Property(e => e.IdCard).HasColumnName("Id_Card");
            entity.Property(e => e.Link).HasColumnName("link");
        });

        modelBuilder.Entity<Commedie>(entity =>
        {
            entity.HasKey(e => e.IdCommedia);

            entity.ToTable("Commedie");

            entity.Property(e => e.IdCommedia).HasColumnName("Id_Commedia");
        });

        modelBuilder.Entity<Contattaci>(entity =>
        {
            entity.HasKey(e => e.IdContattici);

            entity.ToTable("Contattaci");

            entity.Property(e => e.IdContattici).HasColumnName("Id_Contattici");
        });

        modelBuilder.Entity<Contatti>(entity =>
        {
            entity.HasKey(e => e.IdContatto);

            entity.ToTable("Contatti");

            entity.Property(e => e.IdContatto).HasColumnName("Id_Contatto");
        });

        modelBuilder.Entity<Eventi>(entity =>
        {
            entity.HasKey(e => e.IdEvento);

            entity.ToTable("Eventi");

            entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");
            entity.Property(e => e.InCorso).HasColumnName("In_corso");
        });

        modelBuilder.Entity<Home1>(entity =>
        {
            entity.HasKey(e => e.IdHome1);

            entity.ToTable("Home1");

            entity.Property(e => e.IdHome1).HasColumnName("Id_Home1");
        });

        modelBuilder.Entity<Iscriviti>(entity =>
        {
            entity.HasKey(e => e.IdIscriviti);

            entity.ToTable("Iscriviti");

            entity.Property(e => e.IdIscriviti).HasColumnName("Id_Iscriviti");
            entity.Property(e => e.DataFine).HasColumnName("Data_fine");
            entity.Property(e => e.DataInizio).HasColumnName("Data_inizio");
            entity.Property(e => e.EMail).HasColumnName("E_mail");
        });

        modelBuilder.Entity<Laboratorio>(entity =>
        {
            entity.HasKey(e => e.IdLaboratorio);

            entity.ToTable("Laboratorio");

            entity.Property(e => e.IdLaboratorio).HasColumnName("Id_Laboratorio");
            entity.Property(e => e.DataFine).HasColumnName("Data_fine");
            entity.Property(e => e.DataInizio).HasColumnName("Data_inizio");
            entity.Property(e => e.EMail).HasColumnName("E_mail");
            entity.Property(e => e.PostiLiberi).HasColumnName("Posti_liberi");
        });

        modelBuilder.Entity<Progetti>(entity =>
        {
            entity.HasKey(e => e.IdProgetto);

            entity.ToTable("Progetti");

            entity.Property(e => e.IdProgetto).HasColumnName("Id_Progetto");
            entity.Property(e => e.Pdf).HasColumnName("pdf");
        });

        modelBuilder.Entity<Rassegne>(entity =>
        {
            entity.HasKey(e => e.IdRassegna);

            entity.ToTable("Rassegne");

            entity.Property(e => e.IdRassegna).HasColumnName("Id_Rassegna");
        });

        modelBuilder.Entity<Richieste>(entity =>
        {
            entity.HasKey(e => e.IdRichiesta);

            entity.ToTable("Richieste");

            entity.Property(e => e.IdRichiesta).HasColumnName("Id_Richiesta");
            entity.Property(e => e.Cognome)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CorpoRichiesta)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Corpo_richiesta");
            entity.Property(e => e.FkLaboratorio).HasColumnName("fk_laboratorio");
            entity.Property(e => e.FkUtente).HasColumnName("fk_utente");
            entity.Property(e => e.Nome)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.FkLaboratorioNavigation).WithMany(p => p.Richiestes)
                .HasForeignKey(d => d.FkLaboratorio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Richieste_Laboratorio");

            entity.HasOne(d => d.FkUtenteNavigation).WithMany(p => p.Richiestes)
                .HasForeignKey(d => d.FkUtente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Richieste_Utenti");
        });

        modelBuilder.Entity<Utenti>(entity =>
        {
            entity.HasKey(e => e.IdUtente);

            entity.ToTable("Utenti");

            entity.Property(e => e.IdUtente).HasColumnName("Id_Utente");
            entity.Property(e => e.Cognome)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Nome)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
