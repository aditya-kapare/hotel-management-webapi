using AutoMapper;
using Contracts;
using DTOs.Stay;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Prevent double booking
            var existingStays = await repository.Stay.GetByRoomNoAsync(dto.RoomNo, false);
            if (existingStays.Any(s => s.CheckOutAt == null))
                throw new StayAlreadyExistsException(dto.RoomNo);

            var room = await repository.Room.GetRoomByRoomNoAsync(dto.RoomNo, false);
            if (room == null)
                throw new RoomNotFoundException(dto.RoomNo);

            var stay = mapper.Map<Stay>(dto);

            stay.CheckInAt = DateTime.Now;

            // ✅ IMPORTANT: checkout NOT allowed during creation
            stay.CheckOutAt = null;

            stay.AmountPaid = dto.DepositPaid;

            stay.PendingAmount = room.Price - stay.AmountPaid;
            if (stay.PendingAmount < 0)
                stay.PendingAmount = 0;

            repository.Stay.CreateStay(stay);
            repository.Save();

            return mapper.Map<StayDTO>(stay);
        }


        // ================= UPDATE (CHECK-OUT) =================

        public async Task UpdateAsync(int stayId, StayForUpdateDTO dto)
        {
            var stay = await repository.Stay.GetByIdAsync(dto.StayId, true);
            if (stay == null)
                throw new StayNotFoundException(dto.StayId);

            // ✅ Validate checkout time
            if (dto.CheckOutAt.HasValue && dto.CheckOutAt.Value < stay.CheckInAt)
                throw new InvalidStayOperationException(
                    "Checkout time cannot be before check-in time.");

            // ✅ Update checkout time ONLY if provided
            if (dto.CheckOutAt.HasValue)
                stay.CheckOutAt = dto.CheckOutAt.Value;

            // ✅ Update payment ONLY if user explicitly paid something
            if (dto.AmountPaid > 0)
            {
                stay.AmountPaid += dto.AmountPaid;

                var room = stay.Room
                    ?? throw new RoomNotFoundException(stay.RoomNo);

                stay.PendingAmount = room.Price - stay.AmountPaid;

                if (stay.PendingAmount < 0)
                    stay.PendingAmount = 0;
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
        }
    }

   


