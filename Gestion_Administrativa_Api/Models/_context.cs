using System;
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

    public virtual DbSet<DetalleFacturas> DetalleFacturas { get; set; }

    public virtual DbSet<DetalleFormaPagos> DetalleFormaPagos { get; set; }

    public virtual DbSet<DetallePrecioProductos> DetallePrecioProductos { get; set; }

    public virtual DbSet<DetalleProformas> DetalleProformas { get; set; }

    public virtual DbSet<DocumentosEmitir> DocumentosEmitir { get; set; }

    public virtual DbSet<Empleados> Empleados { get; set; }

    public virtual DbSet<Empresas> Empresas { get; set; }

    public virtual DbSet<Establecimientos> Establecimientos { get; set; }

    public virtual DbSet<Facturas> Facturas { get; set; }

    public virtual DbSet<FormaPagos> FormaPagos { get; set; }

    public virtual DbSet<ImpuestoRetenciones> ImpuestoRetenciones { get; set; }

    public virtual DbSet<InformacionAdicional> InformacionAdicional { get; set; }

    public virtual DbSet<InformacionAdicionalRetencion> InformacionAdicionalRetencion { get; set; }

    public virtual DbSet<InformacionFirmas> InformacionFirmas { get; set; }

    public virtual DbSet<Ivas> Ivas { get; set; }

    public virtual DbSet<Lotes> Lotes { get; set; }

    public virtual DbSet<Personas> Personas { get; set; }

    public virtual DbSet<PorcentajeImpuestosRetenciones> PorcentajeImpuestosRetenciones { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Proformas> Proformas { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }

    public virtual DbSet<Provincias> Provincias { get; set; }

    public virtual DbSet<PuntoEmisiones> PuntoEmisiones { get; set; }

    public virtual DbSet<Retenciones> Retenciones { get; set; }

    public virtual DbSet<Secuenciales> Secuenciales { get; set; }

    public virtual DbSet<SecuencialesProformas> SecuencialesProformas { get; set; }

    public virtual DbSet<Sriambientes> Sriambientes { get; set; }

    public virtual DbSet<Sriestados> Sriestados { get; set; }

    public virtual DbSet<Srifacturas> Srifacturas { get; set; }

    public virtual DbSet<Sriformaspagos> Sriformaspagos { get; set; }

    public virtual DbSet<Srimonedas> Srimonedas { get; set; }

    public virtual DbSet<Sriporcentajesimpuestos> Sriporcentajesimpuestos { get; set; }

    public virtual DbSet<Sritiposdocumentos> Sritiposdocumentos { get; set; }

    public virtual DbSet<Sritiposidentificaciones> Sritiposidentificaciones { get; set; }

    public virtual DbSet<Sriunidadestiempos> Sriunidadestiempos { get; set; }

    public virtual DbSet<TiempoFormaPagos> TiempoFormaPagos { get; set; }

    public virtual DbSet<TipoDocumentos> TipoDocumentos { get; set; }

    public virtual DbSet<TipoEstadoDocumentos> TipoEstadoDocumentos { get; set; }

    public virtual DbSet<TipoEstadoSri> TipoEstadoSri { get; set; }

    public virtual DbSet<TipoIdentificaciones> TipoIdentificaciones { get; set; }

    public virtual DbSet<TipoIdentificacionesGeneracionDocumentos> TipoIdentificacionesGeneracionDocumentos { get; set; }

    public virtual DbSet<TipoNegocios> TipoNegocios { get; set; }

    public virtual DbSet<TipoValorRetenciones> TipoValorRetenciones { get; set; }

    public virtual DbSet<UsuarioEmpresas> UsuarioEmpresas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("name=cn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudades>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("ciudades_pkey");

            entity.ToTable("ciudades");

            entity.HasIndex(e => e.IdProvincia, "IX_ciudades_idProvincia");

            entity.Property(e => e.IdCiudad)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idCiudad");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
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

            entity.HasIndex(e => e.IdCiudad, "IX_clientes_idCiudad");

            entity.HasIndex(e => e.IdEmpresa, "IX_clientes_idEmpresa");

            entity.HasIndex(e => e.IdTipoIdentificacion, "IX_clientes_idTipoIdentificacion");

            entity.Property(e => e.IdCliente)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idCliente");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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

        modelBuilder.Entity<DetalleFacturas>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura).HasName("detalleFacturas_pkey");

            entity.ToTable("detalleFacturas");

            entity.HasIndex(e => e.IdFactura, "IX_detalleFacturas_idFactura");

            entity.Property(e => e.IdDetalleFactura)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDetalleFactura");
            entity.Property(e => e.Cantidad)
                .HasPrecision(8, 2)
                .HasColumnName("cantidad");
            entity.Property(e => e.Descuento)
                .HasPrecision(8, 2)
                .HasColumnName("descuento");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdIva).HasColumnName("idIva");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(8, 2)
                .HasColumnName("porcentaje");
            entity.Property(e => e.Precio)
                .HasPrecision(8, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Subtotal)
                .HasPrecision(8, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(8, 2)
                .HasColumnName("total");
            entity.Property(e => e.ValorPorcentaje)
                .HasPrecision(8, 2)
                .HasColumnName("valorPorcentaje");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleFacturas_idFactura_fkey");
        });

        modelBuilder.Entity<DetalleFormaPagos>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFormaPago).HasName("detalleFormaPagos_pkey");

            entity.ToTable("detalleFormaPagos");

            entity.HasIndex(e => e.IdFactura, "IX_detalleFormaPagos_idFactura");

            entity.HasIndex(e => e.IdTiempoFormaPago, "IX_detalleFormaPagos_idTiempoFormaPago");

            entity.Property(e => e.IdDetalleFormaPago)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDetalleFormaPago");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdFormaPago).HasColumnName("idFormaPago");
            entity.Property(e => e.IdTiempoFormaPago).HasColumnName("idTiempoFormaPago");
            entity.Property(e => e.Plazo)
                .HasPrecision(8, 2)
                .HasColumnName("plazo");
            entity.Property(e => e.Valor)
                .HasPrecision(8, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFormaPagos)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleFormaPagos_idFactura_fkey");

            entity.HasOne(d => d.IdTiempoFormaPagoNavigation).WithMany(p => p.DetalleFormaPagos)
                .HasForeignKey(d => d.IdTiempoFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleFormaPagos_idTiempoFormaPago_fkey");
        });

        modelBuilder.Entity<DetallePrecioProductos>(entity =>
        {
            entity.HasKey(e => e.IdDetallePrecioProducto).HasName("detallePrecioProductos_pkey");

            entity.ToTable("detallePrecioProductos");

            entity.HasIndex(e => e.IdIva, "IX_detallePrecioProductos_idIva");

            entity.HasIndex(e => e.IdProducto, "IX_detallePrecioProductos_idProducto");

            entity.Property(e => e.IdDetallePrecioProducto)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDetallePrecioProducto");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdIva).HasColumnName("idIva");
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

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.DetallePrecioProductos)
                .HasForeignKey(d => d.IdIva)
                .HasConstraintName("detallePrecioProductos_idIva_fkey");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePrecioProductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("detallePrecioProductos_idProducto_fkey");
        });

        modelBuilder.Entity<DetalleProformas>(entity =>
        {
            entity.HasKey(e => e.IdDetalleProforma).HasName("detalleProformas_pkey");

            entity.ToTable("detalleProformas");

            entity.HasIndex(e => e.IdProforma, "IX_detalleProformas_idProforma");

            entity.Property(e => e.IdDetalleProforma)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDetalleProforma");
            entity.Property(e => e.Cantidad)
                .HasPrecision(8, 2)
                .HasColumnName("cantidad");
            entity.Property(e => e.Descuento)
                .HasPrecision(8, 2)
                .HasColumnName("descuento");
            entity.Property(e => e.IdIva).HasColumnName("idIva");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdProforma).HasColumnName("idProforma");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(8, 2)
                .HasColumnName("porcentaje");
            entity.Property(e => e.Precio)
                .HasPrecision(8, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Subtotal)
                .HasPrecision(8, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(8, 2)
                .HasColumnName("total");
            entity.Property(e => e.ValorPorcentaje)
                .HasPrecision(8, 2)
                .HasColumnName("valorPorcentaje");

            entity.HasOne(d => d.IdProformaNavigation).WithMany(p => p.DetalleProformas)
                .HasForeignKey(d => d.IdProforma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idProforma");
        });

        modelBuilder.Entity<DocumentosEmitir>(entity =>
        {
            entity.HasKey(e => e.IdDocumentoEmitir).HasName("documentosEmitir_pkey");

            entity.ToTable("documentosEmitir");

            entity.HasIndex(e => e.IdTipoDocumento, "IX_documentosEmitir_idTipoDocumento");

            entity.Property(e => e.IdDocumentoEmitir)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDocumentoEmitir");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.DocumentosEmitir)
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("documentosEmitir_idTipoDocumento_fkey");
        });

        modelBuilder.Entity<Empleados>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("empleados_pkey");

            entity.ToTable("empleados");

            entity.HasIndex(e => e.IdCiudad, "IX_empleados_idCiudad");

            entity.HasIndex(e => e.IdEmpresa, "IX_empleados_idEmpresa");

            entity.HasIndex(e => e.IdTipoIdentificacion, "IX_empleados_idTipoIdentificacion");

            entity.Property(e => e.IdEmpleado)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idEmpleado");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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

            entity.HasIndex(e => e.IdInformacionFirma, "IX_empresas_idInformacionFirma");

            entity.HasIndex(e => e.IdTipoNegocio, "IX_empresas_idTipoNegocio");

            entity.Property(e => e.IdEmpresa)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idEmpresa");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.AgenteRetencion).HasColumnName("agenteRetencion");
            entity.Property(e => e.DireccionMatriz)
                .HasMaxLength(1000)
                .HasColumnName("direccionMatriz");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdInformacionFirma).HasColumnName("idInformacionFirma");
            entity.Property(e => e.IdTipoNegocio).HasColumnName("idTipoNegocio");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .HasColumnName("identificacion");
            entity.Property(e => e.LlevaContabilidad).HasColumnName("llevaContabilidad");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .HasColumnName("razonSocial");
            entity.Property(e => e.RegimenMicroEmpresas).HasColumnName("regimenMicroEmpresas");
            entity.Property(e => e.RegimenRimpe).HasColumnName("regimenRimpe");
            entity.Property(e => e.ResolucionAgenteRetencion)
                .HasMaxLength(500)
                .HasColumnName("resolucionAgenteRetencion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdInformacionFirmaNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdInformacionFirma)
                .HasConstraintName("fk_empresas_informacionfirmas");

            entity.HasOne(d => d.IdTipoNegocioNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdTipoNegocio)
                .HasConstraintName("empresas_TipoNegocios");
        });

        modelBuilder.Entity<Establecimientos>(entity =>
        {
            entity.HasKey(e => e.IdEstablecimiento).HasName("establecimientos_pkey");

            entity.ToTable("establecimientos");

            entity.HasIndex(e => e.IdEmpresa, "IX_establecimientos_idEmpresa");

            entity.Property(e => e.IdEstablecimiento)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idEstablecimiento");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(1000)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Predeterminado)
                .HasDefaultValue(false)
                .HasColumnName("predeterminado");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Establecimientos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("establecimientos_idEmpresa_fkey");
        });

        modelBuilder.Entity<Facturas>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("facturas_pkey");

            entity.ToTable("facturas");

            entity.HasIndex(e => e.IdTipoEstadoDocumento, "IX_facturas_idTipoEstadoDocumento");

            entity.HasIndex(e => e.IdTipoEstadoSri, "IX_facturas_idTipoEstadoSri");

            entity.HasIndex(e => e.IdUsuario, "IX_facturas_idUsuario");

            entity.Property(e => e.IdFactura)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idFactura");
            entity.Property(e => e.AgenteRetencion)
                .HasDefaultValue(false)
                .HasColumnName("agenteRetencion");
            entity.Property(e => e.Ambiente).HasColumnName("ambiente");
            entity.Property(e => e.Cambio)
                .HasPrecision(8, 2)
                .HasColumnName("cambio");
            entity.Property(e => e.ClaveAcceso)
                .HasMaxLength(50)
                .HasColumnName("claveAcceso");
            entity.Property(e => e.CodigoDocModificado).HasColumnName("codigoDocModificado");
            entity.Property(e => e.ContribuyenteEspecial)
                .HasMaxLength(20)
                .HasColumnName("contribuyenteEspecial");
            entity.Property(e => e.ContribuyenteRimpe)
                .HasDefaultValue(false)
                .HasColumnName("contribuyenteRimpe");
            entity.Property(e => e.CorreoEnviado)
                .HasDefaultValue(false)
                .HasColumnName("correoEnviado");
            entity.Property(e => e.DireccionEstablecimiento)
                .HasMaxLength(300)
                .HasColumnName("direccionEstablecimiento");
            entity.Property(e => e.DireccionMatriz)
                .HasMaxLength(300)
                .HasColumnName("direccionMatriz");
            entity.Property(e => e.EmisorRazonSocial)
                .HasMaxLength(300)
                .HasColumnName("emisorRazonSocial");
            entity.Property(e => e.EmisorRuc)
                .HasMaxLength(20)
                .HasColumnName("emisorRuc");
            entity.Property(e => e.Establecimiento).HasColumnName("establecimiento");
            entity.Property(e => e.ExentoIva)
                .HasPrecision(8, 2)
                .HasColumnName("exentoIva");
            entity.Property(e => e.FechaAutorizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaAutorizacion");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaEmision");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Ice)
                .HasPrecision(8, 2)
                .HasColumnName("ice");
            entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdDocumentoEmitir).HasColumnName("idDocumentoEmitir");
            entity.Property(e => e.IdEstablecimiento).HasColumnName("idEstablecimiento");
            entity.Property(e => e.IdPuntoEmision).HasColumnName("idPuntoEmision");
            entity.Property(e => e.IdTipoEstadoDocumento).HasColumnName("idTipoEstadoDocumento");
            entity.Property(e => e.IdTipoEstadoSri).HasColumnName("idTipoEstadoSri");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Irbpnr)
                .HasPrecision(8, 2)
                .HasColumnName("irbpnr");
            entity.Property(e => e.Isd)
                .HasPrecision(8, 2)
                .HasColumnName("isd");
            entity.Property(e => e.Iva12)
                .HasPrecision(8, 2)
                .HasColumnName("iva12");
            entity.Property(e => e.Mensaje).HasColumnName("mensaje");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .HasColumnName("moneda");
            entity.Property(e => e.ObligadoContabilidad).HasColumnName("obligadoContabilidad");
            entity.Property(e => e.PuntoEmision).HasColumnName("puntoEmision");
            entity.Property(e => e.ReceptorCorreo)
                .HasMaxLength(400)
                .HasColumnName("receptorCorreo");
            entity.Property(e => e.ReceptorDireccion)
                .HasMaxLength(500)
                .HasColumnName("receptorDireccion");
            entity.Property(e => e.ReceptorRazonSocial)
                .HasMaxLength(500)
                .HasColumnName("receptorRazonSocial");
            entity.Property(e => e.ReceptorRuc)
                .HasMaxLength(20)
                .HasColumnName("receptorRuc");
            entity.Property(e => e.ReceptorTelefono)
                .HasMaxLength(30)
                .HasColumnName("receptorTelefono");
            entity.Property(e => e.ReceptorTipoIdentificacion).HasColumnName("receptorTipoIdentificacion");
            entity.Property(e => e.RegimenMicroempresas).HasColumnName("regimenMicroempresas");
            entity.Property(e => e.RegimenRimpe).HasColumnName("regimenRimpe");
            entity.Property(e => e.ResolucionAgenteRetencion)
                .HasMaxLength(500)
                .HasColumnName("resolucionAgenteRetencion");
            entity.Property(e => e.Ruta)
                .HasMaxLength(1000)
                .HasColumnName("ruta");
            entity.Property(e => e.Saldo)
                .HasPrecision(8, 2)
                .HasColumnName("saldo");
            entity.Property(e => e.Secuencial).HasColumnName("secuencial");
            entity.Property(e => e.Subtotal0)
                .HasPrecision(8, 2)
                .HasColumnName("subtotal0");
            entity.Property(e => e.Subtotal12)
                .HasPrecision(8, 2)
                .HasColumnName("subtotal12");
            entity.Property(e => e.TipoDocumento).HasColumnName("tipoDocumento");
            entity.Property(e => e.TipoEmision).HasColumnName("tipoEmision");
            entity.Property(e => e.TotalDescuento)
                .HasPrecision(8, 2)
                .HasColumnName("totalDescuento");
            entity.Property(e => e.TotalImporte)
                .HasPrecision(8, 2)
                .HasColumnName("totalImporte");
            entity.Property(e => e.TotalSinImpuesto)
                .HasPrecision(8, 2)
                .HasColumnName("totalSinImpuesto");
            entity.Property(e => e.ValorRecibido)
                .HasPrecision(8, 2)
                .HasColumnName("valorRecibido");
            entity.Property(e => e.VersionXml)
                .HasMaxLength(10)
                .HasColumnName("versionXml");

            entity.HasOne(d => d.IdTipoEstadoDocumentoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTipoEstadoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("facturas_idTipoEstadoDocumento_fkey");

            entity.HasOne(d => d.IdTipoEstadoSriNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTipoEstadoSri)
                .HasConstraintName("facturas_idTipoEstadoSri_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("facturas_idUsuario_fkey");
        });

        modelBuilder.Entity<FormaPagos>(entity =>
        {
            entity.HasKey(e => e.IdFormaPago).HasName("formaPagos_pkey");

            entity.ToTable("formaPagos");

            entity.Property(e => e.IdFormaPago)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idFormaPago");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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

        modelBuilder.Entity<ImpuestoRetenciones>(entity =>
        {
            entity.HasKey(e => e.IdImpuestoRetencion).HasName("impuestoRetenciones_pkey");

            entity.ToTable("impuestoRetenciones");

            entity.Property(e => e.IdImpuestoRetencion)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idImpuestoRetencion");
            entity.Property(e => e.BaseImponible)
                .HasPrecision(8, 2)
                .HasColumnName("baseImponible");
            entity.Property(e => e.CodDocSustento)
                .HasPrecision(8, 2)
                .HasColumnName("codDocSustento");
            entity.Property(e => e.FechaEmisionDocSustento)
                .HasMaxLength(50)
                .HasColumnName("fechaEmisionDocSustento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdPorcentajeImpuestoRetencion).HasColumnName("idPorcentajeImpuestoRetencion");
            entity.Property(e => e.IdRetencion).HasColumnName("idRetencion");
            entity.Property(e => e.IdTipoValorRetencion).HasColumnName("idTipoValorRetencion");
            entity.Property(e => e.NumDocSustento)
                .HasPrecision(8, 2)
                .HasColumnName("numDocSustento");
            entity.Property(e => e.PorcentajeRetener)
                .HasPrecision(8, 2)
                .HasColumnName("porcentajeRetener");
            entity.Property(e => e.ValorRetenido)
                .HasPrecision(8, 2)
                .HasColumnName("valorRetenido");

            entity.HasOne(d => d.IdPorcentajeImpuestoRetencionNavigation).WithMany(p => p.ImpuestoRetenciones)
                .HasForeignKey(d => d.IdPorcentajeImpuestoRetencion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("impuestoRetenciones_idPorcentajeImpuestoRetencion_fkey");

            entity.HasOne(d => d.IdRetencionNavigation).WithMany(p => p.ImpuestoRetenciones)
                .HasForeignKey(d => d.IdRetencion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipoRetencionIva_idRetencion_fkey");

            entity.HasOne(d => d.IdTipoValorRetencionNavigation).WithMany(p => p.ImpuestoRetenciones)
                .HasForeignKey(d => d.IdTipoValorRetencion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("impuestoRetenciones_idTipoValorRetencion_fkey");
        });

        modelBuilder.Entity<InformacionAdicional>(entity =>
        {
            entity.HasKey(e => e.IdInformacionAdicional).HasName("informacionAdicional_pkey");

            entity.ToTable("informacionAdicional");

            entity.HasIndex(e => e.IdFactura, "IX_informacionAdicional_idFactura");

            entity.Property(e => e.IdInformacionAdicional)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idInformacionAdicional");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasMaxLength(500)
                .HasColumnName("valor");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.InformacionAdicional)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("informacionAdicional_idFactura_fkey");
        });

        modelBuilder.Entity<InformacionAdicionalRetencion>(entity =>
        {
            entity.HasKey(e => e.IdInformacionAdicionalRetencion).HasName("informacionAdicionalRetencion_pkey");

            entity.ToTable("informacionAdicionalRetencion");

            entity.Property(e => e.IdInformacionAdicionalRetencion)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idInformacionAdicionalRetencion");
            entity.Property(e => e.IdRetencion).HasColumnName("idRetencion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasMaxLength(500)
                .HasColumnName("valor");

            entity.HasOne(d => d.IdRetencionNavigation).WithMany(p => p.InformacionAdicionalRetencion)
                .HasForeignKey(d => d.IdRetencion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("informacionAdicionalRetencion_idRetencion_fkey");
        });

        modelBuilder.Entity<InformacionFirmas>(entity =>
        {
            entity.HasKey(e => e.IdInformacionFirma).HasName("informacionFirmas_pkey");

            entity.ToTable("informacionFirmas");

            entity.Property(e => e.IdInformacionFirma)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idInformacionFirma");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(500)
                .HasColumnName("codigo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .HasColumnName("identificacion");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Ruta)
                .HasMaxLength(1000)
                .HasColumnName("ruta");
        });

        modelBuilder.Entity<Ivas>(entity =>
        {
            entity.HasKey(e => e.IdIva).HasName("ivas_pkey");

            entity.ToTable("ivas");

            entity.Property(e => e.IdIva)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idIva");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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

        modelBuilder.Entity<Lotes>(entity =>
        {
            entity.HasKey(e => e.IdLote).HasName("lotes_pkey");

            entity.ToTable("lotes");

            entity.Property(e => e.IdLote).HasColumnName("idLote");
            entity.Property(e => e.Cantidad)
                .HasPrecision(8, 2)
                .HasColumnName("cantidad");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("lotes_idProducto_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("lotes_idUsuario_fkey");
        });

        modelBuilder.Entity<Personas>(entity =>
        {
            entity.HasKey(e => e.Identificacion).HasName("personas_pkey");

            entity.ToTable("personas");

            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .HasColumnName("identificacion");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.Direccion)
                .HasMaxLength(300)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.Razonsocial)
                .HasMaxLength(200)
                .HasColumnName("razonsocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.Tipoidentificacion)
                .HasMaxLength(5)
                .HasColumnName("tipoidentificacion");

            entity.HasOne(d => d.TipoidentificacionNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.Tipoidentificacion)
                .HasConstraintName("personas_tipoidentificacion_fkey");
        });

        modelBuilder.Entity<PorcentajeImpuestosRetenciones>(entity =>
        {
            entity.HasKey(e => e.IdPorcentajeImpuestoRetencion).HasName("porcentajeRetencion_pkey");

            entity.ToTable("porcentajeImpuestosRetenciones");

            entity.Property(e => e.IdPorcentajeImpuestoRetencion)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idPorcentajeImpuestoRetencion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdTipoValorRetencion).HasColumnName("idTipoValorRetencion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasPrecision(8, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.IdTipoValorRetencionNavigation).WithMany(p => p.PorcentajeImpuestosRetenciones)
                .HasForeignKey(d => d.IdTipoValorRetencion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipoRetencionIva_idTipoValorRetencion_fkey");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("productos_pkey");

            entity.ToTable("productos");

            entity.HasIndex(e => e.IdEmpresa, "IX_productos_idEmpresa");

            entity.HasIndex(e => e.IdIva, "IX_productos_idIva");

            entity.Property(e => e.IdProducto)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idProducto");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ActivoProducto)
                .HasDefaultValue(true)
                .HasColumnName("activoProducto");
            entity.Property(e => e.Cantidad)
                .HasPrecision(8, 2)
                .HasColumnName("cantidad");
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

        modelBuilder.Entity<Proformas>(entity =>
        {
            entity.HasKey(e => e.IdProforma).HasName("proformas_pkey");

            entity.ToTable("proformas");

            entity.HasIndex(e => e.IdEstablecimiento, "IX_proformas_idEstablecimiento");

            entity.HasIndex(e => e.IdPuntoEmision, "IX_proformas_idPuntoEmision");

            entity.HasIndex(e => e.IdUsuario, "IX_proformas_idUsuario");

            entity.Property(e => e.IdProforma)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idProforma");
            entity.Property(e => e.Ambiente).HasColumnName("ambiente");
            entity.Property(e => e.Establecimiento).HasColumnName("establecimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEstablecimiento).HasColumnName("idEstablecimiento");
            entity.Property(e => e.IdPuntoEmision).HasColumnName("idPuntoEmision");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .HasColumnName("moneda");
            entity.Property(e => e.PuntoEmision).HasColumnName("puntoEmision");
            entity.Property(e => e.ReceptorCorreo)
                .HasMaxLength(400)
                .HasColumnName("receptorCorreo");
            entity.Property(e => e.ReceptorDireccion)
                .HasMaxLength(500)
                .HasColumnName("receptorDireccion");
            entity.Property(e => e.ReceptorRazonSocial)
                .HasMaxLength(500)
                .HasColumnName("receptorRazonSocial");
            entity.Property(e => e.ReceptorRuc)
                .HasMaxLength(20)
                .HasColumnName("receptorRuc");
            entity.Property(e => e.ReceptorTelefono)
                .HasMaxLength(30)
                .HasColumnName("receptorTelefono");
            entity.Property(e => e.ReceptorTipoIdentificacion).HasColumnName("receptorTipoIdentificacion");
            entity.Property(e => e.Secuencial).HasColumnName("secuencial");
            entity.Property(e => e.Subtotal12)
                .HasPrecision(8, 2)
                .HasColumnName("subtotal12");
            entity.Property(e => e.TotalDescuento)
                .HasPrecision(8, 2)
                .HasColumnName("totalDescuento");
            entity.Property(e => e.TotalImporte)
                .HasPrecision(8, 2)
                .HasColumnName("totalImporte");
            entity.Property(e => e.TotalSinImpuesto)
                .HasPrecision(8, 2)
                .HasColumnName("totalSinImpuesto");

            entity.HasOne(d => d.IdEstablecimientoNavigation).WithMany(p => p.Proformas)
                .HasForeignKey(d => d.IdEstablecimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proformas_idEstablecimiento_fkey");

            entity.HasOne(d => d.IdPuntoEmisionNavigation).WithMany(p => p.Proformas)
                .HasForeignKey(d => d.IdPuntoEmision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proformas_idPuntoEmision_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Proformas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proformas_idUsuario_fkey");
        });

        modelBuilder.Entity<Proveedores>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("proveedores_pkey");

            entity.ToTable("proveedores");

            entity.HasIndex(e => e.IdCiudad, "IX_proveedores_idCiudad");

            entity.HasIndex(e => e.IdEmpresa, "IX_proveedores_idEmpresa");

            entity.HasIndex(e => e.IdTipoIdentificacion, "IX_proveedores_idTipoIdentificacion");

            entity.Property(e => e.IdProveedor)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idProveedor");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<PuntoEmisiones>(entity =>
        {
            entity.HasKey(e => e.IdPuntoEmision).HasName("puntoEmisiones_pkey");

            entity.ToTable("puntoEmisiones");

            entity.HasIndex(e => e.IdEmpresa, "IX_puntoEmisiones_idEmpresa");

            entity.Property(e => e.IdPuntoEmision)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idPuntoEmision");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(1000)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Predeterminado)
                .HasDefaultValue(false)
                .HasColumnName("predeterminado");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.PuntoEmisiones)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("puntoEmisiones_idEmpresa_fkey");
        });

        modelBuilder.Entity<Retenciones>(entity =>
        {
            entity.HasKey(e => e.IdRetencion).HasName("retenciones_pkey");

            entity.ToTable("retenciones");

            entity.Property(e => e.IdRetencion)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idRetencion");
            entity.Property(e => e.AgenteRetencion)
                .HasMaxLength(300)
                .HasColumnName("agenteRetencion");
            entity.Property(e => e.Ambiente).HasColumnName("ambiente");
            entity.Property(e => e.ClaveAcceso)
                .HasMaxLength(50)
                .HasColumnName("claveAcceso");
            entity.Property(e => e.CodigoDocumento).HasColumnName("codigoDocumento");
            entity.Property(e => e.DireccionMatriz)
                .HasMaxLength(300)
                .HasColumnName("direccionMatriz");
            entity.Property(e => e.EmisorNombreComercial)
                .HasMaxLength(300)
                .HasColumnName("emisorNombreComercial");
            entity.Property(e => e.EmisorRazonSocial)
                .HasMaxLength(300)
                .HasColumnName("emisorRazonSocial");
            entity.Property(e => e.EmisorRuc)
                .HasMaxLength(20)
                .HasColumnName("emisorRuc");
            entity.Property(e => e.Establecimiento).HasColumnName("establecimiento");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaEmision");
            entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdDocumentoEmitir).HasColumnName("idDocumentoEmitir");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdEstablecimiento).HasColumnName("idEstablecimiento");
            entity.Property(e => e.IdPuntoEmision).HasColumnName("idPuntoEmision");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");
            entity.Property(e => e.IdTipoEstadoDocumento).HasColumnName("idTipoEstadoDocumento");
            entity.Property(e => e.IdTipoEstadoSri).HasColumnName("idTipoEstadoSri");
            entity.Property(e => e.IdTipoIdenticacion).HasColumnName("idTipoIdenticacion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdentificacionSujetoRetenido)
                .HasMaxLength(20)
                .HasColumnName("identificacionSujetoRetenido");
            entity.Property(e => e.ObligadoContabilidad).HasColumnName("obligadoContabilidad");
            entity.Property(e => e.PeriodoFiscal)
                .HasMaxLength(20)
                .HasColumnName("periodoFiscal");
            entity.Property(e => e.PuntoEmision).HasColumnName("puntoEmision");
            entity.Property(e => e.RazonSocialSujetoRetenido)
                .HasMaxLength(500)
                .HasColumnName("razonSocialSujetoRetenido");
            entity.Property(e => e.Ruta)
                .HasMaxLength(1000)
                .HasColumnName("ruta");
            entity.Property(e => e.Secuencial).HasColumnName("secuencial");
            entity.Property(e => e.TipoDocumento).HasColumnName("tipoDocumento");
            entity.Property(e => e.TipoEmision).HasColumnName("tipoEmision");
            entity.Property(e => e.TipoIdentificacionSujetoRetenido)
                .HasMaxLength(20)
                .HasColumnName("tipoIdentificacionSujetoRetenido");
            entity.Property(e => e.VersionXml)
                .HasMaxLength(10)
                .HasColumnName("versionXml");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdCiudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idCiudad_fkey");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idCliente_fkey");

            entity.HasOne(d => d.IdDocumentoEmitirNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdDocumentoEmitir)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idDocumentoEmitir_fkey");

            entity.HasOne(d => d.IdEstablecimientoNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdEstablecimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idEstablecimiento_fkey");

            entity.HasOne(d => d.IdPuntoEmisionNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdPuntoEmision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idPuntoEmision_fkey");

            entity.HasOne(d => d.IdTipoEstadoDocumentoNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdTipoEstadoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idTipoEstadoDocumento_fkey");

            entity.HasOne(d => d.IdTipoEstadoSriNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdTipoEstadoSri)
                .HasConstraintName("retenciones_idTipoEstadoSri_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idUsuario_fkey");
        });

        modelBuilder.Entity<Secuenciales>(entity =>
        {
            entity.HasKey(e => e.IdSecuencial).HasName("secuenciales_pkey");

            entity.ToTable("secuenciales");

            entity.HasIndex(e => e.IdEmpresa, "IX_secuenciales_idEmpresa");

            entity.HasIndex(e => e.IdTipoDocumento, "IX_secuenciales_idTipoDocumento");

            entity.Property(e => e.IdSecuencial)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idSecuencial");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");
            entity.Property(e => e.Nombre).HasColumnName("nombre");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Secuenciales)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("secuenciales_idEmpresa_fkey");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Secuenciales)
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("secuenciales_idTipoDocumento_fkey");
        });

        modelBuilder.Entity<SecuencialesProformas>(entity =>
        {
            entity.HasKey(e => e.IdSecuencialesProforma).HasName("secuencialesProformas_pkey");

            entity.ToTable("secuencialesProformas");

            entity.HasIndex(e => e.IdEmpresa, "IX_secuencialesProformas_idEmpresa");

            entity.HasIndex(e => e.IdTipoDocumento, "IX_secuencialesProformas_idTipoDocumento");

            entity.Property(e => e.IdSecuencialesProforma)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idSecuencialesProforma");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");
            entity.Property(e => e.Nombre).HasColumnName("nombre");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.SecuencialesProformas)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("secuencialesProformas_idEmpresa_fkey");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.SecuencialesProformas)
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("secuencialesProformas_idTipoDocumento_fkey");
        });

        modelBuilder.Entity<Sriambientes>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("sriambientes_pkey");

            entity.ToTable("sriambientes");

            entity.Property(e => e.Codigo)
                .HasMaxLength(3)
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Ambiente)
                .HasMaxLength(50)
                .HasColumnName("ambiente");
        });

        modelBuilder.Entity<Sriestados>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("sriestados_pkey");

            entity.ToTable("sriestados");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Srifacturas>(entity =>
        {
            entity.HasKey(e => e.Idfactura).HasName("srifacturas_pkey");

            entity.ToTable("srifacturas");

            entity.Property(e => e.Idfactura).HasColumnName("idfactura");
            entity.Property(e => e.Ambiente)
                .HasMaxLength(5)
                .HasColumnName("ambiente");
            entity.Property(e => e.Claveacceso)
                .HasMaxLength(64)
                .HasColumnName("claveacceso");
            entity.Property(e => e.Coddoc)
                .HasMaxLength(5)
                .HasColumnName("coddoc");
            entity.Property(e => e.Codigoestado)
                .HasDefaultValue(0)
                .HasColumnName("codigoestado");
            entity.Property(e => e.Compra)
                .HasDefaultValue(false)
                .HasColumnName("compra");
            entity.Property(e => e.Contribuyenteespecial)
                .HasMaxLength(5)
                .HasColumnName("contribuyenteespecial");
            entity.Property(e => e.Direstablecimiento)
                .HasMaxLength(300)
                .HasColumnName("direstablecimiento");
            entity.Property(e => e.Dirmatriz)
                .HasMaxLength(300)
                .HasColumnName("dirmatriz");
            entity.Property(e => e.Estab)
                .HasMaxLength(5)
                .HasColumnName("estab");
            entity.Property(e => e.Fechaautorizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaautorizacion");
            entity.Property(e => e.Fechaemision).HasColumnName("fechaemision");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Idempresa).HasColumnName("idempresa");
            entity.Property(e => e.Identificacioncomprador)
                .HasMaxLength(20)
                .HasColumnName("identificacioncomprador");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Importetotal)
                .HasPrecision(10, 2)
                .HasColumnName("importetotal");
            entity.Property(e => e.Moneda)
                .HasMaxLength(20)
                .HasColumnName("moneda");
            entity.Property(e => e.Nombrecomercial)
                .HasMaxLength(200)
                .HasColumnName("nombrecomercial");
            entity.Property(e => e.Obligadocontabilidad)
                .HasMaxLength(5)
                .HasColumnName("obligadocontabilidad");
            entity.Property(e => e.Propina)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("propina");
            entity.Property(e => e.Ptoemi)
                .HasMaxLength(5)
                .HasColumnName("ptoemi");
            entity.Property(e => e.Razonsocial)
                .HasMaxLength(200)
                .HasColumnName("razonsocial");
            entity.Property(e => e.Razonsocialcomprador)
                .HasMaxLength(200)
                .HasColumnName("razonsocialcomprador");
            entity.Property(e => e.Ruc)
                .HasMaxLength(20)
                .HasColumnName("ruc");
            entity.Property(e => e.Secuencial)
                .HasMaxLength(12)
                .HasColumnName("secuencial");
            entity.Property(e => e.Tipoidentificacioncomprador)
                .HasMaxLength(5)
                .HasColumnName("tipoidentificacioncomprador");
            entity.Property(e => e.Totaldescuento)
                .HasPrecision(10, 2)
                .HasColumnName("totaldescuento");
            entity.Property(e => e.Totalsinimpuesto)
                .HasPrecision(10, 2)
                .HasColumnName("totalsinimpuesto");
            entity.Property(e => e.Version)
                .HasMaxLength(10)
                .HasColumnName("version");

            entity.HasOne(d => d.AmbienteNavigation).WithMany(p => p.Srifacturas)
                .HasForeignKey(d => d.Ambiente)
                .HasConstraintName("srifacturas_ambiente_fkey");

            entity.HasOne(d => d.CoddocNavigation).WithMany(p => p.Srifacturas)
                .HasForeignKey(d => d.Coddoc)
                .HasConstraintName("srifacturas_coddoc_fkey");

            entity.HasOne(d => d.IdentificacioncompradorNavigation).WithMany(p => p.Srifacturas)
                .HasForeignKey(d => d.Identificacioncomprador)
                .HasConstraintName("srifacturas_identificacioncomprador_fkey");

            entity.HasOne(d => d.MonedaNavigation).WithMany(p => p.Srifacturas)
                .HasForeignKey(d => d.Moneda)
                .HasConstraintName("srifacturas_moneda_fkey");

            entity.HasOne(d => d.TipoidentificacioncompradorNavigation).WithMany(p => p.Srifacturas)
                .HasForeignKey(d => d.Tipoidentificacioncomprador)
                .HasConstraintName("srifacturas_tipoidentificacioncomprador_fkey");
        });

        modelBuilder.Entity<Sriformaspagos>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("sriformaspagos_pkey");

            entity.ToTable("sriformaspagos");

            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Formapago)
                .HasMaxLength(200)
                .HasColumnName("formapago");
        });

        modelBuilder.Entity<Srimonedas>(entity =>
        {
            entity.HasKey(e => e.Moneda).HasName("srimonedas_pkey");

            entity.ToTable("srimonedas");

            entity.Property(e => e.Moneda)
                .HasMaxLength(20)
                .HasColumnName("moneda");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
        });

        modelBuilder.Entity<Sriporcentajesimpuestos>(entity =>
        {
            entity.HasKey(e => e.Codigoporcentaje).HasName("sriporcentajesimpuestos_pkey");

            entity.ToTable("sriporcentajesimpuestos");

            entity.HasIndex(e => e.Codigo, "sriporcentajesimpuestos_codigo_key").IsUnique();

            entity.Property(e => e.Codigoporcentaje)
                .HasMaxLength(5)
                .HasColumnName("codigoporcentaje");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(5, 2)
                .HasColumnName("porcentaje");
            entity.Property(e => e.Valor)
                .HasPrecision(5, 2)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<Sritiposdocumentos>(entity =>
        {
            entity.HasKey(e => e.Coddoc).HasName("sritiposdocumentos_pkey");

            entity.ToTable("sritiposdocumentos");

            entity.Property(e => e.Coddoc)
                .HasMaxLength(5)
                .HasColumnName("coddoc");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Tipodocumento)
                .HasMaxLength(50)
                .HasColumnName("tipodocumento");
        });

        modelBuilder.Entity<Sritiposidentificaciones>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("sritiposidentificaciones_pkey");

            entity.ToTable("sritiposidentificaciones");

            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Tipoidentificacion)
                .HasMaxLength(50)
                .HasColumnName("tipoidentificacion");
        });

        modelBuilder.Entity<Sriunidadestiempos>(entity =>
        {
            entity.HasKey(e => e.Unidadtiempo).HasName("sriunidadestiempos_pkey");

            entity.ToTable("sriunidadestiempos");

            entity.Property(e => e.Unidadtiempo)
                .HasMaxLength(50)
                .HasColumnName("unidadtiempo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .HasColumnName("codigo");
        });

        modelBuilder.Entity<TiempoFormaPagos>(entity =>
        {
            entity.HasKey(e => e.IdTiempoFormaPago).HasName("tiempoFormaPagos_pkey");

            entity.ToTable("tiempoFormaPagos");

            entity.Property(e => e.IdTiempoFormaPago)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTiempoFormaPago");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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

        modelBuilder.Entity<TipoDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("tipoDocumentos_pkey");

            entity.ToTable("tipoDocumentos");

            entity.Property(e => e.IdTipoDocumento)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTipoDocumento");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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

        modelBuilder.Entity<TipoEstadoDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoEstadoDocumento).HasName("tipoEstadoDocumentos_pkey");

            entity.ToTable("tipoEstadoDocumentos");

            entity.Property(e => e.IdTipoEstadoDocumento).HasColumnName("idTipoEstadoDocumento");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
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

        modelBuilder.Entity<TipoEstadoSri>(entity =>
        {
            entity.HasKey(e => e.IdTipoEstadoSri).HasName("tipoEstadoSri_pkey");

            entity.ToTable("tipoEstadoSri");

            entity.Property(e => e.IdTipoEstadoSri).HasColumnName("idTipoEstadoSri");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
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

        modelBuilder.Entity<TipoIdentificaciones>(entity =>
        {
            entity.HasKey(e => e.IdTipoIdentificacion).HasName("tipoIdentificaciones_pkey");

            entity.ToTable("tipoIdentificaciones");

            entity.Property(e => e.IdTipoIdentificacion)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
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

        modelBuilder.Entity<TipoIdentificacionesGeneracionDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoIdentificacionesGeneracionDocumentos).HasName("tipoIdentificacionesGeneracionDocumentos_pkey");

            entity.ToTable("tipoIdentificacionesGeneracionDocumentos");

            entity.Property(e => e.IdTipoIdentificacionesGeneracionDocumentos)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTipoIdentificacionesGeneracionDocumentos");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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
                .HasDefaultValue(false)
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

        modelBuilder.Entity<TipoValorRetenciones>(entity =>
        {
            entity.HasKey(e => e.IdTipoValorRetencion).HasName("tipoRetencion_pkey");

            entity.ToTable("tipoValorRetenciones");

            entity.Property(e => e.IdTipoValorRetencion)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTipoValorRetencion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
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

        modelBuilder.Entity<UsuarioEmpresas>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioEmpresas).HasName("usuarioEmpresas_pkey");

            entity.ToTable("usuarioEmpresas");

            entity.HasIndex(e => e.IdEmpresa, "IX_usuarioEmpresas_idEmpresa");

            entity.HasIndex(e => e.IdUsuario, "IX_usuarioEmpresas_idUsuario");

            entity.Property(e => e.IdUsuarioEmpresas)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idUsuarioEmpresas");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.UsuarioEmpresas)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarioEmpresas_idEmpresa_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioEmpresas)
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
                .HasDefaultValue(true)
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
