using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ticketanicav2.DataLayer;

public partial class TicketanicaDbContext : DbContext
{
    public TicketanicaDbContext()
    {
    }

    public TicketanicaDbContext(DbContextOptions<TicketanicaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Direccione> Direcciones { get; set; }

    public virtual DbSet<Entrada> Entradas { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<ResetToken> ResetTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user id=root;password=colacao2;database=ticketanica", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.10.2-mariadb"));
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Direccione>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PRIMARY");

            entity.ToTable("direcciones");

            entity.Property(e => e.IdDireccion)
                .HasColumnType("int(11)")
                .HasColumnName("id_direccion");
            entity.Property(e => e.CalleName)
                .HasMaxLength(45)
                .HasColumnName("calle_name");
            entity.Property(e => e.CalleNro)
                .HasColumnType("int(11)")
                .HasColumnName("calle_nro");
            entity.Property(e => e.CiudadName)
                .HasMaxLength(45)
                .HasColumnName("ciudad_name");
            entity.Property(e => e.LocalName)
                .HasMaxLength(45)
                .HasColumnName("local_name");
        });

        modelBuilder.Entity<Entrada>(entity =>
        {
            entity.HasKey(e => e.IdEntrada).HasName("PRIMARY");

            entity.ToTable("entradas");

            entity.Property(e => e.IdEntrada)
                .HasColumnType("int(11)")
                .HasColumnName("id_entrada");
            entity.Property(e => e.CodigoQr)
                .HasMaxLength(45)
                .HasColumnName("codigoQR");
            entity.Property(e => e.Usada).HasColumnName("usada");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PRIMARY");

            entity.ToTable("eventos");

            entity.HasIndex(e => e.IdDireccion, "eventos_direcciones_id_direccion_fk");

            entity.HasIndex(e => e.IdEntrada, "eventos_entradas_id_entrada_fk");

            entity.Property(e => e.IdEvento)
                .HasColumnType("int(11)")
                .HasColumnName("id_evento");
            entity.Property(e => e.ArtistaName)
                .HasMaxLength(45)
                .HasColumnName("artista_name");
            entity.Property(e => e.CapacidadMaxima)
                .HasColumnType("int(11)")
                .HasColumnName("capacidad_maxima");
            entity.Property(e => e.EventoName)
                .HasMaxLength(45)
                .HasColumnName("evento_name");
            entity.Property(e => e.IdDireccion)
                .HasColumnType("int(11)")
                .HasColumnName("id_direccion");
            entity.Property(e => e.IdEntrada)
                .HasColumnType("int(11)")
                .HasColumnName("id_entrada");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventos_direcciones_id_direccion_fk");

            entity.HasOne(d => d.IdEntradaNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdEntrada)
                .HasConstraintName("eventos_entradas_id_entrada_fk");
        });

        modelBuilder.Entity<ResetToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reset_tokens");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp")
                .HasColumnName("expires_at");
            entity.Property(e => e.Token)
                .HasMaxLength(64)
                .IsFixedLength()
                .HasColumnName("token");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ResetTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("reset_tokens_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
