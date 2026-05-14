using AutoMapper;
using DTOs.DataTransferObjects;
using DTOs.DropPickRequest;
using DTOs.Stay;
using Entities.Enums;
using Entities.Models;

namespace HMSWebApiProject
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomForCreationDTO, Room>();
            CreateMap<RoomForUpdateDTO, Room>();

            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerForCreationDTO, Customer>();
            CreateMap<CustomerForUpdateDTO, Customer>();

            CreateMap<Customer, CustomerBriefDTO>();
            CreateMap<Stay, StayDTO>()
                .ForCtorParam("RoomPrice",
                    opt => opt.MapFrom(src => src.Room.Price))

                .ForCtorParam("Customer",
                    opt => opt.MapFrom(src =>
                        src.Customer == null
                            ? null
                            : new CustomerBriefDTO(
                                src.Customer.IdentityId,
                                src.Customer.Name,
                                src.Customer.MobileNo
                            )
                    ));



            CreateMap<StayForCreationDTO, Stay>();
            CreateMap<StayForUpdateDTO, Stay>();

            CreateMap<CabDriver, CabDriverDTO>();
            CreateMap<CabDriverForCreationDTO, CabDriver>();
            CreateMap<CabDriverForUpdateDTO, CabDriver>();



            CreateMap<DropPickRequest, DropPickRequestDTO>()
              .ForCtorParam("RequestId", opt => opt.MapFrom(src => src.RequestId))
              .ForCtorParam("StayId", opt => opt.MapFrom(src => src.StayId))
              .ForCtorParam("DriverId", opt => opt.MapFrom(src => src.DriverId))
              .ForCtorParam("RequestType", opt => opt.MapFrom(src => (int)src.RequestType))
              .ForCtorParam("Status", opt => opt.MapFrom(src => (int)src.Status))
              .ForCtorParam("RequestedAt", opt => opt.MapFrom(src => src.RequestedAt))
              .ForCtorParam("Notes", opt => opt.MapFrom(src => src.Notes))
              .ForCtorParam("RoomNo", opt => opt.MapFrom(src => src.Stay.RoomNo))

.ForCtorParam("CustomerName",
    opt => opt.MapFrom(src => src.Stay.Customer != null ? src.Stay.Customer.Name : ""))

              .ForCtorParam("CustomerPhone", opt => opt.MapFrom(src => src.Stay.Customer.MobileNo))

.ForCtorParam("DriverName",
    opt => opt.MapFrom(src => src.CabDriver != null ? src.CabDriver.Name : "Unassigned"))

              .ForCtorParam("CanEdit", opt => opt.MapFrom(src =>
                  src.Status == DropPickStatus.Assigned ||
                  src.Status == DropPickStatus.InProgress
              ));


            CreateMap<DropPickRequestForCreationDTO, DropPickRequest>();
            CreateMap<DropPickRequestForUpdateDTO, DropPickRequest>();

        }

    }
}
