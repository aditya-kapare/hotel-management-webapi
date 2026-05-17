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
    .ForCtorParam(nameof(StayDTO.StayId),
        opt => opt.MapFrom(s => s.StayId))

    .ForCtorParam(nameof(StayDTO.RoomNo),
        opt => opt.MapFrom(s => s.RoomNo))

    .ForCtorParam(nameof(StayDTO.CustomerIdentityId),
        opt => opt.MapFrom(s => s.CustomerIdentityId))

    .ForCtorParam(nameof(StayDTO.CheckInAt),
        opt => opt.MapFrom(s => s.CheckInAt))

    .ForCtorParam(nameof(StayDTO.CheckOutAt),
        opt => opt.MapFrom(s => s.CheckOutAt))

    .ForCtorParam(nameof(StayDTO.RoomPrice),
        opt => opt.MapFrom(s => s.Room.Price))

    .ForCtorParam(nameof(StayDTO.DepositPaid),
        opt => opt.MapFrom(s => s.DepositPaid))

    .ForCtorParam(nameof(StayDTO.AmountPaid),
        opt => opt.MapFrom(s => s.AmountPaid))

    .ForCtorParam(nameof(StayDTO.PendingAmount),
        opt => opt.MapFrom(s => s.PendingAmount))

    .ForCtorParam(nameof(StayDTO.Customer),
        opt => opt.MapFrom(s =>
            s.Customer == null
                ? null
                : new CustomerBriefDTO(
                    s.Customer.IdentityId,
                    s.Customer.Name,
                    s.Customer.MobileNo
                )));


            CreateMap<StayForCreationDTO, Stay>();
          //  CreateMap<StayForUpdateDTO, Stay>();

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
