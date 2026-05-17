using AutoMapper;
using Contracts;
using DTOs.DataTransferObjects;
using DTOs.Stay;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Services.Contracts;

namespace Services
{
    public class StayService : IStayService
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public StayService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            this.repository = repositoryManager;
            this.logger = loggerManager;
            this.mapper = mapper;
        }
        // ================= READ =================

        public async Task<IEnumerable<StayDTO>> GetAllAsync()
        {
            var stays = await repository.Stay.GetAllAsync(trackChanges: false);
            return mapper.Map<IEnumerable<StayDTO>>(stays);
        }

        public async Task<StayDTO?> GetByIdAsync(int stayId)
        {
            var stay = await repository.Stay.GetByIdAsync(stayId, trackChanges: false);
            return stay == null ? null : mapper.Map<StayDTO>(stay);
        }

        public async Task<IEnumerable<StayDTO>> GetByRoomNoAsync(int roomNo)
        {
            var stays = await repository.Stay.GetByRoomNoAsync(roomNo, trackChanges: false);
            return mapper.Map<IEnumerable<StayDTO>>(stays);
        }

        public async Task<IEnumerable<StayDTO>> GetByCustomerIdentityIdAsync(string customerIdentityId)
        {
            var stays = await repository.Stay
                .GetByCustomerIdentityIdAsync(customerIdentityId, trackChanges: false);

            return mapper.Map<IEnumerable<StayDTO>>(stays);
        }

        public async Task<IEnumerable<StayDTO>> GetByCheckInDateAsync(DateTime date)
        {
            var stays = await repository.Stay
                .GetByCheckInDateAsync(date, trackChanges: false);

            return mapper.Map<IEnumerable<StayDTO>>(stays);
        }

        // ================= CREATE =================
   

        public async Task<StayDTO> CreateAsync(StayForCreationDTO dto)
        {
            var existingStays = await repository.Stay.GetByRoomNoAsync(dto.RoomNo, false);
            if (existingStays.Any(s => s.CheckOutAt == null))
                throw new StayAlreadyExistsException(dto.RoomNo);

            var room = await repository.Room.GetRoomByRoomNoAsync(dto.RoomNo, false)
                ?? throw new RoomNotFoundException(dto.RoomNo);

            var stay = mapper.Map<Stay>(dto);

            stay.RoomNo = dto.RoomNo;
           
            stay.CheckInAt = DateTime.Now;
            stay.CheckOutAt = null;

            stay.DepositPaid = dto.DepositPaid;
            stay.AmountPaid = 0;

            stay.PendingAmount = Math.Max(0, room.Price - stay.DepositPaid);

            repository.Stay.CreateStay(stay);
            repository.Save();

            var createdStay = await repository.Stay
              .GetByIdAsync(stay.StayId, false);
            return mapper.Map<StayDTO>(createdStay);
        }

        // ================= UPDATE (CHECK-OUT) =================


        public async Task<StayDTO> UpdateAndReturnAsync(int stayId, StayForUpdateDTO dto)
        {
            await UpdateAsync(stayId, dto);
            var stay = await repository.Stay.GetByIdAsync(stayId, false);
            return mapper.Map<StayDTO>(stay);
        }


        public async Task UpdateAsync(int stayId, StayForUpdateDTO dto)
        {
            if (stayId <= 0)
                throw new InvalidStayOperationException("Invalid stay id.");

            var stay = await repository.Stay.GetByIdAsync(stayId, trackChanges: true);
            if (stay == null)
                throw new StayNotFoundException(stayId);

         
            if (stay.CheckOutAt != null)
                throw new InvalidStayOperationException(
                    $"Stay '{stayId}' is already checked out.");

        
            if (dto.RoomNo.HasValue && dto.RoomNo.Value != stay.RoomNo)
            {
                stay.RoomNo = dto.RoomNo.Value;
            }

           
            if (dto.AmountPaid.HasValue)
            {
                if (dto.AmountPaid.Value < 0)
                    throw new InvalidStayOperationException("Amount paid cannot be negative.");

                stay.AmountPaid += dto.AmountPaid.Value;

            }

            if (dto.CheckOutAt.HasValue)
            {
                if (dto.CheckOutAt.Value < stay.CheckInAt)
                    throw new InvalidStayOperationException(
                        "Checkout time cannot be before check-in time.");

                stay.CheckOutAt = dto.CheckOutAt.Value;
                stay.PendingAmount = RecalculatePendingAmount(stay);

            }

            repository.Save();
        }



        // ================= DELETE =================

        public async Task DeleteAsync(int stayId)
        {
            var stay = await repository.Stay.GetByIdAsync(stayId, trackChanges: true);
            if (stay == null)
                throw new StayNotFoundException(stayId);

            repository.Stay.DeleteStay(stay);
            repository.Save();
        }


        public async Task<IEnumerable<StayDTO>> GetActiveAsync()
        {
            var stays = await repository.Stay.GetAllAsync(false);
            return mapper.Map<IEnumerable<StayDTO>>(
                stays.Where(s => s.CheckOutAt == null));
        }

        public async Task<IEnumerable<StayDTO>> GetPastAsync()
        {
            var stays = await repository.Stay.GetAllAsync(false);
            return mapper.Map<IEnumerable<StayDTO>>(
                stays.Where(s => s.CheckOutAt != null));
        }

        public async Task<StayDTO> CheckOutAsync(int stayId, CheckOutRequestDTO dto)
        {
            var stay = await repository.Stay.GetByIdAsync(stayId, true);
            if (stay == null)
                throw new StayNotFoundException(stayId);

            if (stay.CheckOutAt != null)
                throw new InvalidStayOperationException(
                    $"Stay '{stayId}' is already checked out.");

            var checkOutAt = dto.CheckOutAt ?? DateTime.Now;

            var room = stay.Room
                ?? throw new RoomNotFoundException(stay.RoomNo);

            var nights = (int)Math.Ceiling(
                (checkOutAt - stay.CheckInAt).TotalDays);
            nights = Math.Max(1, nights);

            var totalCharge = nights * room.Price;
            stay.AmountPaid += dto.AmountPaid;

            stay.PendingAmount = Math.Max(0,
                totalCharge - (stay.DepositPaid + stay.AmountPaid));

            stay.CheckOutAt = checkOutAt;

            room.AvailabilityStatus = AvailabilityStatus.Available;
            room.CleanStatus = CleanStatus.Dirty;

            repository.Save();

            return mapper.Map<StayDTO>(stay);
        }

        public async Task<BillingSummaryDTO> GetBillingSummaryAsync(int stayId)
        {
            var stay = await repository.Stay.GetByIdAsync(stayId, false)
                ?? throw new StayNotFoundException(stayId);

            var room = stay.Room
                ?? throw new RoomNotFoundException(stay.RoomNo);

            var effectiveCheckOut = stay.CheckOutAt ?? DateTime.Now;

            var nights = (int)Math.Ceiling(
                (effectiveCheckOut - stay.CheckInAt).TotalDays);
            nights = Math.Max(1, nights);

            var totalCharge = nights * room.Price;
            var totalPaid = stay.DepositPaid + stay.AmountPaid;

            return new BillingSummaryDTO(
                nights,
                room.Price,
                totalCharge,
                stay.DepositPaid,
                stay.AmountPaid,
                totalPaid,
                Math.Max(0, totalCharge - totalPaid)
            );
        }

  
        private decimal RecalculatePendingAmount(Stay stay)
        {
            if (stay.CheckOutAt == null)
                throw new InvalidStayOperationException(
                    "Pending amount can only be calculated after checkout.");

            var room = stay.Room
                ?? throw new RoomNotFoundException(stay.RoomNo);

            var nights = (int)Math.Ceiling(
                (stay.CheckOutAt.Value - stay.CheckInAt).TotalDays);

            nights = Math.Max(1, nights);

            var totalCharge = nights * room.Price;
            var totalPaid = stay.DepositPaid + stay.AmountPaid;

            return Math.Max(0, totalCharge - totalPaid);
        }


    }



}

