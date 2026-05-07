using AutoMapper;
using DTOs.DataTransferObjects;
using DTOs.Stay;
using Entities.Models;

namespace HMSWebApiProject
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomForCreationDTO, Room>();
            CreateMap<RoomForUpdateDTO, Room>();

            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerForCreationDTO, Customer>();
            CreateMap<CustomerForUpdateDTO, Customer>();

            CreateMap<Customer, CustomerBriefDTO>();

            CreateMap<Stay, StayDTO>()
                .ForMember(dest => dest.Customer,
                    opt => opt.MapFrom(src => src.Customer));


           
            CreateMap<StayForCreationDTO, Stay>();
            CreateMap<StayForUpdateDTO, Stay>();

            CreateMap<CabDriver, CabDriverDTO>();
            CreateMap<CabDriverForCreationDTO, CabDriver>();
            CreateMap<CabDriverForUpdateDTO, CabDriver>();

        }

    }
}
