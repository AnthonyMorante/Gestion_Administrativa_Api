using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Administrativa_Api.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    idError = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    error = table.Column<string>(type: "varchar(7300)", unicode: false, maxLength: 7300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ErrorLog__6FC78380515670CB", x => x.idError);
                });

            migrationBuilder.CreateTable(
                name: "formaPagos",
                columns: table => new
                {
                    idFormaPago = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    codigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("formaPagos_pkey", x => x.idFormaPago);
                });

            migrationBuilder.CreateTable(
                name: "informacionFirmas",
                columns: table => new
                {
                    idInformacionFirma = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    razonSocial = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    identificacion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    ruta = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    codigo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("informacionFirmas_pkey", x => x.idInformacionFirma);
                });

            migrationBuilder.CreateTable(
                name: "ivas",
                columns: table => new
                {
                    idIva = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    valor = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    codigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ivas_pkey", x => x.idIva);
                });

            migrationBuilder.CreateTable(
                name: "provincias",
                columns: table => new
                {
                    idProvincia = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("provincias_pkey", x => x.idProvincia);
                });

            migrationBuilder.CreateTable(
                name: "SriAmbientes",
                columns: table => new
                {
                    codigo = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    ambiente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriAmbie__40F9A20795A36BD9", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "SriEstados",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriEstad__40F9A207020C3F18", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "SriFormasPagos",
                columns: table => new
                {
                    codigo = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    formaPago = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriForma__40F9A207FDC79B7D", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "SriMonedas",
                columns: table => new
                {
                    moneda = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriMoned__93B33AA15A7813C3", x => x.moneda);
                });

            migrationBuilder.CreateTable(
                name: "SriTarifasImpuestos",
                columns: table => new
                {
                    codigo = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    tarifa = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    valor = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriTarif__40F9A207A42C4BA1", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "SriTiposDocumentos",
                columns: table => new
                {
                    codDoc = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    tipoDocumento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriTipos__9FE736F2AC209630", x => x.codDoc);
                });

            migrationBuilder.CreateTable(
                name: "SriTiposIdentificaciones",
                columns: table => new
                {
                    codigo = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    tipoIdentificacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriTipos__40F9A207CE71EE3D", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "SriUnidadesTiempos",
                columns: table => new
                {
                    unidadTiempo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    codigo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SriUnidadesTiempos_pkey", x => x.unidadTiempo);
                });

            migrationBuilder.CreateTable(
                name: "tiempoFormaPagos",
                columns: table => new
                {
                    idTiempoFormaPago = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    codigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tiempoFormaPagos_pkey", x => x.idTiempoFormaPago);
                });

            migrationBuilder.CreateTable(
                name: "tipoDocumentos",
                columns: table => new
                {
                    idTipoDocumento = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    codigo = table.Column<int>(type: "int", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoDocumentos_pkey", x => x.idTipoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "tipoEstadoDocumentos",
                columns: table => new
                {
                    idTipoEstadoDocumento = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    codigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoEstadoDocumentos_pkey", x => x.idTipoEstadoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "tipoEstadoSri",
                columns: table => new
                {
                    idTipoEstadoSri = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    codigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoEstadoSri_pkey", x => x.idTipoEstadoSri);
                });

            migrationBuilder.CreateTable(
                name: "tipoIdentificaciones",
                columns: table => new
                {
                    idTipoIdentificacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    codigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoIdentificaciones_pkey", x => x.idTipoIdentificacion);
                });

            migrationBuilder.CreateTable(
                name: "tipoIdentificacionesGeneracionDocumentos",
                columns: table => new
                {
                    idTipoIdentificacionesGeneracionDocumentos = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    codigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoIdentificacionesGeneracionDocumentos_pkey", x => x.idTipoIdentificacionesGeneracionDocumentos);
                });

            migrationBuilder.CreateTable(
                name: "tipoNegocios",
                columns: table => new
                {
                    idTipoNegocio = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    codigo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoNegocios_pkey", x => x.idTipoNegocio);
                });

            migrationBuilder.CreateTable(
                name: "TiposDenominacionesDinero",
                columns: table => new
                {
                    idTipoDenominacion = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    tipo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TiposDen__438C6E7F112628AC", x => x.idTipoDenominacion);
                });

            migrationBuilder.CreateTable(
                name: "tipoValorRetenciones",
                columns: table => new
                {
                    idTipoValorRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    codigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoRetencion_pkey", x => x.idTipoValorRetencion);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    clave = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuarios_pkey", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "ciudades",
                columns: table => new
                {
                    idCiudad = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    idProvincia = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ciudades_pkey", x => x.idCiudad);
                    table.ForeignKey(
                        name: "ciudades_idProvincia_fkey",
                        column: x => x.idProvincia,
                        principalTable: "provincias",
                        principalColumn: "idProvincia");
                });

            migrationBuilder.CreateTable(
                name: "SriFacturas",
                columns: table => new
                {
                    idFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fechaEmision = table.Column<DateOnly>(type: "date", nullable: true),
                    compra = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    fechaAutorizacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    codigoEstado = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    claveAcceso = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    codDoc = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    obligadoContabilidad = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    ambiente = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    dirMatriz = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    estab = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    nombreComercial = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    ptoEmi = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    razonSocial = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    ruc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    secuencial = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    version = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    contribuyenteEspecial = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    dirEstablecimiento = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    identificacionComprador = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    razonSocialComprador = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    tipoIdentificacionComprador = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    moneda = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    propina = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0m),
                    totalDescuento = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    totalSinImpuesto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    importeTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    retencionGenerada = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriFactu__3CD5687EDC14F968", x => x.idFactura);
                    table.ForeignKey(
                        name: "SriFacturas_ambiente_fkey",
                        column: x => x.ambiente,
                        principalTable: "SriAmbientes",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "SriFacturas_codDoc_fkey",
                        column: x => x.codDoc,
                        principalTable: "SriTiposDocumentos",
                        principalColumn: "codDoc");
                    table.ForeignKey(
                        name: "SriFacturas_moneda_fkey",
                        column: x => x.moneda,
                        principalTable: "SriMonedas",
                        principalColumn: "moneda");
                });

            migrationBuilder.CreateTable(
                name: "SriPersonas",
                columns: table => new
                {
                    identificacion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tipoIdentificacion = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    razonSocial = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    nombres = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    direccion = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    proveedor = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriPerso__C196DEC6AE40344B", x => x.identificacion);
                    table.ForeignKey(
                        name: "SriPersonas_tipoIdentificacion_fkey",
                        column: x => x.tipoIdentificacion,
                        principalTable: "SriTiposIdentificaciones",
                        principalColumn: "codigo");
                });

            migrationBuilder.CreateTable(
                name: "documentosEmitir",
                columns: table => new
                {
                    idDocumentoEmitir = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    idTipoDocumento = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("documentosEmitir_pkey", x => x.idDocumentoEmitir);
                    table.ForeignKey(
                        name: "documentosEmitir_idTipoDocumento_fkey",
                        column: x => x.idTipoDocumento,
                        principalTable: "tipoDocumentos",
                        principalColumn: "idTipoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    identificacion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    razonSocial = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    telefono = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    llevaContabilidad = table.Column<bool>(type: "bit", nullable: true),
                    regimenMicroEmpresas = table.Column<bool>(type: "bit", nullable: true),
                    regimenRimpe = table.Column<bool>(type: "bit", nullable: true),
                    agenteRetencion = table.Column<bool>(type: "bit", nullable: true),
                    idTipoNegocio = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    direccionMatriz = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    idInformacionFirma = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    resolucionAgenteRetencion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("empresas_pkey", x => x.idEmpresa);
                    table.ForeignKey(
                        name: "empresas_TipoNegocios",
                        column: x => x.idTipoNegocio,
                        principalTable: "tipoNegocios",
                        principalColumn: "idTipoNegocio");
                    table.ForeignKey(
                        name: "fk_empresas_informacionfirmas",
                        column: x => x.idInformacionFirma,
                        principalTable: "informacionFirmas",
                        principalColumn: "idInformacionFirma");
                });

            migrationBuilder.CreateTable(
                name: "DenominacionesDinero",
                columns: table => new
                {
                    idDenominacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    valor = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    idTipoDenominacion = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Denomina__80C12401D636482D", x => x.idDenominacion);
                    table.ForeignKey(
                        name: "FK__Denominac__idTip__4BE214AA",
                        column: x => x.idTipoDenominacion,
                        principalTable: "TiposDenominacionesDinero",
                        principalColumn: "idTipoDenominacion");
                });

            migrationBuilder.CreateTable(
                name: "porcentajeImpuestosRetenciones",
                columns: table => new
                {
                    idPorcentajeImpuestoRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    valor = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    codigo = table.Column<int>(type: "int", nullable: true),
                    idTipoValorRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("porcentajeRetencion_pkey", x => x.idPorcentajeImpuestoRetencion);
                    table.ForeignKey(
                        name: "tipoRetencionIva_idTipoValorRetencion_fkey",
                        column: x => x.idTipoValorRetencion,
                        principalTable: "tipoValorRetenciones",
                        principalColumn: "idTipoValorRetencion");
                });

            migrationBuilder.CreateTable(
                name: "facturas",
                columns: table => new
                {
                    idFactura = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    agenteRetencion = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    ambiente = table.Column<int>(type: "int", nullable: true),
                    claveAcceso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    codigoDocModificado = table.Column<int>(type: "int", nullable: true),
                    contribuyenteRimpe = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    contribuyenteEspecial = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    direccionEstablecimiento = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    direccionMatriz = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    emisorRazonSocial = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    emisorRuc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    establecimiento = table.Column<int>(type: "int", nullable: true),
                    puntoEmision = table.Column<int>(type: "int", nullable: true),
                    secuencial = table.Column<int>(type: "int", nullable: true),
                    fechaAutorizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fechaEmision = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    exentoIva = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    ice = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    irbpnr = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    isd = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    iva12 = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    subtotal0 = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    moneda = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    obligadoContabilidad = table.Column<bool>(type: "bit", nullable: true),
                    receptorRazonSocial = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    receptorRuc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    receptorTipoIdentificacion = table.Column<int>(type: "int", nullable: true),
                    regimenMicroempresas = table.Column<bool>(type: "bit", nullable: true),
                    tipoDocumento = table.Column<int>(type: "int", nullable: true),
                    tipoEmision = table.Column<int>(type: "int", nullable: true),
                    totalDescuento = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    totalImporte = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    totalSinImpuesto = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    versionXml = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    idTipoEstadoDocumento = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    receptorTelefono = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    receptorCorreo = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true),
                    receptorDireccion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    idCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idCiudad = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idDocumentoEmitir = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEstablecimiento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPuntoEmision = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subtotal12 = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    regimenRimpe = table.Column<bool>(type: "bit", nullable: true),
                    ruta = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    resolucionAgenteRetencion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    saldo = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    valorRecibido = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    cambio = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    idTipoEstadoSri = table.Column<int>(type: "int", nullable: true),
                    mensaje = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    correoEnviado = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("facturas_pkey", x => x.idFactura);
                    table.ForeignKey(
                        name: "facturas_idTipoEstadoDocumento_fkey",
                        column: x => x.idTipoEstadoDocumento,
                        principalTable: "tipoEstadoDocumentos",
                        principalColumn: "idTipoEstadoDocumento");
                    table.ForeignKey(
                        name: "facturas_idTipoEstadoSri_fkey",
                        column: x => x.idTipoEstadoSri,
                        principalTable: "tipoEstadoSri",
                        principalColumn: "idTipoEstadoSri");
                    table.ForeignKey(
                        name: "facturas_idUsuario_fkey",
                        column: x => x.idUsuario,
                        principalTable: "usuarios",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "SriCamposAdicionales",
                columns: table => new
                {
                    idCampoAdicional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idFactura = table.Column<int>(type: "int", nullable: true),
                    nombre = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    text = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriCampo__745B4250D651D0DE", x => x.idCampoAdicional);
                    table.ForeignKey(
                        name: "SriCamposAdicionales_idFactura_fkey",
                        column: x => x.idFactura,
                        principalTable: "SriFacturas",
                        principalColumn: "idFactura");
                });

            migrationBuilder.CreateTable(
                name: "SriPagos",
                columns: table => new
                {
                    idPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idFactura = table.Column<int>(type: "int", nullable: true),
                    formaPago = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    plazo = table.Column<int>(type: "int", nullable: true),
                    unidadTiempo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriPagos__BD2295ADA7885B83", x => x.idPago);
                    table.ForeignKey(
                        name: "SriPagos_formaPago_fkey",
                        column: x => x.formaPago,
                        principalTable: "SriFormasPagos",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "SriPagos_idFactura_fkey",
                        column: x => x.idFactura,
                        principalTable: "SriFacturas",
                        principalColumn: "idFactura");
                    table.ForeignKey(
                        name: "SriPagos_unidadTiempo_fkey",
                        column: x => x.unidadTiempo,
                        principalTable: "SriUnidadesTiempos",
                        principalColumn: "unidadTiempo");
                });

            migrationBuilder.CreateTable(
                name: "SriTotalesConImpuestos",
                columns: table => new
                {
                    idTotalConImpuesto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idFactura = table.Column<int>(type: "int", nullable: true),
                    baseImponible = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    codigo = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    codigoPorcentaje = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    descuentoAdicional = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriTotal__22EAEE70429CEFD4", x => x.idTotalConImpuesto);
                    table.ForeignKey(
                        name: "SriTotalesConImpuestos_codigoPorcentaje_fkey",
                        column: x => x.codigoPorcentaje,
                        principalTable: "SriTarifasImpuestos",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "SriTotalesConImpuestos_idFactura_fkey",
                        column: x => x.idFactura,
                        principalTable: "SriFacturas",
                        principalColumn: "idFactura");
                });

            migrationBuilder.CreateTable(
                name: "Cajas",
                columns: table => new
                {
                    idCaja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fechaCierre = table.Column<DateTime>(type: "datetime", nullable: true),
                    totalApertura = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    totalCierre = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    detallado = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cajas__8BC79B34440E38E5", x => x.idCaja);
                    table.ForeignKey(
                        name: "FK__Cajas__idEmpresa__3AB788A8",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    idCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    identificacion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    razonSocial = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    direccion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    email = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    telefono = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    observacion = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    idCiudad = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idTipoIdentificacion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clientes_pkey", x => x.idCliente);
                    table.ForeignKey(
                        name: "cientes_empresas",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "clientes_idCiudad_fkey",
                        column: x => x.idCiudad,
                        principalTable: "ciudades",
                        principalColumn: "idCiudad");
                    table.ForeignKey(
                        name: "clientes_idTipoIdentificacion_fkey",
                        column: x => x.idTipoIdentificacion,
                        principalTable: "tipoIdentificaciones",
                        principalColumn: "idTipoIdentificacion");
                });

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    idEmpleado = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    identificacion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    razonSocial = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    direccion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    email = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    telefono = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    observacion = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    idCiudad = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idTipoIdentificacion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("empleados_pkey", x => x.idEmpleado);
                    table.ForeignKey(
                        name: "empleados_empresas",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "empleados_idCiudad_fkey",
                        column: x => x.idCiudad,
                        principalTable: "ciudades",
                        principalColumn: "idCiudad");
                    table.ForeignKey(
                        name: "empleados_idTipoIdentificacion_fkey",
                        column: x => x.idTipoIdentificacion,
                        principalTable: "tipoIdentificaciones",
                        principalColumn: "idTipoIdentificacion");
                });

            migrationBuilder.CreateTable(
                name: "establecimientos",
                columns: table => new
                {
                    idEstablecimiento = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    predeterminado = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    nombre = table.Column<long>(type: "bigint", nullable: true),
                    direccion = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("establecimientos_pkey", x => x.idEstablecimiento);
                    table.ForeignKey(
                        name: "establecimientos_idEmpresa_fkey",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    idProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    codigo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    precio = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    idIva = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    activoProducto = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    totalIva = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    cantidad = table.Column<decimal>(type: "numeric(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("productos_pkey", x => x.idProducto);
                    table.ForeignKey(
                        name: "productos_empresas",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "productos_idIva_fkey",
                        column: x => x.idIva,
                        principalTable: "ivas",
                        principalColumn: "idIva");
                });

            migrationBuilder.CreateTable(
                name: "proveedores",
                columns: table => new
                {
                    idProveedor = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    identificacion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    razonSocial = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    representante = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    direccion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    email = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    telefono = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    paginaWeb = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    observacion = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    idCiudad = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idTipoIdentificacion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("proveedores_pkey", x => x.idProveedor);
                    table.ForeignKey(
                        name: "proveedores_empresas",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "proveedores_idCiudad_fkey",
                        column: x => x.idCiudad,
                        principalTable: "ciudades",
                        principalColumn: "idCiudad");
                    table.ForeignKey(
                        name: "proveedores_idTipoIdentificacion_fkey",
                        column: x => x.idTipoIdentificacion,
                        principalTable: "tipoIdentificaciones",
                        principalColumn: "idTipoIdentificacion");
                });

            migrationBuilder.CreateTable(
                name: "puntoEmisiones",
                columns: table => new
                {
                    idPuntoEmision = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    predeterminado = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    nombre = table.Column<long>(type: "bigint", nullable: true),
                    direccion = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("puntoEmisiones_pkey", x => x.idPuntoEmision);
                    table.ForeignKey(
                        name: "puntoEmisiones_idEmpresa_fkey",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "secuenciales",
                columns: table => new
                {
                    idSecuencial = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<long>(type: "bigint", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idTipoDocumento = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("secuenciales_pkey", x => x.idSecuencial);
                    table.ForeignKey(
                        name: "secuenciales_idEmpresa_fkey",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "secuenciales_idTipoDocumento_fkey",
                        column: x => x.idTipoDocumento,
                        principalTable: "tipoDocumentos",
                        principalColumn: "idTipoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "secuencialesProformas",
                columns: table => new
                {
                    idSecuencialesProforma = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<long>(type: "bigint", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idTipoDocumento = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("secuencialesProformas_pkey", x => x.idSecuencialesProforma);
                    table.ForeignKey(
                        name: "secuencialesProformas_idEmpresa_fkey",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "secuencialesProformas_idTipoDocumento_fkey",
                        column: x => x.idTipoDocumento,
                        principalTable: "tipoDocumentos",
                        principalColumn: "idTipoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "SriProductos",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    identificacion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    codigoPrincipal = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    producto = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    precioCompra = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    stock = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    disponible = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriProdu__07F4A1327F54DC9C", x => x.idProducto);
                    table.ForeignKey(
                        name: "SriProductos_idEmpresa_fkey",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "SriProductos_identificacion_fkey",
                        column: x => x.identificacion,
                        principalTable: "SriPersonas",
                        principalColumn: "identificacion");
                });

            migrationBuilder.CreateTable(
                name: "usuarioEmpresas",
                columns: table => new
                {
                    idUsuarioEmpresas = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    idUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuarioEmpresas_pkey", x => x.idUsuarioEmpresas);
                    table.ForeignKey(
                        name: "usuarioEmpresas_idEmpresa_fkey",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "usuarioEmpresas_idUsuario_fkey",
                        column: x => x.idUsuario,
                        principalTable: "usuarios",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "detalleFormaPagos",
                columns: table => new
                {
                    idDetalleFormaPago = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    plazo = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    valor = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    idTiempoFormaPago = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idFactura = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idFormaPago = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalleFormaPagos_pkey", x => x.idDetalleFormaPago);
                    table.ForeignKey(
                        name: "detalleFormaPagos_idFactura_fkey",
                        column: x => x.idFactura,
                        principalTable: "facturas",
                        principalColumn: "idFactura");
                    table.ForeignKey(
                        name: "detalleFormaPagos_idTiempoFormaPago_fkey",
                        column: x => x.idTiempoFormaPago,
                        principalTable: "tiempoFormaPagos",
                        principalColumn: "idTiempoFormaPago");
                    table.ForeignKey(
                        name: "idFormaPago_",
                        column: x => x.idFormaPago,
                        principalTable: "formaPagos",
                        principalColumn: "idFormaPago");
                });

            migrationBuilder.CreateTable(
                name: "informacionAdicional",
                columns: table => new
                {
                    idInformacionAdicional = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    valor = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    idFactura = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("informacionAdicional_pkey", x => x.idInformacionAdicional);
                    table.ForeignKey(
                        name: "informacionAdicional_idFactura_fkey",
                        column: x => x.idFactura,
                        principalTable: "facturas",
                        principalColumn: "idFactura");
                });

            migrationBuilder.CreateTable(
                name: "DetallesCajas",
                columns: table => new
                {
                    idDetalleCaja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCaja = table.Column<int>(type: "int", nullable: true),
                    idDenominacion = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalles__0D031FF916F49A74", x => x.idDetalleCaja);
                    table.ForeignKey(
                        name: "FK__DetallesC__idCaj__4EBE8155",
                        column: x => x.idCaja,
                        principalTable: "Cajas",
                        principalColumn: "idCaja");
                    table.ForeignKey(
                        name: "FK__DetallesC__idDen__4FB2A58E",
                        column: x => x.idDenominacion,
                        principalTable: "DenominacionesDinero",
                        principalColumn: "idDenominacion");
                });

            migrationBuilder.CreateTable(
                name: "DetallesCajasCierres",
                columns: table => new
                {
                    idDetalleCajaCierre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCaja = table.Column<int>(type: "int", nullable: true),
                    idDenominacion = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalles__B96398E0F455CA0C", x => x.idDetalleCajaCierre);
                    table.ForeignKey(
                        name: "FK__DetallesC__idCaj__528F1239",
                        column: x => x.idCaja,
                        principalTable: "Cajas",
                        principalColumn: "idCaja");
                    table.ForeignKey(
                        name: "FK__DetallesC__idDen__53833672",
                        column: x => x.idDenominacion,
                        principalTable: "DenominacionesDinero",
                        principalColumn: "idDenominacion");
                });

            migrationBuilder.CreateTable(
                name: "detalleFacturas",
                columns: table => new
                {
                    idDetalleFactura = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    cantidad = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    descuento = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    precio = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    idIva = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    porcentaje = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    valorPorcentaje = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    subtotal = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    total = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    idFactura = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechoRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalleFacturas_pkey", x => x.idDetalleFactura);
                    table.ForeignKey(
                        name: "detalleFacturas_",
                        column: x => x.idProducto,
                        principalTable: "productos",
                        principalColumn: "idProducto");
                    table.ForeignKey(
                        name: "detalleFacturas_idFactura_fkey",
                        column: x => x.idFactura,
                        principalTable: "facturas",
                        principalColumn: "idFactura");
                    table.ForeignKey(
                        name: "idIva_",
                        column: x => x.idIva,
                        principalTable: "ivas",
                        principalColumn: "idIva");
                });

            migrationBuilder.CreateTable(
                name: "detallePrecioProductos",
                columns: table => new
                {
                    idDetallePrecioProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    totalIva = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    porcentaje = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    total = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    idProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    idIva = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detallePrecioProductos_pkey", x => x.idDetallePrecioProducto);
                    table.ForeignKey(
                        name: "detallePrecioProductos_idIva_fkey",
                        column: x => x.idIva,
                        principalTable: "ivas",
                        principalColumn: "idIva");
                    table.ForeignKey(
                        name: "detallePrecioProductos_idProducto_fkey",
                        column: x => x.idProducto,
                        principalTable: "productos",
                        principalColumn: "idProducto");
                });

            migrationBuilder.CreateTable(
                name: "lotes",
                columns: table => new
                {
                    idLote = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    cantidad = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    idProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lotes__1B91FFCB655DA6A5", x => x.idLote);
                    table.ForeignKey(
                        name: "lotes_idProducto_fkey",
                        column: x => x.idProducto,
                        principalTable: "productos",
                        principalColumn: "idProducto");
                    table.ForeignKey(
                        name: "lotes_idUsuario_fkey",
                        column: x => x.idUsuario,
                        principalTable: "usuarios",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ProductosProveedores",
                columns: table => new
                {
                    idProductoProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    idProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    codigoPrincipal = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    identificacion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__358285855AECBB43", x => x.idProductoProveedor);
                    table.ForeignKey(
                        name: "ProductosProveedores_idProducto_fkey",
                        column: x => x.idProducto,
                        principalTable: "productos",
                        principalColumn: "idProducto");
                    table.ForeignKey(
                        name: "ProductosProveedores_identificacion_fkey",
                        column: x => x.identificacion,
                        principalTable: "SriPersonas",
                        principalColumn: "identificacion");
                });

            migrationBuilder.CreateTable(
                name: "proformas",
                columns: table => new
                {
                    idProforma = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ambiente = table.Column<int>(type: "int", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    moneda = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    receptorRazonSocial = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    receptorRuc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    receptorTipoIdentificacion = table.Column<int>(type: "int", nullable: true),
                    receptorTelefono = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    receptorCorreo = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true),
                    receptorDireccion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    totalDescuento = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    totalImporte = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    totalSinImpuesto = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    idUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEstablecimiento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPuntoEmision = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subtotal12 = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    establecimiento = table.Column<int>(type: "int", nullable: true),
                    puntoEmision = table.Column<int>(type: "int", nullable: true),
                    secuencial = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("proformas_pkey", x => x.idProforma);
                    table.ForeignKey(
                        name: "proformas_idEstablecimiento_fkey",
                        column: x => x.idEstablecimiento,
                        principalTable: "establecimientos",
                        principalColumn: "idEstablecimiento");
                    table.ForeignKey(
                        name: "proformas_idPuntoEmision_fkey",
                        column: x => x.idPuntoEmision,
                        principalTable: "puntoEmisiones",
                        principalColumn: "idPuntoEmision");
                    table.ForeignKey(
                        name: "proformas_idUsuario_fkey",
                        column: x => x.idUsuario,
                        principalTable: "usuarios",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "retenciones",
                columns: table => new
                {
                    idRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ambiente = table.Column<int>(type: "int", nullable: true),
                    tipoEmision = table.Column<int>(type: "int", nullable: true),
                    emisorRazonSocial = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    emisorNombreComercial = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    emisorRuc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    claveAcceso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    codigoDocumento = table.Column<int>(type: "int", nullable: true),
                    establecimiento = table.Column<int>(type: "int", nullable: true),
                    puntoEmision = table.Column<int>(type: "int", nullable: true),
                    secuencial = table.Column<int>(type: "int", nullable: true),
                    direccionMatriz = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    agenteRetencion = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    fechaEmision = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    obligadoContabilidad = table.Column<bool>(type: "bit", nullable: true),
                    tipoIdentificacionSujetoRetenido = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    razonSocialSujetoRetenido = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    identificacionSujetoRetenido = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    periodoFiscal = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    tipoDocumento = table.Column<int>(type: "int", nullable: true),
                    versionXml = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    idTipoEstadoDocumento = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idDocumentoEmitir = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEstablecimiento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPuntoEmision = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ruta = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    idTipoEstadoSri = table.Column<int>(type: "int", nullable: true),
                    idTipoDocumento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numAutDocSustento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idFactura = table.Column<int>(type: "int", nullable: false),
                    fechaAutorizacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    receptorCorreo = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    correoEnviado = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("retenciones_pkey", x => x.idRetencion);
                    table.ForeignKey(
                        name: "FK_retenciones_sriFactura",
                        column: x => x.idFactura,
                        principalTable: "SriFacturas",
                        principalColumn: "idFactura");
                    table.ForeignKey(
                        name: "empresas_",
                        column: x => x.idEmpresa,
                        principalTable: "empresas",
                        principalColumn: "idEmpresa");
                    table.ForeignKey(
                        name: "retenciones_idDocumentoEmitir_fkey",
                        column: x => x.idDocumentoEmitir,
                        principalTable: "documentosEmitir",
                        principalColumn: "idDocumentoEmitir");
                    table.ForeignKey(
                        name: "retenciones_idEstablecimiento_fkey",
                        column: x => x.idEstablecimiento,
                        principalTable: "establecimientos",
                        principalColumn: "idEstablecimiento");
                    table.ForeignKey(
                        name: "retenciones_idPuntoEmision_fkey",
                        column: x => x.idPuntoEmision,
                        principalTable: "puntoEmisiones",
                        principalColumn: "idPuntoEmision");
                    table.ForeignKey(
                        name: "retenciones_idTipoEstadoDocumento_fkey",
                        column: x => x.idTipoEstadoDocumento,
                        principalTable: "tipoEstadoDocumentos",
                        principalColumn: "idTipoEstadoDocumento");
                    table.ForeignKey(
                        name: "retenciones_idTipoEstadoSri_fkey",
                        column: x => x.idTipoEstadoSri,
                        principalTable: "tipoEstadoSri",
                        principalColumn: "idTipoEstadoSri");
                    table.ForeignKey(
                        name: "retenciones_idUsuario_fkey",
                        column: x => x.idUsuario,
                        principalTable: "usuarios",
                        principalColumn: "idUsuario");
                    table.ForeignKey(
                        name: "tipoDocumento",
                        column: x => x.idTipoDocumento,
                        principalTable: "tipoDocumentos",
                        principalColumn: "idTipoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "SriDetallesFacturas",
                columns: table => new
                {
                    idDetalleFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idFactura = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    codigoPrincipal = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idProducto = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    descuento = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    precioTotalSinImpuesto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    precioTotalConImpuesto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    precioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriDetal__DFF38252565B8160", x => x.idDetalleFactura);
                    table.ForeignKey(
                        name: "SriDetallesFacturas_idFactura_fkey",
                        column: x => x.idFactura,
                        principalTable: "SriFacturas",
                        principalColumn: "idFactura");
                    table.ForeignKey(
                        name: "SriDetallesFacturas_idProducto_fkey",
                        column: x => x.idProducto,
                        principalTable: "SriProductos",
                        principalColumn: "idProducto");
                });

            migrationBuilder.CreateTable(
                name: "SriPrecios",
                columns: table => new
                {
                    idPrecio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProducto = table.Column<int>(type: "int", nullable: true),
                    baseImponible = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    codigo = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    valor = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    tarifa = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    totalConImpuestos = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriPreci__BF8B120CC62B3C36", x => x.idPrecio);
                    table.ForeignKey(
                        name: "SriPrecios_codigo_fkey",
                        column: x => x.codigo,
                        principalTable: "SriTarifasImpuestos",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "SriPrecios_idProducto_fkey",
                        column: x => x.idProducto,
                        principalTable: "SriProductos",
                        principalColumn: "idProducto");
                });

            migrationBuilder.CreateTable(
                name: "detalleProformas",
                columns: table => new
                {
                    idDetalleProforma = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    cantidad = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    descuento = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    precio = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    idIva = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    porcentaje = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    valorPorcentaje = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    subtotal = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    total = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    idProforma = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalleProformas_pkey", x => x.idDetalleProforma);
                    table.ForeignKey(
                        name: "idProforma",
                        column: x => x.idProforma,
                        principalTable: "proformas",
                        principalColumn: "idProforma");
                });

            migrationBuilder.CreateTable(
                name: "impuestoRetenciones",
                columns: table => new
                {
                    idImpuestoRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    baseImponible = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    porcentajeRetener = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    valorRetenido = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    codDocSustento = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    numDocSustento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    fechaEmisionDocSustento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idTipoValorRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPorcentajeImpuestoRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    idRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("impuestoRetenciones_pkey", x => x.idImpuestoRetencion);
                    table.ForeignKey(
                        name: "impuestoRetenciones_idPorcentajeImpuestoRetencion_fkey",
                        column: x => x.idPorcentajeImpuestoRetencion,
                        principalTable: "porcentajeImpuestosRetenciones",
                        principalColumn: "idPorcentajeImpuestoRetencion");
                    table.ForeignKey(
                        name: "impuestoRetenciones_idTipoValorRetencion_fkey",
                        column: x => x.idTipoValorRetencion,
                        principalTable: "tipoValorRetenciones",
                        principalColumn: "idTipoValorRetencion");
                    table.ForeignKey(
                        name: "tipoRetencionIva_idRetencion_fkey",
                        column: x => x.idRetencion,
                        principalTable: "retenciones",
                        principalColumn: "idRetencion");
                });

            migrationBuilder.CreateTable(
                name: "informacionAdicionalRetencion",
                columns: table => new
                {
                    idInformacionAdicionalRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    valor = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    idRetencion = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("informacionAdicionalRetencion_pkey", x => x.idInformacionAdicionalRetencion);
                    table.ForeignKey(
                        name: "informacionAdicionalRetencion_idRetencion_fkey",
                        column: x => x.idRetencion,
                        principalTable: "retenciones",
                        principalColumn: "idRetencion");
                });

            migrationBuilder.CreateTable(
                name: "SriDetallesFacturasImpuestos",
                columns: table => new
                {
                    idDetalleFacturaImpuesto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDetalleFactura = table.Column<int>(type: "int", nullable: true),
                    baseImponible = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    codigo = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    codigoPorcentaje = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    tarifa = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SriDetal__5383C987320853EA", x => x.idDetalleFacturaImpuesto);
                    table.ForeignKey(
                        name: "SriDetallesFacturasImpuestos_codigoPorcentaje_fkey",
                        column: x => x.codigoPorcentaje,
                        principalTable: "SriTarifasImpuestos",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "SriDetallesFacturasImpuestos_idDetalleFactura_fkey",
                        column: x => x.idDetalleFactura,
                        principalTable: "SriDetallesFacturas",
                        principalColumn: "idDetalleFactura");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_idEmpresa",
                table: "Cajas",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_idProvincia",
                table: "ciudades",
                column: "idProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_idCiudad",
                table: "clientes",
                column: "idCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_idEmpresa",
                table: "clientes",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_idTipoIdentificacion",
                table: "clientes",
                column: "idTipoIdentificacion");

            migrationBuilder.CreateIndex(
                name: "IX_DenominacionesDinero_idTipoDenominacion",
                table: "DenominacionesDinero",
                column: "idTipoDenominacion");

            migrationBuilder.CreateIndex(
                name: "IX_detalleFacturas_idFactura",
                table: "detalleFacturas",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_detalleFacturas_idIva",
                table: "detalleFacturas",
                column: "idIva");

            migrationBuilder.CreateIndex(
                name: "IX_detalleFacturas_idProducto",
                table: "detalleFacturas",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_detalleFormaPagos_idFactura",
                table: "detalleFormaPagos",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_detalleFormaPagos_idFormaPago",
                table: "detalleFormaPagos",
                column: "idFormaPago");

            migrationBuilder.CreateIndex(
                name: "IX_detalleFormaPagos_idTiempoFormaPago",
                table: "detalleFormaPagos",
                column: "idTiempoFormaPago");

            migrationBuilder.CreateIndex(
                name: "IX_detallePrecioProductos_idIva",
                table: "detallePrecioProductos",
                column: "idIva");

            migrationBuilder.CreateIndex(
                name: "IX_detallePrecioProductos_idProducto",
                table: "detallePrecioProductos",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_detalleProformas_idProforma",
                table: "detalleProformas",
                column: "idProforma");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCajas_idCaja",
                table: "DetallesCajas",
                column: "idCaja");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCajas_idDenominacion",
                table: "DetallesCajas",
                column: "idDenominacion");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCajasCierres_idCaja",
                table: "DetallesCajasCierres",
                column: "idCaja");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCajasCierres_idDenominacion",
                table: "DetallesCajasCierres",
                column: "idDenominacion");

            migrationBuilder.CreateIndex(
                name: "IX_documentosEmitir_idTipoDocumento",
                table: "documentosEmitir",
                column: "idTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_empleados_idCiudad",
                table: "empleados",
                column: "idCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_empleados_idEmpresa",
                table: "empleados",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_empleados_idTipoIdentificacion",
                table: "empleados",
                column: "idTipoIdentificacion");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_idInformacionFirma",
                table: "empresas",
                column: "idInformacionFirma");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_idTipoNegocio",
                table: "empresas",
                column: "idTipoNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_establecimientos_idEmpresa",
                table: "establecimientos",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_idTipoEstadoDocumento",
                table: "facturas",
                column: "idTipoEstadoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_idTipoEstadoSri",
                table: "facturas",
                column: "idTipoEstadoSri");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_idUsuario",
                table: "facturas",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_impuestoRetenciones_idPorcentajeImpuestoRetencion",
                table: "impuestoRetenciones",
                column: "idPorcentajeImpuestoRetencion");

            migrationBuilder.CreateIndex(
                name: "IX_impuestoRetenciones_idRetencion",
                table: "impuestoRetenciones",
                column: "idRetencion");

            migrationBuilder.CreateIndex(
                name: "IX_impuestoRetenciones_idTipoValorRetencion",
                table: "impuestoRetenciones",
                column: "idTipoValorRetencion");

            migrationBuilder.CreateIndex(
                name: "IX_informacionAdicional_idFactura",
                table: "informacionAdicional",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_informacionAdicionalRetencion_idRetencion",
                table: "informacionAdicionalRetencion",
                column: "idRetencion");

            migrationBuilder.CreateIndex(
                name: "IX_lotes_idProducto",
                table: "lotes",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_lotes_idUsuario",
                table: "lotes",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_porcentajeImpuestosRetenciones_idTipoValorRetencion",
                table: "porcentajeImpuestosRetenciones",
                column: "idTipoValorRetencion");

            migrationBuilder.CreateIndex(
                name: "IX_productos_idEmpresa",
                table: "productos",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_productos_idIva",
                table: "productos",
                column: "idIva");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosProveedores_identificacion",
                table: "ProductosProveedores",
                column: "identificacion");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosProveedores_idProducto",
                table: "ProductosProveedores",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_proformas_idEstablecimiento",
                table: "proformas",
                column: "idEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_proformas_idPuntoEmision",
                table: "proformas",
                column: "idPuntoEmision");

            migrationBuilder.CreateIndex(
                name: "IX_proformas_idUsuario",
                table: "proformas",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_idCiudad",
                table: "proveedores",
                column: "idCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_idEmpresa",
                table: "proveedores",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_idTipoIdentificacion",
                table: "proveedores",
                column: "idTipoIdentificacion");

            migrationBuilder.CreateIndex(
                name: "IX_puntoEmisiones_idEmpresa",
                table: "puntoEmisiones",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idDocumentoEmitir",
                table: "retenciones",
                column: "idDocumentoEmitir");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idEmpresa",
                table: "retenciones",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idEstablecimiento",
                table: "retenciones",
                column: "idEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idFactura",
                table: "retenciones",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idPuntoEmision",
                table: "retenciones",
                column: "idPuntoEmision");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idTipoDocumento",
                table: "retenciones",
                column: "idTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idTipoEstadoDocumento",
                table: "retenciones",
                column: "idTipoEstadoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idTipoEstadoSri",
                table: "retenciones",
                column: "idTipoEstadoSri");

            migrationBuilder.CreateIndex(
                name: "IX_retenciones_idUsuario",
                table: "retenciones",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_secuenciales_idEmpresa",
                table: "secuenciales",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_secuenciales_idTipoDocumento",
                table: "secuenciales",
                column: "idTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_secuencialesProformas_idEmpresa",
                table: "secuencialesProformas",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_secuencialesProformas_idTipoDocumento",
                table: "secuencialesProformas",
                column: "idTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_SriCamposAdicionales_idFactura",
                table: "SriCamposAdicionales",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_SriDetallesFacturas_idFactura",
                table: "SriDetallesFacturas",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_SriDetallesFacturas_idProducto",
                table: "SriDetallesFacturas",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_SriDetallesFacturasImpuestos_codigoPorcentaje",
                table: "SriDetallesFacturasImpuestos",
                column: "codigoPorcentaje");

            migrationBuilder.CreateIndex(
                name: "IX_SriDetallesFacturasImpuestos_idDetalleFactura",
                table: "SriDetallesFacturasImpuestos",
                column: "idDetalleFactura");

            migrationBuilder.CreateIndex(
                name: "IX_SriFacturas_ambiente",
                table: "SriFacturas",
                column: "ambiente");

            migrationBuilder.CreateIndex(
                name: "IX_SriFacturas_codDoc",
                table: "SriFacturas",
                column: "codDoc");

            migrationBuilder.CreateIndex(
                name: "IX_SriFacturas_moneda",
                table: "SriFacturas",
                column: "moneda");

            migrationBuilder.CreateIndex(
                name: "IX_SriPagos_formaPago",
                table: "SriPagos",
                column: "formaPago");

            migrationBuilder.CreateIndex(
                name: "IX_SriPagos_idFactura",
                table: "SriPagos",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_SriPagos_unidadTiempo",
                table: "SriPagos",
                column: "unidadTiempo");

            migrationBuilder.CreateIndex(
                name: "IX_SriPersonas_tipoIdentificacion",
                table: "SriPersonas",
                column: "tipoIdentificacion");

            migrationBuilder.CreateIndex(
                name: "IX_SriPrecios_codigo",
                table: "SriPrecios",
                column: "codigo");

            migrationBuilder.CreateIndex(
                name: "IX_SriPrecios_idProducto",
                table: "SriPrecios",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_SriProductos_idEmpresa",
                table: "SriProductos",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_SriProductos_identificacion",
                table: "SriProductos",
                column: "identificacion");

            migrationBuilder.CreateIndex(
                name: "IX_SriTotalesConImpuestos_codigoPorcentaje",
                table: "SriTotalesConImpuestos",
                column: "codigoPorcentaje");

            migrationBuilder.CreateIndex(
                name: "IX_SriTotalesConImpuestos_idFactura",
                table: "SriTotalesConImpuestos",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioEmpresas_idEmpresa",
                table: "usuarioEmpresas",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioEmpresas_idUsuario",
                table: "usuarioEmpresas",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "detalleFacturas");

            migrationBuilder.DropTable(
                name: "detalleFormaPagos");

            migrationBuilder.DropTable(
                name: "detallePrecioProductos");

            migrationBuilder.DropTable(
                name: "detalleProformas");

            migrationBuilder.DropTable(
                name: "DetallesCajas");

            migrationBuilder.DropTable(
                name: "DetallesCajasCierres");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "impuestoRetenciones");

            migrationBuilder.DropTable(
                name: "informacionAdicional");

            migrationBuilder.DropTable(
                name: "informacionAdicionalRetencion");

            migrationBuilder.DropTable(
                name: "lotes");

            migrationBuilder.DropTable(
                name: "ProductosProveedores");

            migrationBuilder.DropTable(
                name: "proveedores");

            migrationBuilder.DropTable(
                name: "secuenciales");

            migrationBuilder.DropTable(
                name: "secuencialesProformas");

            migrationBuilder.DropTable(
                name: "SriCamposAdicionales");

            migrationBuilder.DropTable(
                name: "SriDetallesFacturasImpuestos");

            migrationBuilder.DropTable(
                name: "SriEstados");

            migrationBuilder.DropTable(
                name: "SriPagos");

            migrationBuilder.DropTable(
                name: "SriPrecios");

            migrationBuilder.DropTable(
                name: "SriTotalesConImpuestos");

            migrationBuilder.DropTable(
                name: "tipoIdentificacionesGeneracionDocumentos");

            migrationBuilder.DropTable(
                name: "usuarioEmpresas");

            migrationBuilder.DropTable(
                name: "tiempoFormaPagos");

            migrationBuilder.DropTable(
                name: "formaPagos");

            migrationBuilder.DropTable(
                name: "proformas");

            migrationBuilder.DropTable(
                name: "Cajas");

            migrationBuilder.DropTable(
                name: "DenominacionesDinero");

            migrationBuilder.DropTable(
                name: "porcentajeImpuestosRetenciones");

            migrationBuilder.DropTable(
                name: "facturas");

            migrationBuilder.DropTable(
                name: "retenciones");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "ciudades");

            migrationBuilder.DropTable(
                name: "tipoIdentificaciones");

            migrationBuilder.DropTable(
                name: "SriDetallesFacturas");

            migrationBuilder.DropTable(
                name: "SriFormasPagos");

            migrationBuilder.DropTable(
                name: "SriUnidadesTiempos");

            migrationBuilder.DropTable(
                name: "SriTarifasImpuestos");

            migrationBuilder.DropTable(
                name: "TiposDenominacionesDinero");

            migrationBuilder.DropTable(
                name: "tipoValorRetenciones");

            migrationBuilder.DropTable(
                name: "documentosEmitir");

            migrationBuilder.DropTable(
                name: "establecimientos");

            migrationBuilder.DropTable(
                name: "puntoEmisiones");

            migrationBuilder.DropTable(
                name: "tipoEstadoDocumentos");

            migrationBuilder.DropTable(
                name: "tipoEstadoSri");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "ivas");

            migrationBuilder.DropTable(
                name: "provincias");

            migrationBuilder.DropTable(
                name: "SriFacturas");

            migrationBuilder.DropTable(
                name: "SriProductos");

            migrationBuilder.DropTable(
                name: "tipoDocumentos");

            migrationBuilder.DropTable(
                name: "SriAmbientes");

            migrationBuilder.DropTable(
                name: "SriTiposDocumentos");

            migrationBuilder.DropTable(
                name: "SriMonedas");

            migrationBuilder.DropTable(
                name: "empresas");

            migrationBuilder.DropTable(
                name: "SriPersonas");

            migrationBuilder.DropTable(
                name: "tipoNegocios");

            migrationBuilder.DropTable(
                name: "informacionFirmas");

            migrationBuilder.DropTable(
                name: "SriTiposIdentificaciones");
        }
    }
}
