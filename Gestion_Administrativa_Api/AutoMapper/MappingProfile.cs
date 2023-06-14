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

        }



    }
}
