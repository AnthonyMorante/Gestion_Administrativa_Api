﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Models;

public partial class _context : DbContext
{
    public _context()
    {
    }

    public _context(DbContextOptions<_context> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudades> Ciudades { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<DetallePrecioProductos> DetallePrecioProductos { get; set; }

    public virtual DbSet<Empleados> Empleados { get; set; }

    public virtual DbSet<Empresas> Empresas { get; set; }

    public virtual DbSet<Ivas> Ivas { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }

    public virtual DbSet<Provincias> Provincias { get; set; }

    public virtual DbSet<TipoIdentificaciones> TipoIdentificaciones { get; set; }

    public virtual DbSet<TipoNegocios> TipoNegocios { get; set; }

    public virtual DbSet<UsuarioEmpresas> UsuarioEmpresas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432; Database=gestion_administrativa; Username=postgres; Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudades>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("ciudades_pkey");

            entity.ToTable("ciudades");

            entity.Property(e => e.IdCiudad)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idCiudad");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("false")
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdProvincia).HasColumnName("idProvincia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("ciudades_idProvincia_fkey");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.Property(e => e.IdCliente)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idCliente");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .HasColumnName("identificacion");
            entity.Property(e => e.Observacion)
                .HasMaxLength(1000)
                .HasColumnName("observacion");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Representante)
                .HasMaxLength(30)
                .HasColumnName("representante");
            entity.Property(e => e.Telefono)
                .HasMaxLength(500)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("clientes_idCiudad_fkey");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("cientes_empresas");

            entity.HasOne(d => d.IdTipoIdentificacionNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdTipoIdentificacion)
                .HasConstraintName("clientes_idTipoIdentificacion_fkey");
        });

        modelBuilder.Entity<DetallePrecioProductos>(entity =>
        {
            entity.HasKey(e => e.IdDetallePrecioProducto).HasName("detallePrecioProductos_pkey");

            entity.ToTable("detallePrecioProductos");

            entity.Property(e => e.IdDetallePrecioProducto)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDetallePrecioProducto");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(8, 2)
                .HasColumnName("porcentaje");
            entity.Property(e => e.Total)
                .HasPrecision(8, 2)
                .HasColumnName("total");
            entity.Property(e => e.TotalIva)
                .HasPrecision(8, 2)
                .HasColumnName("totalIva");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePrecioProductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("detallePrecioProductos_idProducto_fkey");
        });

        modelBuilder.Entity<Empleados>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("empleados_pkey");

            entity.ToTable("empleados");

            entity.Property(e => e.IdEmpleado)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idEmpleado");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .HasColumnName("identificacion");
            entity.Property(e => e.Observacion)
                .HasMaxLength(1000)
                .HasColumnName("observacion");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(500)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("empleados_idCiudad_fkey");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("empleados_empresas");

            entity.HasOne(d => d.IdTipoIdentificacionNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTipoIdentificacion)
                .HasConstraintName("empleados_idTipoIdentificacion_fkey");
        });

        modelBuilder.Entity<Empresas>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("empresas_pkey");

            entity.ToTable("empresas");

            entity.Property(e => e.IdEmpresa)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idEmpresa");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.AgenteRetencion).HasColumnName("agenteRetencion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdTipoNegocio).HasColumnName("idTipoNegocio");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .HasColumnName("identificacion");
            entity.Property(e => e.LlevaContabilidad).HasColumnName("llevaContabilidad");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .HasColumnName("razonSocial");
            entity.Property(e => e.RegimenMicroempresas).HasColumnName("regimenMicroempresas");
            entity.Property(e => e.RegimenRimpe).HasColumnName("regimenRimpe");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdEmpresaNavigation).WithOne(p => p.InverseIdEmpresaNavigation)
                .HasForeignKey<Empresas>(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("empresas_empresas");

            entity.HasOne(d => d.IdTipoNegocioNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdTipoNegocio)
                .HasConstraintName("empresas_TipoNegocios");
        });

        modelBuilder.Entity<Ivas>(entity =>
        {
            entity.HasKey(e => e.IdIva).HasName("ivas_pkey");

            entity.ToTable("ivas");

            entity.Property(e => e.IdIva)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idIva");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasPrecision(8, 2)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("productos_pkey");

            entity.ToTable("productos");

            entity.Property(e => e.IdProducto)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idProducto");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.ActivoProducto)
                .HasDefaultValueSql("true")
                .HasColumnName("activoProducto");
            entity.Property(e => e.Codigo)
                .HasMaxLength(500)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdIva).HasColumnName("idIva");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(8, 2)
                .HasColumnName("precio");
            entity.Property(e => e.TotalIva)
                .HasPrecision(8, 2)
                .HasColumnName("totalIva");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("productos_empresas");

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdIva)
                .HasConstraintName("productos_idIva_fkey");
        });

        modelBuilder.Entity<Proveedores>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("proveedores_pkey");

            entity.ToTable("proveedores");

            entity.Property(e => e.IdProveedor)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idProveedor");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .HasColumnName("identificacion");
            entity.Property(e => e.Observacion)
                .HasMaxLength(1000)
                .HasColumnName("observacion");
            entity.Property(e => e.PaginaWeb)
                .HasMaxLength(1000)
                .HasColumnName("paginaWeb");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Representante)
                .HasMaxLength(30)
                .HasColumnName("representante");
            entity.Property(e => e.Telefono)
                .HasMaxLength(500)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("proveedores_idCiudad_fkey");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("proveedores_empresas");

            entity.HasOne(d => d.IdTipoIdentificacionNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdTipoIdentificacion)
                .HasConstraintName("proveedores_idTipoIdentificacion_fkey");
        });

        modelBuilder.Entity<Provincias>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("provincias_pkey");

            entity.ToTable("provincias");

            entity.Property(e => e.IdProvincia)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idProvincia");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("false")
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoIdentificaciones>(entity =>
        {
            entity.HasKey(e => e.IdTipoIdentificacion).HasName("tipoIdentificaciones_pkey");

            entity.ToTable("tipoIdentificaciones");

            entity.Property(e => e.IdTipoIdentificacion)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("false")
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoNegocios>(entity =>
        {
            entity.HasKey(e => e.IdTipoNegocio).HasName("tipoNegocios_pkey");

            entity.ToTable("tipoNegocios");

            entity.Property(e => e.IdTipoNegocio)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTipoNegocio");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("false")
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(500)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UsuarioEmpresas>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("usuarioEmpresas");

            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdUsuarioEmpresas)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idUsuarioEmpresas");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany()
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarioEmpresas_idEmpresa_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarioEmpresas_idUsuario_fkey");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.Clave)
                .HasMaxLength(500)
                .HasColumnName("clave");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
