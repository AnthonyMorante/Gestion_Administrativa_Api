using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gestion_Administrativa_Api.Migrations
{
    /// <inheritdoc />
    public partial class ocean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "formaPagos",
                columns: table => new
                {
                    idFormaPago = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    codigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("formaPagos_pkey", x => x.idFormaPago);
                });

            migrationBuilder.CreateTable(
                name: "informacionFirmas",
                columns: table => new
                {
                    idInformacionFirma = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    razonSocial = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    identificacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    ruta = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    codigo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("informacionFirmas_pkey", x => x.idInformacionFirma);
                });

            migrationBuilder.CreateTable(
                name: "ivas",
                columns: table => new
                {
                    idIva = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    valor = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    codigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ivas_pkey", x => x.idIva);
                });

            migrationBuilder.CreateTable(
                name: "provincias",
                columns: table => new
                {
                    idProvincia = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("provincias_pkey", x => x.idProvincia);
                });

            migrationBuilder.CreateTable(
                name: "tiempoFormaPagos",
                columns: table => new
                {
                    idTiempoFormaPago = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    codigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tiempoFormaPagos_pkey", x => x.idTiempoFormaPago);
                });

            migrationBuilder.CreateTable(
                name: "tipoDocumentos",
                columns: table => new
                {
                    idTipoDocumento = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    codigo = table.Column<int>(type: "integer", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoDocumentos_pkey", x => x.idTipoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "tipoEstadoDocumentos",
                columns: table => new
                {
                    idTipoEstadoDocumento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    codigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoEstadoDocumentos_pkey", x => x.idTipoEstadoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "tipoEstadoSri",
                columns: table => new
                {
                    idTipoEstadoSri = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    codigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoEstadoSri_pkey", x => x.idTipoEstadoSri);
                });

            migrationBuilder.CreateTable(
                name: "tipoIdentificaciones",
                columns: table => new
                {
                    idTipoIdentificacion = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    codigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoIdentificaciones_pkey", x => x.idTipoIdentificacion);
                });

            migrationBuilder.CreateTable(
                name: "tipoIdentificacionesGeneracionDocumentos",
                columns: table => new
                {
                    idTipoIdentificacionesGeneracionDocumentos = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    codigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoIdentificacionesGeneracionDocumentos_pkey", x => x.idTipoIdentificacionesGeneracionDocumentos);
                });

            migrationBuilder.CreateTable(
                name: "tipoNegocios",
                columns: table => new
                {
                    idTipoNegocio = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    codigo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipoNegocios_pkey", x => x.idTipoNegocio);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    clave = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuarios_pkey", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "ciudades",
                columns: table => new
                {
                    idCiudad = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    idProvincia = table.Column<Guid>(type: "uuid", nullable: true)
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
                name: "documentosEmitir",
                columns: table => new
                {
                    idDocumentoEmitir = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    idTipoDocumento = table.Column<Guid>(type: "uuid", nullable: true)
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
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    identificacion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    razonSocial = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    telefono = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    llevaContabilidad = table.Column<bool>(type: "boolean", nullable: true),
                    regimenMicroEmpresas = table.Column<bool>(type: "boolean", nullable: true),
                    regimenRimpe = table.Column<bool>(type: "boolean", nullable: true),
                    agenteRetencion = table.Column<bool>(type: "boolean", nullable: true),
                    idTipoNegocio = table.Column<Guid>(type: "uuid", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    direccionMatriz = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    idInformacionFirma = table.Column<Guid>(type: "uuid", nullable: true),
                    resolucionAgenteRetencion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
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
                name: "facturas",
                columns: table => new
                {
                    idFactura = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    agenteRetencion = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    ambiente = table.Column<int>(type: "integer", nullable: true),
                    claveAcceso = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    codigoDocModificado = table.Column<int>(type: "integer", nullable: true),
                    contribuyenteRimpe = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    contribuyenteEspecial = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    direccionEstablecimiento = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    direccionMatriz = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    emisorRazonSocial = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    emisorRuc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    establecimiento = table.Column<int>(type: "integer", nullable: true),
                    puntoEmision = table.Column<int>(type: "integer", nullable: true),
                    secuencial = table.Column<int>(type: "integer", nullable: true),
                    fechaAutorizacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechaEmision = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    exentoIva = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    ice = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    irbpnr = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    isd = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    iva12 = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    subtotal0 = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    moneda = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    obligadoContabilidad = table.Column<bool>(type: "boolean", nullable: true),
                    receptorRazonSocial = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    receptorRuc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    receptorTipoIdentificacion = table.Column<int>(type: "integer", nullable: true),
                    regimenMicroempresas = table.Column<bool>(type: "boolean", nullable: true),
                    tipoDocumento = table.Column<int>(type: "integer", nullable: true),
                    tipoEmision = table.Column<int>(type: "integer", nullable: true),
                    totalDescuento = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    totalImporte = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    totalSinImpuesto = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    versionXml = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    idTipoEstadoDocumento = table.Column<int>(type: "integer", nullable: false),
                    idUsuario = table.Column<Guid>(type: "uuid", nullable: false),
                    receptorTelefono = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    receptorCorreo = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    receptorDireccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    idCliente = table.Column<Guid>(type: "uuid", nullable: false),
                    idCiudad = table.Column<Guid>(type: "uuid", nullable: false),
                    idDocumentoEmitir = table.Column<Guid>(type: "uuid", nullable: false),
                    idEstablecimiento = table.Column<Guid>(type: "uuid", nullable: false),
                    idPuntoEmision = table.Column<Guid>(type: "uuid", nullable: false),
                    subtotal12 = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    regimenRimpe = table.Column<bool>(type: "boolean", nullable: true),
                    ruta = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    resolucionAgenteRetencion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    saldo = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    valorRecibido = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    cambio = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    idTipoEstadoSri = table.Column<int>(type: "integer", nullable: true),
                    mensaje = table.Column<string>(type: "text", nullable: true)
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
                name: "clientes",
                columns: table => new
                {
                    idCliente = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    identificacion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    razonSocial = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    direccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    telefono = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    observacion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    idCiudad = table.Column<Guid>(type: "uuid", nullable: true),
                    idTipoIdentificacion = table.Column<Guid>(type: "uuid", nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true")
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
                    idEmpleado = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    identificacion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    razonSocial = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    direccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    telefono = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    observacion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    idCiudad = table.Column<Guid>(type: "uuid", nullable: true),
                    idTipoIdentificacion = table.Column<Guid>(type: "uuid", nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true")
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
                    idEstablecimiento = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    predeterminado = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    nombre = table.Column<long>(type: "bigint", nullable: true),
                    direccion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
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
                    idProducto = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    codigo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    precio = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    idIva = table.Column<Guid>(type: "uuid", nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: true),
                    activoProducto = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    totalIva = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    cantidad = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true)
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
                    idProveedor = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    identificacion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    razonSocial = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    representante = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    direccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    telefono = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    paginaWeb = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    observacion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    idCiudad = table.Column<Guid>(type: "uuid", nullable: true),
                    idTipoIdentificacion = table.Column<Guid>(type: "uuid", nullable: true),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
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
                    idPuntoEmision = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    predeterminado = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    nombre = table.Column<long>(type: "bigint", nullable: true),
                    direccion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
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
                    idSecuencial = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<long>(type: "bigint", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: true),
                    idTipoDocumento = table.Column<Guid>(type: "uuid", nullable: true)
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
                    idSecuencialesProforma = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<long>(type: "bigint", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: true),
                    idTipoDocumento = table.Column<Guid>(type: "uuid", nullable: true)
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
                name: "usuarioEmpresas",
                columns: table => new
                {
                    idUsuarioEmpresas = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    idUsuario = table.Column<Guid>(type: "uuid", nullable: false),
                    idEmpresa = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true")
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
                name: "detalleFacturas",
                columns: table => new
                {
                    idDetalleFactura = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    cantidad = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    descuento = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    precio = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    idIva = table.Column<Guid>(type: "uuid", nullable: false),
                    idProducto = table.Column<Guid>(type: "uuid", nullable: false),
                    porcentaje = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    valorPorcentaje = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    subtotal = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    total = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    idFactura = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalleFacturas_pkey", x => x.idDetalleFactura);
                    table.ForeignKey(
                        name: "detalleFacturas_idFactura_fkey",
                        column: x => x.idFactura,
                        principalTable: "facturas",
                        principalColumn: "idFactura");
                });

            migrationBuilder.CreateTable(
                name: "detalleFormaPagos",
                columns: table => new
                {
                    idDetalleFormaPago = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    plazo = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    valor = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    idTiempoFormaPago = table.Column<Guid>(type: "uuid", nullable: false),
                    idFactura = table.Column<Guid>(type: "uuid", nullable: false),
                    idFormaPago = table.Column<Guid>(type: "uuid", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "informacionAdicional",
                columns: table => new
                {
                    idInformacionAdicional = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    valor = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    idFactura = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "detallePrecioProductos",
                columns: table => new
                {
                    idDetallePrecioProducto = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    totalIva = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    porcentaje = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    total = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    idProducto = table.Column<Guid>(type: "uuid", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    idIva = table.Column<Guid>(type: "uuid", nullable: true)
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
                name: "proformas",
                columns: table => new
                {
                    idProforma = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ambiente = table.Column<int>(type: "integer", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    moneda = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    receptorRazonSocial = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    receptorRuc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    receptorTipoIdentificacion = table.Column<int>(type: "integer", nullable: true),
                    receptorTelefono = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    receptorCorreo = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    receptorDireccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    totalDescuento = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    totalImporte = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    totalSinImpuesto = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    idUsuario = table.Column<Guid>(type: "uuid", nullable: false),
                    idEstablecimiento = table.Column<Guid>(type: "uuid", nullable: false),
                    idPuntoEmision = table.Column<Guid>(type: "uuid", nullable: false),
                    subtotal12 = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    establecimiento = table.Column<int>(type: "integer", nullable: true),
                    puntoEmision = table.Column<int>(type: "integer", nullable: true),
                    secuencial = table.Column<int>(type: "integer", nullable: true)
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
                name: "detalleProformas",
                columns: table => new
                {
                    idDetalleProforma = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    cantidad = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    descuento = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    precio = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    idIva = table.Column<Guid>(type: "uuid", nullable: false),
                    idProducto = table.Column<Guid>(type: "uuid", nullable: false),
                    porcentaje = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    valorPorcentaje = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    subtotal = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    total = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    idProforma = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_detalleFacturas_idFactura",
                table: "detalleFacturas",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_detalleFormaPagos_idFactura",
                table: "detalleFormaPagos",
                column: "idFactura");

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
                name: "IX_informacionAdicional_idFactura",
                table: "informacionAdicional",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_productos_idEmpresa",
                table: "productos",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_productos_idIva",
                table: "productos",
                column: "idIva");

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
                name: "documentosEmitir");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "formaPagos");

            migrationBuilder.DropTable(
                name: "informacionAdicional");

            migrationBuilder.DropTable(
                name: "proveedores");

            migrationBuilder.DropTable(
                name: "secuenciales");

            migrationBuilder.DropTable(
                name: "secuencialesProformas");

            migrationBuilder.DropTable(
                name: "tipoIdentificacionesGeneracionDocumentos");

            migrationBuilder.DropTable(
                name: "usuarioEmpresas");

            migrationBuilder.DropTable(
                name: "tiempoFormaPagos");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "proformas");

            migrationBuilder.DropTable(
                name: "facturas");

            migrationBuilder.DropTable(
                name: "ciudades");

            migrationBuilder.DropTable(
                name: "tipoIdentificaciones");

            migrationBuilder.DropTable(
                name: "tipoDocumentos");

            migrationBuilder.DropTable(
                name: "ivas");

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
                name: "provincias");

            migrationBuilder.DropTable(
                name: "empresas");

            migrationBuilder.DropTable(
                name: "tipoNegocios");

            migrationBuilder.DropTable(
                name: "informacionFirmas");
        }
    }
}
