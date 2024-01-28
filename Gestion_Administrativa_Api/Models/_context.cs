using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Models;

public partial class _context : DbContext
{
    public _context(DbContextOptions<_context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cajas> Cajas { get; set; }

    public virtual DbSet<Ciudades> Ciudades { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<DenominacionesDinero> DenominacionesDinero { get; set; }

    public virtual DbSet<DetalleFacturas> DetalleFacturas { get; set; }

    public virtual DbSet<DetalleFormaPagos> DetalleFormaPagos { get; set; }

    public virtual DbSet<DetallePrecioProductos> DetallePrecioProductos { get; set; }

    public virtual DbSet<DetalleProformas> DetalleProformas { get; set; }

    public virtual DbSet<DetallesCajas> DetallesCajas { get; set; }

    public virtual DbSet<DetallesCajasCierres> DetallesCajasCierres { get; set; }

    public virtual DbSet<DocumentosEmitir> DocumentosEmitir { get; set; }

    public virtual DbSet<Empleados> Empleados { get; set; }

    public virtual DbSet<Empresas> Empresas { get; set; }

    public virtual DbSet<ErrorLogs> ErrorLogs { get; set; }

    public virtual DbSet<Establecimientos> Establecimientos { get; set; }

    public virtual DbSet<Facturas> Facturas { get; set; }

    public virtual DbSet<FormaPagos> FormaPagos { get; set; }

    public virtual DbSet<ImpuestoRetenciones> ImpuestoRetenciones { get; set; }

    public virtual DbSet<InformacionAdicional> InformacionAdicional { get; set; }

    public virtual DbSet<InformacionAdicionalRetencion> InformacionAdicionalRetencion { get; set; }

    public virtual DbSet<InformacionFirmas> InformacionFirmas { get; set; }

    public virtual DbSet<Ivas> Ivas { get; set; }

    public virtual DbSet<Lotes> Lotes { get; set; }

    public virtual DbSet<PorcentajeImpuestosRetenciones> PorcentajeImpuestosRetenciones { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<ProductosProveedores> ProductosProveedores { get; set; }

    public virtual DbSet<Proformas> Proformas { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }

    public virtual DbSet<Provincias> Provincias { get; set; }

    public virtual DbSet<PuntoEmisiones> PuntoEmisiones { get; set; }

    public virtual DbSet<Retenciones> Retenciones { get; set; }

    public virtual DbSet<Secuenciales> Secuenciales { get; set; }

    public virtual DbSet<SecuencialesProformas> SecuencialesProformas { get; set; }

    public virtual DbSet<SriAmbientes> SriAmbientes { get; set; }

    public virtual DbSet<SriCamposAdicionales> SriCamposAdicionales { get; set; }

    public virtual DbSet<SriDetallesFacturas> SriDetallesFacturas { get; set; }

    public virtual DbSet<SriDetallesFacturasImpuestos> SriDetallesFacturasImpuestos { get; set; }

    public virtual DbSet<SriEstados> SriEstados { get; set; }

    public virtual DbSet<SriFacturas> SriFacturas { get; set; }

    public virtual DbSet<SriFormasPagos> SriFormasPagos { get; set; }

    public virtual DbSet<SriMonedas> SriMonedas { get; set; }

    public virtual DbSet<SriPagos> SriPagos { get; set; }

    public virtual DbSet<SriPersonas> SriPersonas { get; set; }

    public virtual DbSet<SriPrecios> SriPrecios { get; set; }

    public virtual DbSet<SriProductos> SriProductos { get; set; }

    public virtual DbSet<SriTarifasImpuestos> SriTarifasImpuestos { get; set; }

    public virtual DbSet<SriTiposDocumentos> SriTiposDocumentos { get; set; }

    public virtual DbSet<SriTiposIdentificaciones> SriTiposIdentificaciones { get; set; }

    public virtual DbSet<SriTotalesConImpuestos> SriTotalesConImpuestos { get; set; }

    public virtual DbSet<SriUnidadesTiempos> SriUnidadesTiempos { get; set; }

    public virtual DbSet<TiempoFormaPagos> TiempoFormaPagos { get; set; }

    public virtual DbSet<TipoDocumentos> TipoDocumentos { get; set; }

    public virtual DbSet<TipoEstadoDocumentos> TipoEstadoDocumentos { get; set; }

    public virtual DbSet<TipoEstadoSri> TipoEstadoSri { get; set; }

    public virtual DbSet<TipoIdentificaciones> TipoIdentificaciones { get; set; }

    public virtual DbSet<TipoIdentificacionesGeneracionDocumentos> TipoIdentificacionesGeneracionDocumentos { get; set; }

    public virtual DbSet<TipoNegocios> TipoNegocios { get; set; }

    public virtual DbSet<TipoValorRetenciones> TipoValorRetenciones { get; set; }

    public virtual DbSet<TiposDenominacionesDinero> TiposDenominacionesDinero { get; set; }

    public virtual DbSet<UsuarioEmpresas> UsuarioEmpresas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Cajas>(entity =>
        {
            entity.HasKey(e => e.IdCaja).HasName("PK__Cajas__8BC79B34440E38E5");

            entity.Property(e => e.IdCaja).HasColumnName("idCaja");
            entity.Property(e => e.Detallado)
                .HasDefaultValue(false)
                .HasColumnName("detallado");
            entity.Property(e => e.FechaCierre)
                .HasColumnType("datetime")
                .HasColumnName("fechaCierre");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.TotalApertura)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalApertura");
            entity.Property(e => e.TotalCierre)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalCierre");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Cajas)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK__Cajas__idEmpresa__3AB788A8");
        });

        modelBuilder.Entity<Ciudades>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("ciudades_pkey");

            entity.ToTable("ciudades");

            entity.Property(e => e.IdCiudad)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idCiudad");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdProvincia).HasColumnName("idProvincia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
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
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idCliente");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Observacion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacion");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(500)
                .IsUnicode(false)
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

        modelBuilder.Entity<DenominacionesDinero>(entity =>
        {
            entity.HasKey(e => e.IdDenominacion).HasName("PK__Denomina__80C12401D636482D");

            entity.Property(e => e.IdDenominacion).HasColumnName("idDenominacion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.IdTipoDenominacion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("idTipoDenominacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdTipoDenominacionNavigation).WithMany(p => p.DenominacionesDinero)
                .HasForeignKey(d => d.IdTipoDenominacion)
                .HasConstraintName("FK__Denominac__idTip__4BE214AA");
        });

        modelBuilder.Entity<DetalleFacturas>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura).HasName("detalleFacturas_pkey");

            entity.ToTable("detalleFacturas");

            entity.Property(e => e.IdDetalleFactura)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idDetalleFactura");
            entity.Property(e => e.Cantidad)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.Descuento)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.FechoRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechoRegistro");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdIva).HasColumnName("idIva");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Porcentaje)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("porcentaje");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Subtotal)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("total");
            entity.Property(e => e.ValorPorcentaje)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("valorPorcentaje");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleFacturas_idFactura_fkey");

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdIva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idIva_");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleFacturas_");
        });

        modelBuilder.Entity<DetalleFormaPagos>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFormaPago).HasName("detalleFormaPagos_pkey");

            entity.ToTable("detalleFormaPagos");

            entity.Property(e => e.IdDetalleFormaPago)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idDetalleFormaPago");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdFormaPago).HasColumnName("idFormaPago");
            entity.Property(e => e.IdTiempoFormaPago).HasColumnName("idTiempoFormaPago");
            entity.Property(e => e.Plazo)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("plazo");
            entity.Property(e => e.Valor)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFormaPagos)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleFormaPagos_idFactura_fkey");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.DetalleFormaPagos)
                .HasForeignKey(d => d.IdFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idFormaPago_");

            entity.HasOne(d => d.IdTiempoFormaPagoNavigation).WithMany(p => p.DetalleFormaPagos)
                .HasForeignKey(d => d.IdTiempoFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleFormaPagos_idTiempoFormaPago_fkey");
        });

        modelBuilder.Entity<DetallePrecioProductos>(entity =>
        {
            entity.HasKey(e => e.IdDetallePrecioProducto).HasName("detallePrecioProductos_pkey");

            entity.ToTable("detallePrecioProductos");

            entity.Property(e => e.IdDetallePrecioProducto)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idDetallePrecioProducto");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdIva).HasColumnName("idIva");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Porcentaje)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("porcentaje");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("total");
            entity.Property(e => e.TotalIva)
                .HasColumnType("numeric(8, 2)")
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

            entity.Property(e => e.IdDetalleProforma)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idDetalleProforma");
            entity.Property(e => e.Cantidad)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.Descuento)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.IdIva).HasColumnName("idIva");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdProforma).HasColumnName("idProforma");
            entity.Property(e => e.Porcentaje)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("porcentaje");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Subtotal)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("total");
            entity.Property(e => e.ValorPorcentaje)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("valorPorcentaje");

            entity.HasOne(d => d.IdProformaNavigation).WithMany(p => p.DetalleProformas)
                .HasForeignKey(d => d.IdProforma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idProforma");
        });

        modelBuilder.Entity<DetallesCajas>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCaja).HasName("PK__Detalles__0D031FF916F49A74");

            entity.Property(e => e.IdDetalleCaja).HasColumnName("idDetalleCaja");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCaja).HasColumnName("idCaja");
            entity.Property(e => e.IdDenominacion).HasColumnName("idDenominacion");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdCajaNavigation).WithMany(p => p.DetallesCajas)
                .HasForeignKey(d => d.IdCaja)
                .HasConstraintName("FK__DetallesC__idCaj__4EBE8155");

            entity.HasOne(d => d.IdDenominacionNavigation).WithMany(p => p.DetallesCajas)
                .HasForeignKey(d => d.IdDenominacion)
                .HasConstraintName("FK__DetallesC__idDen__4FB2A58E");
        });

        modelBuilder.Entity<DetallesCajasCierres>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCajaCierre).HasName("PK__Detalles__B96398E0F455CA0C");

            entity.Property(e => e.IdDetalleCajaCierre).HasColumnName("idDetalleCajaCierre");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCaja).HasColumnName("idCaja");
            entity.Property(e => e.IdDenominacion).HasColumnName("idDenominacion");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdCajaNavigation).WithMany(p => p.DetallesCajasCierres)
                .HasForeignKey(d => d.IdCaja)
                .HasConstraintName("FK__DetallesC__idCaj__528F1239");

            entity.HasOne(d => d.IdDenominacionNavigation).WithMany(p => p.DetallesCajasCierres)
                .HasForeignKey(d => d.IdDenominacion)
                .HasConstraintName("FK__DetallesC__idDen__53833672");
        });

        modelBuilder.Entity<DocumentosEmitir>(entity =>
        {
            entity.HasKey(e => e.IdDocumentoEmitir).HasName("documentosEmitir_pkey");

            entity.ToTable("documentosEmitir");

            entity.Property(e => e.IdDocumentoEmitir)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idDocumentoEmitir");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.DocumentosEmitir)
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("documentosEmitir_idTipoDocumento_fkey");
        });

        modelBuilder.Entity<Empleados>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("empleados_pkey");

            entity.ToTable("empleados");

            entity.Property(e => e.IdEmpleado)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idEmpleado");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Observacion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacion");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(500)
                .IsUnicode(false)
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
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idEmpresa");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.AgenteRetencion).HasColumnName("agenteRetencion");
            entity.Property(e => e.DireccionMatriz)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("direccionMatriz");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdInformacionFirma).HasColumnName("idInformacionFirma");
            entity.Property(e => e.IdTipoNegocio).HasColumnName("idTipoNegocio");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.LlevaContabilidad).HasColumnName("llevaContabilidad");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.RegimenMicroEmpresas).HasColumnName("regimenMicroEmpresas");
            entity.Property(e => e.RegimenRimpe).HasColumnName("regimenRimpe");
            entity.Property(e => e.ResolucionAgenteRetencion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("resolucionAgenteRetencion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdInformacionFirmaNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdInformacionFirma)
                .HasConstraintName("fk_empresas_informacionfirmas");

            entity.HasOne(d => d.IdTipoNegocioNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdTipoNegocio)
                .HasConstraintName("empresas_TipoNegocios");
        });

        modelBuilder.Entity<ErrorLogs>(entity =>
        {
            entity.HasKey(e => e.IdError).HasName("PK__ErrorLog__6FC78380515670CB");

            entity.Property(e => e.IdError).HasColumnName("idError");
            entity.Property(e => e.Error)
                .HasMaxLength(7300)
                .IsUnicode(false)
                .HasColumnName("error");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<Establecimientos>(entity =>
        {
            entity.HasKey(e => e.IdEstablecimiento).HasName("establecimientos_pkey");

            entity.ToTable("establecimientos");

            entity.Property(e => e.IdEstablecimiento)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idEstablecimiento");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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

            entity.Property(e => e.IdFactura)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idFactura");
            entity.Property(e => e.AgenteRetencion)
                .HasDefaultValue(false)
                .HasColumnName("agenteRetencion");
            entity.Property(e => e.Ambiente).HasColumnName("ambiente");
            entity.Property(e => e.Cambio)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("cambio");
            entity.Property(e => e.ClaveAcceso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("claveAcceso");
            entity.Property(e => e.CodigoDocModificado).HasColumnName("codigoDocModificado");
            entity.Property(e => e.ContribuyenteEspecial)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contribuyenteEspecial");
            entity.Property(e => e.ContribuyenteRimpe)
                .HasDefaultValue(false)
                .HasColumnName("contribuyenteRimpe");
            entity.Property(e => e.CorreoEnviado)
                .HasDefaultValue(false)
                .HasColumnName("correoEnviado");
            entity.Property(e => e.DireccionEstablecimiento)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("direccionEstablecimiento");
            entity.Property(e => e.DireccionMatriz)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("direccionMatriz");
            entity.Property(e => e.EmisorRazonSocial)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("emisorRazonSocial");
            entity.Property(e => e.EmisorRuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("emisorRuc");
            entity.Property(e => e.Establecimiento).HasColumnName("establecimiento");
            entity.Property(e => e.ExentoIva)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("exentoIva");
            entity.Property(e => e.FechaAutorizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaAutorizacion");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaEmision");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Ice)
                .HasColumnType("numeric(8, 2)")
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
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("irbpnr");
            entity.Property(e => e.Isd)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("isd");
            entity.Property(e => e.Iva12)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("iva12");
            entity.Property(e => e.Mensaje)
                .IsUnicode(false)
                .HasColumnName("mensaje");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("moneda");
            entity.Property(e => e.ObligadoContabilidad).HasColumnName("obligadoContabilidad");
            entity.Property(e => e.PuntoEmision).HasColumnName("puntoEmision");
            entity.Property(e => e.ReceptorCorreo)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("receptorCorreo");
            entity.Property(e => e.ReceptorDireccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("receptorDireccion");
            entity.Property(e => e.ReceptorRazonSocial)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("receptorRazonSocial");
            entity.Property(e => e.ReceptorRuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("receptorRuc");
            entity.Property(e => e.ReceptorTelefono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("receptorTelefono");
            entity.Property(e => e.ReceptorTipoIdentificacion).HasColumnName("receptorTipoIdentificacion");
            entity.Property(e => e.RegimenMicroempresas).HasColumnName("regimenMicroempresas");
            entity.Property(e => e.RegimenRimpe).HasColumnName("regimenRimpe");
            entity.Property(e => e.ResolucionAgenteRetencion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("resolucionAgenteRetencion");
            entity.Property(e => e.Ruta)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ruta");
            entity.Property(e => e.Saldo)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("saldo");
            entity.Property(e => e.Secuencial).HasColumnName("secuencial");
            entity.Property(e => e.Subtotal0)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("subtotal0");
            entity.Property(e => e.Subtotal12)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("subtotal12");
            entity.Property(e => e.TipoDocumento).HasColumnName("tipoDocumento");
            entity.Property(e => e.TipoEmision).HasColumnName("tipoEmision");
            entity.Property(e => e.TotalDescuento)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("totalDescuento");
            entity.Property(e => e.TotalImporte)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("totalImporte");
            entity.Property(e => e.TotalSinImpuesto)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("totalSinImpuesto");
            entity.Property(e => e.ValorRecibido)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("valorRecibido");
            entity.Property(e => e.VersionXml)
                .HasMaxLength(10)
                .IsUnicode(false)
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
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idFormaPago");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ImpuestoRetenciones>(entity =>
        {
            entity.HasKey(e => e.IdImpuestoRetencion).HasName("impuestoRetenciones_pkey");

            entity.ToTable("impuestoRetenciones");

            entity.Property(e => e.IdImpuestoRetencion)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idImpuestoRetencion");
            entity.Property(e => e.BaseImponible)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("baseImponible");
            entity.Property(e => e.CodDocSustento)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("codDocSustento");
            entity.Property(e => e.FechaEmisionDocSustento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fechaEmisionDocSustento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdPorcentajeImpuestoRetencion).HasColumnName("idPorcentajeImpuestoRetencion");
            entity.Property(e => e.IdRetencion).HasColumnName("idRetencion");
            entity.Property(e => e.IdTipoValorRetencion).HasColumnName("idTipoValorRetencion");
            entity.Property(e => e.NumDocSustento)
                .HasMaxLength(100)
                .HasColumnName("numDocSustento");
            entity.Property(e => e.PorcentajeRetener)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("porcentajeRetener");
            entity.Property(e => e.ValorRetenido)
                .HasColumnType("numeric(8, 2)")
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

            entity.Property(e => e.IdInformacionAdicional)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idInformacionAdicional");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasMaxLength(500)
                .IsUnicode(false)
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
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idInformacionAdicionalRetencion");
            entity.Property(e => e.IdRetencion).HasColumnName("idRetencion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasMaxLength(500)
                .IsUnicode(false)
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
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idInformacionFirma");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Ruta)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ruta");
        });

        modelBuilder.Entity<Ivas>(entity =>
        {
            entity.HasKey(e => e.IdIva).HasName("ivas_pkey");

            entity.ToTable("ivas");

            entity.Property(e => e.IdIva)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idIva");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("valor");
        });

        modelBuilder.Entity<Lotes>(entity =>
        {
            entity.HasKey(e => e.IdLote).HasName("PK__lotes__1B91FFCB655DA6A5");

            entity.ToTable("lotes");

            entity.Property(e => e.IdLote).HasColumnName("idLote");
            entity.Property(e => e.Cantidad)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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

        modelBuilder.Entity<PorcentajeImpuestosRetenciones>(entity =>
        {
            entity.HasKey(e => e.IdPorcentajeImpuestoRetencion).HasName("porcentajeRetencion_pkey");

            entity.ToTable("porcentajeImpuestosRetenciones");

            entity.Property(e => e.IdPorcentajeImpuestoRetencion)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idPorcentajeImpuestoRetencion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdTipoValorRetencion).HasColumnName("idTipoValorRetencion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasColumnType("numeric(8, 2)")
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

            entity.Property(e => e.IdProducto)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idProducto");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ActivoProducto)
                .HasDefaultValue(true)
                .HasColumnName("activoProducto");
            entity.Property(e => e.Cantidad)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.Codigo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdIva).HasColumnName("idIva");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.TotalIva)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("totalIva");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("productos_empresas");

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdIva)
                .HasConstraintName("productos_idIva_fkey");
        });

        modelBuilder.Entity<ProductosProveedores>(entity =>
        {
            entity.HasKey(e => e.IdProductoProveedor).HasName("PK__Producto__358285855AECBB43");

            entity.Property(e => e.IdProductoProveedor).HasColumnName("idProductoProveedor");
            entity.Property(e => e.CodigoPrincipal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigoPrincipal");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacion");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("ProductosProveedores_idProducto_fkey");

            entity.HasOne(d => d.IdentificacionNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.Identificacion)
                .HasConstraintName("ProductosProveedores_identificacion_fkey");
        });

        modelBuilder.Entity<Proformas>(entity =>
        {
            entity.HasKey(e => e.IdProforma).HasName("proformas_pkey");

            entity.ToTable("proformas");

            entity.Property(e => e.IdProforma)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idProforma");
            entity.Property(e => e.Ambiente).HasColumnName("ambiente");
            entity.Property(e => e.Establecimiento).HasColumnName("establecimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEstablecimiento).HasColumnName("idEstablecimiento");
            entity.Property(e => e.IdPuntoEmision).HasColumnName("idPuntoEmision");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("moneda");
            entity.Property(e => e.PuntoEmision).HasColumnName("puntoEmision");
            entity.Property(e => e.ReceptorCorreo)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("receptorCorreo");
            entity.Property(e => e.ReceptorDireccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("receptorDireccion");
            entity.Property(e => e.ReceptorRazonSocial)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("receptorRazonSocial");
            entity.Property(e => e.ReceptorRuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("receptorRuc");
            entity.Property(e => e.ReceptorTelefono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("receptorTelefono");
            entity.Property(e => e.ReceptorTipoIdentificacion).HasColumnName("receptorTipoIdentificacion");
            entity.Property(e => e.Secuencial).HasColumnName("secuencial");
            entity.Property(e => e.Subtotal12)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("subtotal12");
            entity.Property(e => e.TotalDescuento)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("totalDescuento");
            entity.Property(e => e.TotalImporte)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("totalImporte");
            entity.Property(e => e.TotalSinImpuesto)
                .HasColumnType("numeric(8, 2)")
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

            entity.Property(e => e.IdProveedor)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idProveedor");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Observacion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacion");
            entity.Property(e => e.PaginaWeb)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("paginaWeb");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Representante)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("representante");
            entity.Property(e => e.Telefono)
                .HasMaxLength(500)
                .IsUnicode(false)
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
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idProvincia");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<PuntoEmisiones>(entity =>
        {
            entity.HasKey(e => e.IdPuntoEmision).HasName("puntoEmisiones_pkey");

            entity.ToTable("puntoEmisiones");

            entity.Property(e => e.IdPuntoEmision)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idPuntoEmision");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idRetencion");
            entity.Property(e => e.AgenteRetencion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("agenteRetencion");
            entity.Property(e => e.Ambiente).HasColumnName("ambiente");
            entity.Property(e => e.ClaveAcceso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("claveAcceso");
            entity.Property(e => e.CodigoDocumento).HasColumnName("codigoDocumento");
            entity.Property(e => e.CorreoEnviado)
                .HasDefaultValue(false)
                .HasColumnName("correoEnviado");
            entity.Property(e => e.DireccionMatriz)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("direccionMatriz");
            entity.Property(e => e.EmisorNombreComercial)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("emisorNombreComercial");
            entity.Property(e => e.EmisorRazonSocial)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("emisorRazonSocial");
            entity.Property(e => e.EmisorRuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("emisorRuc");
            entity.Property(e => e.Establecimiento).HasColumnName("establecimiento");
            entity.Property(e => e.FechaAutorizacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaAutorizacion");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaEmision");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdDocumentoEmitir).HasColumnName("idDocumentoEmitir");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdEstablecimiento).HasColumnName("idEstablecimiento");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdPuntoEmision).HasColumnName("idPuntoEmision");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");
            entity.Property(e => e.IdTipoEstadoDocumento).HasColumnName("idTipoEstadoDocumento");
            entity.Property(e => e.IdTipoEstadoSri).HasColumnName("idTipoEstadoSri");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdentificacionSujetoRetenido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacionSujetoRetenido");
            entity.Property(e => e.NumAutDocSustento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numAutDocSustento");
            entity.Property(e => e.ObligadoContabilidad).HasColumnName("obligadoContabilidad");
            entity.Property(e => e.PeriodoFiscal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("periodoFiscal");
            entity.Property(e => e.PuntoEmision).HasColumnName("puntoEmision");
            entity.Property(e => e.RazonSocialSujetoRetenido)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("razonSocialSujetoRetenido");
            entity.Property(e => e.ReceptorCorreo)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("receptorCorreo");
            entity.Property(e => e.Ruta)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ruta");
            entity.Property(e => e.Secuencial).HasColumnName("secuencial");
            entity.Property(e => e.TipoDocumento).HasColumnName("tipoDocumento");
            entity.Property(e => e.TipoEmision).HasColumnName("tipoEmision");
            entity.Property(e => e.TipoIdentificacionSujetoRetenido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoIdentificacionSujetoRetenido");
            entity.Property(e => e.VersionXml)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("versionXml");

            entity.HasOne(d => d.IdDocumentoEmitirNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdDocumentoEmitir)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idDocumentoEmitir_fkey");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("empresas_");

            entity.HasOne(d => d.IdEstablecimientoNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdEstablecimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idEstablecimiento_fkey");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_retenciones_sriFactura");

            entity.HasOne(d => d.IdPuntoEmisionNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdPuntoEmision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retenciones_idPuntoEmision_fkey");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipoDocumento");

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

            entity.Property(e => e.IdSecuencial)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idSecuencial");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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

            entity.Property(e => e.IdSecuencialesProforma)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idSecuencialesProforma");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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

        modelBuilder.Entity<SriAmbientes>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__SriAmbie__40F9A20795A36BD9");

            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Ambiente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ambiente");
        });

        modelBuilder.Entity<SriCamposAdicionales>(entity =>
        {
            entity.HasKey(e => e.IdCampoAdicional).HasName("PK__SriCampo__745B4250D651D0DE");

            entity.Property(e => e.IdCampoAdicional).HasColumnName("idCampoAdicional");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Nombre)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Text)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("text");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.SriCamposAdicionales)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("SriCamposAdicionales_idFactura_fkey");
        });

        modelBuilder.Entity<SriDetallesFacturas>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura).HasName("PK__SriDetal__DFF38252565B8160");

            entity.Property(e => e.IdDetalleFactura).HasColumnName("idDetalleFactura");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CodigoPrincipal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoPrincipal");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Descuento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.PrecioTotalConImpuesto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioTotalConImpuesto");
            entity.Property(e => e.PrecioTotalSinImpuesto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioTotalSinImpuesto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioUnitario");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.SriDetallesFacturas)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("SriDetallesFacturas_idFactura_fkey");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.SriDetallesFacturas)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("SriDetallesFacturas_idProducto_fkey");
        });

        modelBuilder.Entity<SriDetallesFacturasImpuestos>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFacturaImpuesto).HasName("PK__SriDetal__5383C987320853EA");

            entity.Property(e => e.IdDetalleFacturaImpuesto).HasColumnName("idDetalleFacturaImpuesto");
            entity.Property(e => e.BaseImponible)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("baseImponible");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.CodigoPorcentaje)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigoPorcentaje");
            entity.Property(e => e.IdDetalleFactura).HasColumnName("idDetalleFactura");
            entity.Property(e => e.Tarifa)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("tarifa");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.CodigoPorcentajeNavigation).WithMany(p => p.SriDetallesFacturasImpuestos)
                .HasForeignKey(d => d.CodigoPorcentaje)
                .HasConstraintName("SriDetallesFacturasImpuestos_codigoPorcentaje_fkey");

            entity.HasOne(d => d.IdDetalleFacturaNavigation).WithMany(p => p.SriDetallesFacturasImpuestos)
                .HasForeignKey(d => d.IdDetalleFactura)
                .HasConstraintName("SriDetallesFacturasImpuestos_idDetalleFactura_fkey");
        });

        modelBuilder.Entity<SriEstados>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__SriEstad__40F9A207020C3F18");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<SriFacturas>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__SriFactu__3CD5687EDC14F968");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Ambiente)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ambiente");
            entity.Property(e => e.ClaveAcceso)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("claveAcceso");
            entity.Property(e => e.CodDoc)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codDoc");
            entity.Property(e => e.CodigoEstado)
                .HasDefaultValue(0)
                .HasColumnName("codigoEstado");
            entity.Property(e => e.Compra)
                .HasDefaultValue(false)
                .HasColumnName("compra");
            entity.Property(e => e.ContribuyenteEspecial)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("contribuyenteEspecial");
            entity.Property(e => e.DirEstablecimiento)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("dirEstablecimiento");
            entity.Property(e => e.DirMatriz)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("dirMatriz");
            entity.Property(e => e.Estab)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("estab");
            entity.Property(e => e.FechaAutorizacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaAutorizacion");
            entity.Property(e => e.FechaEmision).HasColumnName("fechaEmision");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdentificacionComprador)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacionComprador");
            entity.Property(e => e.ImporteTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("importeTotal");
            entity.Property(e => e.Moneda)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("moneda");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombreComercial");
            entity.Property(e => e.ObligadoContabilidad)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("obligadoContabilidad");
            entity.Property(e => e.Propina)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("propina");
            entity.Property(e => e.PtoEmi)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ptoEmi");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.RazonSocialComprador)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("razonSocialComprador");
            entity.Property(e => e.RetencionGenerada)
                .HasDefaultValue(false)
                .HasColumnName("retencionGenerada");
            entity.Property(e => e.Ruc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ruc");
            entity.Property(e => e.Secuencial)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("secuencial");
            entity.Property(e => e.TipoIdentificacionComprador)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tipoIdentificacionComprador");
            entity.Property(e => e.TotalDescuento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalDescuento");
            entity.Property(e => e.TotalSinImpuesto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalSinImpuesto");
            entity.Property(e => e.Version)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("version");

            entity.HasOne(d => d.AmbienteNavigation).WithMany(p => p.SriFacturas)
                .HasForeignKey(d => d.Ambiente)
                .HasConstraintName("SriFacturas_ambiente_fkey");

            entity.HasOne(d => d.CodDocNavigation).WithMany(p => p.SriFacturas)
                .HasForeignKey(d => d.CodDoc)
                .HasConstraintName("SriFacturas_codDoc_fkey");

            entity.HasOne(d => d.MonedaNavigation).WithMany(p => p.SriFacturas)
                .HasForeignKey(d => d.Moneda)
                .HasConstraintName("SriFacturas_moneda_fkey");
        });

        modelBuilder.Entity<SriFormasPagos>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__SriForma__40F9A207FDC79B7D");

            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FormaPago)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("formaPago");
        });

        modelBuilder.Entity<SriMonedas>(entity =>
        {
            entity.HasKey(e => e.Moneda).HasName("PK__SriMoned__93B33AA15A7813C3");

            entity.Property(e => e.Moneda)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("moneda");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
        });

        modelBuilder.Entity<SriPagos>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__SriPagos__BD2295ADA7885B83");

            entity.Property(e => e.IdPago).HasColumnName("idPago");
            entity.Property(e => e.FormaPago)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("formaPago");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Plazo).HasColumnName("plazo");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
            entity.Property(e => e.UnidadTiempo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("unidadTiempo");

            entity.HasOne(d => d.FormaPagoNavigation).WithMany(p => p.SriPagos)
                .HasForeignKey(d => d.FormaPago)
                .HasConstraintName("SriPagos_formaPago_fkey");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.SriPagos)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("SriPagos_idFactura_fkey");

            entity.HasOne(d => d.UnidadTiempoNavigation).WithMany(p => p.SriPagos)
                .HasForeignKey(d => d.UnidadTiempo)
                .HasConstraintName("SriPagos_unidadTiempo_fkey");
        });

        modelBuilder.Entity<SriPersonas>(entity =>
        {
            entity.HasKey(e => e.Identificacion).HasName("PK__SriPerso__C196DEC6AE40344B");

            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Direccion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Proveedor)
                .HasDefaultValue(false)
                .HasColumnName("proveedor");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tipoIdentificacion");

            entity.HasOne(d => d.TipoIdentificacionNavigation).WithMany(p => p.SriPersonas)
                .HasForeignKey(d => d.TipoIdentificacion)
                .HasConstraintName("SriPersonas_tipoIdentificacion_fkey");
        });

        modelBuilder.Entity<SriPrecios>(entity =>
        {
            entity.HasKey(e => e.IdPrecio).HasName("PK__SriPreci__BF8B120CC62B3C36");

            entity.Property(e => e.IdPrecio).HasColumnName("idPrecio");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.BaseImponible)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("baseImponible");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Tarifa)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("tarifa");
            entity.Property(e => e.TotalConImpuestos)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalConImpuestos");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.CodigoNavigation).WithMany(p => p.SriPrecios)
                .HasForeignKey(d => d.Codigo)
                .HasConstraintName("SriPrecios_codigo_fkey");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.SriPrecios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("SriPrecios_idProducto_fkey");
        });

        modelBuilder.Entity<SriProductos>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__SriProdu__07F4A1327F54DC9C");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.CodigoPrincipal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoPrincipal");
            entity.Property(e => e.Disponible)
                .HasDefaultValue(true)
                .HasColumnName("disponible");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioCompra");
            entity.Property(e => e.Producto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("producto");
            entity.Property(e => e.Stock)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("stock");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.SriProductos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("SriProductos_idEmpresa_fkey");

            entity.HasOne(d => d.IdentificacionNavigation).WithMany(p => p.SriProductos)
                .HasForeignKey(d => d.Identificacion)
                .HasConstraintName("SriProductos_identificacion_fkey");
        });

        modelBuilder.Entity<SriTarifasImpuestos>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__SriTarif__40F9A207A42C4BA1");

            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Tarifa)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("tarifa");
            entity.Property(e => e.Valor)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("valor");
        });

        modelBuilder.Entity<SriTiposDocumentos>(entity =>
        {
            entity.HasKey(e => e.CodDoc).HasName("PK__SriTipos__9FE736F2AC209630");

            entity.Property(e => e.CodDoc)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codDoc");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoDocumento");
        });

        modelBuilder.Entity<SriTiposIdentificaciones>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__SriTipos__40F9A207CE71EE3D");

            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoIdentificacion");
        });

        modelBuilder.Entity<SriTotalesConImpuestos>(entity =>
        {
            entity.HasKey(e => e.IdTotalConImpuesto).HasName("PK__SriTotal__22EAEE70429CEFD4");

            entity.Property(e => e.IdTotalConImpuesto).HasColumnName("idTotalConImpuesto");
            entity.Property(e => e.BaseImponible)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("baseImponible");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.CodigoPorcentaje)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigoPorcentaje");
            entity.Property(e => e.DescuentoAdicional)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("descuentoAdicional");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.CodigoPorcentajeNavigation).WithMany(p => p.SriTotalesConImpuestos)
                .HasForeignKey(d => d.CodigoPorcentaje)
                .HasConstraintName("SriTotalesConImpuestos_codigoPorcentaje_fkey");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.SriTotalesConImpuestos)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("SriTotalesConImpuestos_idFactura_fkey");
        });

        modelBuilder.Entity<SriUnidadesTiempos>(entity =>
        {
            entity.HasKey(e => e.UnidadTiempo).HasName("SriUnidadesTiempos_pkey");

            entity.Property(e => e.UnidadTiempo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("unidadTiempo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
        });

        modelBuilder.Entity<TiempoFormaPagos>(entity =>
        {
            entity.HasKey(e => e.IdTiempoFormaPago).HasName("tiempoFormaPagos_pkey");

            entity.ToTable("tiempoFormaPagos");

            entity.Property(e => e.IdTiempoFormaPago)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idTiempoFormaPago");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("tipoDocumentos_pkey");

            entity.ToTable("tipoDocumentos");

            entity.Property(e => e.IdTipoDocumento)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idTipoDocumento");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoEstadoDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoEstadoDocumento).HasName("tipoEstadoDocumentos_pkey");

            entity.ToTable("tipoEstadoDocumentos");

            entity.Property(e => e.IdTipoEstadoDocumento)
                .ValueGeneratedNever()
                .HasColumnName("idTipoEstadoDocumento");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoEstadoSri>(entity =>
        {
            entity.HasKey(e => e.IdTipoEstadoSri).HasName("tipoEstadoSri_pkey");

            entity.ToTable("tipoEstadoSri");

            entity.Property(e => e.IdTipoEstadoSri)
                .ValueGeneratedNever()
                .HasColumnName("idTipoEstadoSri");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoIdentificaciones>(entity =>
        {
            entity.HasKey(e => e.IdTipoIdentificacion).HasName("tipoIdentificaciones_pkey");

            entity.ToTable("tipoIdentificaciones");

            entity.Property(e => e.IdTipoIdentificacion)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoIdentificacionesGeneracionDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoIdentificacionesGeneracionDocumentos).HasName("tipoIdentificacionesGeneracionDocumentos_pkey");

            entity.ToTable("tipoIdentificacionesGeneracionDocumentos");

            entity.Property(e => e.IdTipoIdentificacionesGeneracionDocumentos)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idTipoIdentificacionesGeneracionDocumentos");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoNegocios>(entity =>
        {
            entity.HasKey(e => e.IdTipoNegocio).HasName("tipoNegocios_pkey");

            entity.ToTable("tipoNegocios");

            entity.Property(e => e.IdTipoNegocio)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idTipoNegocio");
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoValorRetenciones>(entity =>
        {
            entity.HasKey(e => e.IdTipoValorRetencion).HasName("tipoRetencion_pkey");

            entity.ToTable("tipoValorRetenciones");

            entity.Property(e => e.IdTipoValorRetencion)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idTipoValorRetencion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TiposDenominacionesDinero>(entity =>
        {
            entity.HasKey(e => e.IdTipoDenominacion).HasName("PK__TiposDen__438C6E7F112628AC");

            entity.Property(e => e.IdTipoDenominacion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("idTipoDenominacion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<UsuarioEmpresas>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioEmpresas).HasName("usuarioEmpresas_pkey");

            entity.ToTable("usuarioEmpresas");

            entity.Property(e => e.IdUsuarioEmpresas)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idUsuarioEmpresas");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Clave)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
