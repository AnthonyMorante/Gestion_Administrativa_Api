using AutoMapper;
using Gestion_Administrativa_Api.Documents_Models.Factura;
using Gestion_Administrativa_Api.Documents_Models.Retencion;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;
using static Gestion_Administrativa_Api.Dtos.Interfaz.RetencionDto;

namespace Gestion_Administrativa_Api.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {



            #region FormaPago

            CreateMap<DetalleDto, detalle_V1_0_0>()
                  .ForMember(dest => dest.codigoPrincipal, opt => opt.MapFrom(src => src.codigo))
                  .ForMember(dest => dest.codigoAuxiliar, opt => opt.MapFrom(src => src.codigo))
                  .ForMember(dest => dest.descripcion, opt => opt.MapFrom(src => src.nombre))
                  .ForMember(dest => dest.descuento, opt => opt.MapFrom(src => src.descuento == null ? 0 : src.descuento))
                  .ForMember(dest => dest.precioTotalSinImpuesto, opt => opt.MapFrom(src => src.totalSinIva))
                  .ForMember(dest => dest.precioUnitario, opt => opt.MapFrom(src => src.valorProductoSinIva))
                  //.ForMember(dest => dest.totalConImpuestos, opt => opt.MapFrom(src => src.total))
                  .ForMember(dest => dest.impuestos, opt => opt.MapFrom(src => new List<impuesto_V1_0_0>
                    {
                        new impuesto_V1_0_0
                        {
                            codigo=2,//por que es iva tabla 16,
                            codigoPorcentaje = src.idIva ==  Guid.Parse("53347a4d-5c75-42e8-9456-595a728306aa") ? 2:
                            src.idIva ==  Guid.Parse("d4c41fb5-1791-4739-8285-a312e010afa9") ? 0:
                            src.idIva ==  Guid.Parse("aaf450c1-058f-4406-8c69-7ab3b1d1c339") ? 6:
                            src.idIva ==  Guid.Parse("8980f44a-df38-400a-9d89-4150cadd13ba") ? 7: -1,
                            tarifa=src.tarifaPorcentaje,
                            baseImponible=src.totalSinIva,
                            valor=src.porcentaje

                        }
                    }))

            .ReverseMap();
            ;
            #endregion


            #region pago_V1_0_0

            CreateMap<DetalleFormaPagos, pago_V1_0_0>()
                  .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.Valor))
                  .ForMember(d => d.unidadTiempo, opt =>
                  {

                      opt.MapFrom(src => src.IdTiempoFormaPago == Guid.Parse("0c99e1ec-c09e-41e4-80e1-1f0769c68593") ? "dias" :
                                         src.IdTiempoFormaPago == Guid.Parse("3d558987-97d7-4c3a-a4cb-71fc4971e61b") ? "meses" :
                                         null);
                  })
                  .ForMember(d => d.formaPago, opt =>
                  {

                      opt.MapFrom(src => src.IdFormaPago == Guid.Parse("234c0c98-1831-42ab-a3bb-9b4b2caae4f1") ? "01" :
                                         src.IdFormaPago == Guid.Parse("94a782b2-d3cc-4585-9701-35c99dcf141b") ? "15" :
                                         src.IdFormaPago == Guid.Parse("0159cfa0-144a-4c57-8053-2d98934d7e10") ? "16" :
                                         src.IdFormaPago == Guid.Parse("5a7a7fb4-8be6-44b9-8925-da5a75cf36f2") ? "17" :
                                         src.IdFormaPago == Guid.Parse("fc7b9f2a-bb33-4da3-861e-86113f4abc78") ? "18" :
                                         src.IdFormaPago == Guid.Parse("11ffab20-dd28-48dc-a6ea-6b367a7a08ac") ? "19" :
                                         src.IdFormaPago == Guid.Parse("fc7b9f2a-bb33-4da3-861e-86113f4abc78") ? "20" :
                                         src.IdFormaPago == Guid.Parse("6db235fe-e81c-4fe9-8a12-b3cf54ddcd92") ? "21" :
                                         null);
                  })

            .ReverseMap();
            ;
            #endregion

            #region detAdicional_V1_0_0

            CreateMap<InformacionAdicional, detAdicional_V1_0_0>()

            .ReverseMap();
            ;
            #endregion

            #region infoTributaria_V1_0_0

            CreateMap<Facturas, infoTributaria_V1_0_0>()
                    .ForMember(dest => dest.dirMatriz, opt => opt.MapFrom(src => src.DireccionMatriz))
                    .ForMember(dest => dest.codDoc, opt => opt.MapFrom(src => src.TipoDocumento.ToString().PadLeft(2, '0')))
                    .ForMember(dest => dest.estab, opt => opt.MapFrom(src => src.Establecimiento.ToString().PadLeft(3, '0')))
                    .ForMember(dest => dest.ptoEmi, opt => opt.MapFrom(src => src.PuntoEmision.ToString().PadLeft(3, '0')))
                    .ForMember(dest => dest.secuencial, opt => opt.MapFrom(src => src.Secuencial.ToString().PadLeft(9, '0')))
                    //.ForMember(dest => dest.importeTotal, opt => opt.MapFrom(src => src.TotalImporte))
                    //.ForMember(dest => dest.moneda, opt => opt.MapFrom(src => src.Moneda.ToLower()))
                    .ForMember(dest => dest.razonSocial, opt => opt.MapFrom(src => src.EmisorRazonSocial.ToUpper()))
                    .ForMember(dest => dest.nombreComercial, opt => opt.MapFrom(src => src.EmisorRazonSocial.ToUpper()))
                    .ForMember(dest => dest.ruc, opt => opt.MapFrom(src => src.EmisorRuc.ToUpper()))
                    .ForMember(dest => dest.agenteRetencion, opt => opt.MapFrom(src => Convert.ToBoolean(src.AgenteRetencion) == true ? "SI" : null))
                    .ForMember(dest => dest.contribuyenteRimpe, opt => opt.MapFrom(src => Convert.ToBoolean(src.RegimenRimpe) == true ? "CONTRIBUYENTE RÉGIMEN RIMPE" : null))
                    .ForMember(dest => dest.ruc, opt => opt.MapFrom(src => src.EmisorRuc.ToUpper()))
            .ReverseMap();
            ;
            #endregion

            #region infoFactura_V1_0_0

            CreateMap<Facturas, infoFactura_V1_0_0>()
                .ForMember(dest => dest.fechaEmision, opt => opt.MapFrom(src => $"{src.FechaEmision:dd/MM/yyyy}"))
                .ForMember(dest => dest.obligadoContabilidad, opt => opt.MapFrom(src => Convert.ToBoolean(src.ObligadoContabilidad) == true ? "SI" : null))
                .ForMember(dest => dest.contribuyenteEspecial, opt => opt.MapFrom(src => Convert.ToBoolean(src.ContribuyenteEspecial) == true ? "SI" : null))
                .ForMember(dest => dest.dirEstablecimiento, opt => opt.MapFrom(src => src.DireccionEstablecimiento))
                .ForMember(dest => dest.direccionComprador, opt => opt.MapFrom(src => src.ReceptorDireccion))
                .ForMember(dest => dest.razonSocialComprador, opt => opt.MapFrom(src => src.ReceptorRazonSocial))
                .ForMember(dest => dest.identificacionComprador, opt => opt.MapFrom(src => src.ReceptorRuc))
                .ForMember(dest => dest.totalSinImpuestos, opt => opt.MapFrom(src => src.TotalSinImpuesto))
                .ForMember(dest => dest.propina, opt => opt.MapFrom(src => "0.00"))
                .ForMember(dest => dest.moneda, opt => opt.MapFrom(src => src.Moneda.ToUpper()))
                .ForMember(dest => dest.importeTotal, opt => opt.MapFrom(src => src.TotalImporte))
                .ForMember(dest => dest.totalDescuento, opt => opt.MapFrom(src => src.TotalDescuento))
                .ForMember(dest => dest.tipoIdentificacionComprador, opt => opt.MapFrom(src => src.ReceptorTipoIdentificacion.ToString().PadLeft(2, '0')))



            .ReverseMap();
            ;
            #endregion

            #region InformacionAdicinal

            CreateMap<informacionAdicionalDto, InformacionAdicional>()
            .ReverseMap();
            ;



            #endregion

            #region FormaPago

            CreateMap<formaPagoDto, DetalleFormaPagos>()
            .ReverseMap();
            ;
            #endregion

            #region DetalleFactura

            CreateMap<DetalleDto, DetalleFacturas>()
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.totalSinIva))
            .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.valor))
            .ForMember(dest => dest.Descuento, opt => opt.MapFrom(src => src.descuento == null ? 0 : src.descuento))
            .ReverseMap();
            ;



            #endregion

            #region Factura

            CreateMap<FacturaDto, Facturas>()
            .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.fechaEmision))
            .ForMember(dest => dest.ReceptorRuc, opt => opt.MapFrom(src => src.identificacion))
            .ForMember(dest => dest.ReceptorCorreo, opt => opt.MapFrom(src => src.email))
            .ForMember(dest => dest.ReceptorDireccion, opt => opt.MapFrom(src => src.direccion))
            .ForMember(dest => dest.ReceptorRazonSocial, opt => opt.MapFrom(src => src.razonSocial))
            .ForMember(dest => dest.Iva12, opt => opt.MapFrom(src => src.iva12))
            .ForMember(dest => dest.TotalSinImpuesto, opt => opt.MapFrom(src => src.subtotal))
            .ForMember(dest => dest.Subtotal0, opt => opt.MapFrom(src => src.subtotal0))
            .ForMember(dest => dest.Subtotal12, opt => opt.MapFrom(src => src.subtotal12))
            .ForMember(dest => dest.TotalImporte, opt => opt.MapFrom(src => src.totalFactura))
            .ForMember(dest => dest.ReceptorTelefono, opt => opt.MapFrom(src => src.telefono))
            .ForMember(dest => dest.TotalDescuento, opt => opt.MapFrom(src => src.totDescuento == null ? 0 : src.totDescuento))
            .ForMember(dest => dest.ReceptorTipoIdentificacion, opt => opt.MapFrom(src => src.codigoTipoIdentificacion.ToString().PadLeft(2, '0')))
            .ReverseMap();
            ;



            #endregion

            #region Clientes

            CreateMap<ClientesDto, Clientes>();
            CreateMap<Clientes, Clientes>()
           .ForMember(dest => dest.Activo, opt => opt.Ignore())
           .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore()


             ).ReverseMap();


            #endregion

            #region Proveedores

            CreateMap<ProveedoresDto, Proveedores>();
            CreateMap<Proveedores, Proveedores>()
           .ForMember(dest => dest.Activo, opt => opt.Ignore())
           .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore()

             ).ReverseMap();


            #endregion

            #region Empleados

            CreateMap<EmpleadosDto, Empleados>();
            CreateMap<Empleados, Empleados>()
           .ForMember(dest => dest.Activo, opt => opt.Ignore())
           .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore()

             ).ReverseMap();


            #endregion

            #region Productos

            CreateMap<ProductosDto, Productos>()
           .ForMember(dest => dest.TotalIva, opt => opt.MapFrom(src => src.PrecioIva))
                ;


            CreateMap<Productos, Productos>()
           .ForMember(dest => dest.Activo, opt => opt.Ignore())
           .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore()

             ).ReverseMap();


            #endregion

            #region DetallePrecioProducto

            CreateMap<DetallePrecioProductosDto, DetallePrecioProductos>()
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Utilidad)).ReverseMap();


            #endregion

            #region Retenciones

            CreateMap<RetencionDto, Retenciones>()
            .ForMember(dest => dest.ObligadoContabilidad, opt => opt.Ignore())
            .ForMember(dest => dest.IdTipoEstadoDocumento, opt => opt.MapFrom(src => Convert.ToInt16(4)))
            .ForMember(dest => dest.IdTipoEstadoSri, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.Establecimiento, opt => opt.MapFrom(src => Convert.ToInt16(src.establecimiento)))
            .ForMember(dest => dest.PuntoEmision, opt => opt.MapFrom(src => Convert.ToInt16(src.puntoEmision)))
            .ForMember(dest => dest.Secuencial, opt => opt.MapFrom(src => Convert.ToInt16(src.secuencial)))
            .ForMember(dest => dest.CodigoDocumento, opt => opt.MapFrom(src => 7))
            .ForMember(dest => dest.RazonSocialSujetoRetenido, opt => opt.MapFrom(src => src.razonSocialSujetoRetenido))
            .ForMember(dest => dest.ObligadoContabilidad, opt => opt.MapFrom(src => true));

            #endregion

            #region InformacionAdicionalRetenciones

            CreateMap<InfoAdicional, InformacionAdicionalRetencion>();

            #endregion


            #region InformacionAdicionalRetenciones

            CreateMap<Impuesto, ImpuestoRetenciones>();

            #endregion




        }



    }
}
