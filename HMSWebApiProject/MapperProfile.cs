using AutoMapper;
using DTOs.DataTransferObjects;
using Entities.Models;

namespace HMSWebApiProject
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomForCreationDTO, Room>();
            CreateMap<RoomForUpdateDTO, Room>();
        }
    }
}
