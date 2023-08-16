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

    public virtual DbSet<DocumentosEmitir> DocumentosEmitir { get; set; }

    public virtual DbSet<Empleados> Empleados { get; set; }

    public virtual DbSet<Empresas> Empresas { get; set; }

    public virtual DbSet<Establecimientos> Establecimientos { get; set; }

    public virtual DbSet<Facturas> Facturas { get; set; }

    public virtual DbSet<FormaPagos> FormaPagos { get; set; }

    public virtual DbSet<InformacionAdicional> InformacionAdicional { get; set; }

    public virtual DbSet<Ivas> Ivas { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }

    public virtual DbSet<Provincias> Provincias { get; set; }

    public virtual DbSet<PuntoEmisiones> PuntoEmisiones { get; set; }

    public virtual DbSet<Secuenciales> Secuenciales { get; set; }

    public virtual DbSet<TiempoFormaPagos> TiempoFormaPagos { get; set; }

    public virtual DbSet<TipoDocumentos> TipoDocumentos { get; set; }

    public virtual DbSet<TipoEstadoDocumentos> TipoEstadoDocumentos { get; set; }

    public virtual DbSet<TipoEstadoSri> TipoEstadoSri { get; set; }

    public virtual DbSet<TipoIdentificaciones> TipoIdentificaciones { get; set; }

    public virtual DbSet<TipoIdentificacionesGeneracionDocumentos> TipoIdentificacionesGeneracionDocumentos { get; set; }

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

            entity.Property(e => e.IdDetalleFactura)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDetalleFactura");
            entity.Property(e => e.Cantidad)
                .HasPrecision(8, 2)
                .HasColumnName("cantidad");
            entity.Property(e => e.Descuento)
                .HasPrecision(8, 2)
                .HasColumnName("descuento");
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
        });

        modelBuilder.Entity<DetalleFormaPagos>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFormaPago).HasName("detalleFormaPagos_pkey");

            entity.ToTable("detalleFormaPagos");

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

            entity.Property(e => e.IdDetallePrecioProducto)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDetallePrecioProducto");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
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

        modelBuilder.Entity<DocumentosEmitir>(entity =>
        {
            entity.HasKey(e => e.IdDocumentoEmitir).HasName("documentosEmitir_pkey");

            entity.ToTable("documentosEmitir");

            entity.Property(e => e.IdDocumentoEmitir)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idDocumentoEmitir");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("false")
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
            entity.Property(e => e.DireccionMatriz)
                .HasMaxLength(1000)
                .HasColumnName("direccionMatriz");
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

        modelBuilder.Entity<Establecimientos>(entity =>
        {
            entity.HasKey(e => e.IdEstablecimiento).HasName("establecimientos_pkey");

            entity.ToTable("establecimientos");

            entity.Property(e => e.IdEstablecimiento)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idEstablecimiento");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
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
                .HasDefaultValueSql("false")
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
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idFactura");
            entity.Property(e => e.AgenteRetencion)
                .HasDefaultValueSql("false")
                .HasColumnName("agenteRetencion");
            entity.Property(e => e.Ambiente).HasColumnName("ambiente");
            entity.Property(e => e.ClaveAcceso)
                .HasMaxLength(50)
                .HasColumnName("claveAcceso");
            entity.Property(e => e.CodigoDocModificado).HasColumnName("codigoDocModificado");
            entity.Property(e => e.ContribuyenteEspecial)
                .HasMaxLength(20)
                .HasColumnName("contribuyenteEspecial");
            entity.Property(e => e.ContribuyenteRimpe)
                .HasDefaultValueSql("false")
                .HasColumnName("contribuyenteRimpe");
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
            entity.Property(e => e.Ruta)
                .HasMaxLength(1000)
                .HasColumnName("ruta");
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
            entity.Property(e => e.VersionXml)
                .HasMaxLength(10)
                .HasColumnName("versionXml");

            entity.HasOne(d => d.IdTipoEstadoDocumentoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTipoEstadoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("facturas_idTipoEstadoDocumento_fkey");

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
        });

        modelBuilder.Entity<InformacionAdicional>(entity =>
        {
            entity.HasKey(e => e.IdInformacionAdicional).HasName("informacionAdicional_pkey");

            entity.ToTable("informacionAdicional");

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
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
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

        modelBuilder.Entity<PuntoEmisiones>(entity =>
        {
            entity.HasKey(e => e.IdPuntoEmision).HasName("puntoEmisiones_pkey");

            entity.ToTable("puntoEmisiones");

            entity.Property(e => e.IdPuntoEmision)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idPuntoEmision");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
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
                .HasDefaultValueSql("false")
                .HasColumnName("predeterminado");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.PuntoEmisiones)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("puntoEmisiones_idEmpresa_fkey");
        });

        modelBuilder.Entity<Secuenciales>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("secuenciales");

            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
                .HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSecuencial)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idSecuencial");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");
            entity.Property(e => e.Nombre).HasColumnName("nombre");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany()
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("secuenciales_idEmpresa_fkey");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany()
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("secuenciales_idTipoDocumento_fkey");
        });

        modelBuilder.Entity<TiempoFormaPagos>(entity =>
        {
            entity.HasKey(e => e.IdTiempoFormaPago).HasName("tiempoFormaPagos_pkey");

            entity.ToTable("tiempoFormaPagos");

            entity.Property(e => e.IdTiempoFormaPago)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTiempoFormaPago");
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
        });

        modelBuilder.Entity<TipoDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("tipoDocumentos_pkey");

            entity.ToTable("tipoDocumentos");

            entity.Property(e => e.IdTipoDocumento)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTipoDocumento");
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
        });

        modelBuilder.Entity<TipoEstadoDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoEstadoDocumento).HasName("tipoEstadoDocumentos_pkey");

            entity.ToTable("tipoEstadoDocumentos");

            entity.Property(e => e.IdTipoEstadoDocumento).HasColumnName("idTipoEstadoDocumento");
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

        modelBuilder.Entity<TipoEstadoSri>(entity =>
        {
            entity.HasKey(e => e.IdTipoEstadoSri).HasName("tipoEstadoSri_pkey");

            entity.ToTable("tipoEstadoSri");

            entity.Property(e => e.IdTipoEstadoSri).HasColumnName("idTipoEstadoSri");
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

        modelBuilder.Entity<TipoIdentificacionesGeneracionDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoIdentificacionesGeneracionDocumentos).HasName("tipoIdentificacionesGeneracionDocumentos_pkey");

            entity.ToTable("tipoIdentificacionesGeneracionDocumentos");

            entity.Property(e => e.IdTipoIdentificacionesGeneracionDocumentos)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idTipoIdentificacionesGeneracionDocumentos");
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
            entity.HasKey(e => e.IdUsuarioEmpresas).HasName("usuarioEmpresas_pkey");

            entity.ToTable("usuarioEmpresas");

            entity.Property(e => e.IdUsuarioEmpresas)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("idUsuarioEmpresas");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("true")
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
