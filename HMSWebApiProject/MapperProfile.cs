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


            CreateMap<Stay, StayDTO>();
            CreateMap<StayForCreationDTO, Stay>();
            CreateMap<StayForUpdateDTO, Stay>();

        }

    }
}
