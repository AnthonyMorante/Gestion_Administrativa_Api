using AutoMapper;
using Gestion_Administrativa_Api.Documents_Models.Factura;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;

namespace Gestion_Administrativa_Api.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            #region detAdicional_V1_0_0

            CreateMap<DetalleFormaPagos, pago_V1_0_0>()
                  .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.Valor))

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
                    .ForMember(dest => dest.importeTotal, opt => opt.MapFrom(src => src.TotalImporte))
                    .ForMember(dest => dest.moneda, opt => opt.MapFrom(src => src.Moneda.ToLower()))
                    .ForMember(dest => dest.razonSocial, opt => opt.MapFrom(src => src.EmisorRazonSocial.ToUpper()))
                    .ForMember(dest => dest.nombreComercial, opt => opt.MapFrom(src => src.EmisorRazonSocial.ToUpper()))
                    .ForMember(dest => dest.ruc, opt => opt.MapFrom(src => src.EmisorRuc.ToUpper()))
            .ReverseMap();
            ;
            #endregion


            #region infoFactura_V1_0_0

            CreateMap<Facturas, infoFactura_V1_0_0>()
                .ForMember(dest => dest.obligadoContabilidad, opt => opt.MapFrom(src => Convert.ToBoolean(src.ObligadoContabilidad) == true ? "SI" : null))
                .ForMember(dest => dest.contribuyenteEspecial, opt => opt.MapFrom(src => Convert.ToBoolean(src.ContribuyenteEspecial) == true ? "SI" : null))
                .ForMember(dest => dest.dirEstablecimiento, opt => opt.MapFrom(src => src.DireccionEstablecimiento))
                .ForMember(dest => dest.direccionComprador, opt => opt.MapFrom(src => src.ReceptorRazonSocial))
                .ForMember(dest => dest.identificacionComprador, opt => opt.MapFrom(src => src.ReceptorRuc))
                .ForMember(dest => dest.totalSinImpuestos, opt => opt.MapFrom(src => src.TotalSinImpuesto))
                .ForMember(dest => dest.moneda, opt => opt.MapFrom(src => src.Moneda.ToLower()))
                .ForMember(dest => dest.importeTotal, opt => opt.MapFrom(src => src.TotalImporte))


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
            .ForMember(dest => dest.Descuento, opt => opt.MapFrom(src => src.descuento == null ? 0 :src.descuento))
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
            .ForMember(dest => dest.TotalDescuento, opt => opt.MapFrom(src => src.totalDecuento==null ? 0 : src.totalDecuento))


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

        }



    }
}
