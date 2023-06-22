using AutoMapper;
using Gestion_Administrativa_Api.Dtos;
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

                
                );


            #endregion

            #region Proveedores

            CreateMap<ProveedoresDto, Proveedores>();
            CreateMap<Proveedores, Proveedores>()
           .ForMember(dest => dest.Activo, opt => opt.Ignore())
           .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore()

             );


            #endregion

            #region Empleados

            CreateMap<EmpleadosDto, Empleados>();
            CreateMap<Empleados, Empleados>()
           .ForMember(dest => dest.Activo, opt => opt.Ignore())
           .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore()

             );


            #endregion

            #region Productos

            CreateMap<ProductosDto, Productos>();
            CreateMap<Productos, Productos>()
           .ForMember(dest => dest.Activo, opt => opt.Ignore())
           .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore()

             );


            #endregion

            #region DetallePrecioProducto

            CreateMap<DetallePrecioProductosDto, DetallePrecioProductos>()
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Utilidad));
       
  
            #endregion

        }



    }
}
