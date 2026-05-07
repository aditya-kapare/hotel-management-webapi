using AutoMapper;
using Contracts;
using DTOs.Stay;
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



            // -------- READ --------

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

            // -------- WRITE --------

            public async Task<StayDTO> CreateAsync(StayForCreationDTO dto)
            {
                var stay = mapper.Map<Stay>(dto);

                stay.CheckInAt = DateTime.Now;
                stay.PendingAmount = stay.DepositPaid;

                repository.Stay.CreateStay(stay);
                repository.Save();

                return mapper.Map<StayDTO>(stay);
            }

            public async Task UpdateAsync(StayForUpdateDTO dto)
            {
                var stay = await repository.Stay.GetByIdAsync(dto.StayId, trackChanges: true);
                if (stay == null)
                    throw new Exception("Stay not found");

                stay.AmountPaid = dto.AmountPaid;
                stay.CheckOutAt = dto.CheckOutAt ?? DateTime.Now;
                stay.PendingAmount = 0;

                repository.Stay.UpdateStay(stay);
                repository.Save();
            }

            public async Task DeleteAsync(int stayId)
            {
                var stay = await repository.Stay.GetByIdAsync(stayId, trackChanges: true);
                if (stay == null)
                    throw new Exception("Stay not found");

                repository.Stay.DeleteStay(stay);
                repository.Save();
            }
        }
    }


