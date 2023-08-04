using AutoMapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;

namespace Gestion_Administrativa_Api.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {


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
