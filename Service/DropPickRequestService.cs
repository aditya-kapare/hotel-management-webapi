using AutoMapper;
using Contracts;
using DTOs.DataTransferObjects;
using DTOs.DropPickRequest;
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
    public class DropPickRequestService : IDropPickRequestService
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public DropPickRequestService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            this.repository = repositoryManager;
            this.logger = loggerManager;
            this.mapper = mapper;
        }




        // ---------------- READ OPERATIONS ----------------

        public async Task<IEnumerable<DropPickRequestDTO>> GetAllAsync()
        {
            var requests = await repository.DropPickRequest.GetAllAsync(trackChanges: false);
            return mapper.Map<IEnumerable<DropPickRequestDTO>>(requests);
        }

        public async Task<DropPickRequestDTO?> GetByIdAsync(int requestId)
        {
            var request = await repository.DropPickRequest.GetByIdAsync(requestId, trackChanges: false);
            return request == null ? null : mapper.Map<DropPickRequestDTO>(request);
        }

        public async Task<IEnumerable<DropPickRequestDTO>> GetByStayIdAsync(int stayId)
        {
            var requests = await repository.DropPickRequest.GetByStayIdAsync(stayId, trackChanges: false);
            return mapper.Map<IEnumerable<DropPickRequestDTO>>(requests);
        }

        public async Task<IEnumerable<DropPickRequestDTO>> GetByDriverIdAsync(int driverId)
        {
            var requests = await repository.DropPickRequest.GetByDriverIdAsync(driverId, trackChanges: false);
            return mapper.Map<IEnumerable<DropPickRequestDTO>>(requests);
        }


        public async Task<IReadOnlyList<CabDriverBriefDTO>> GetAvailableDriversAsync()
        {
            var drivers =
                await repository.DropPickRequest.GetAvailableDriversAsync();

            return drivers
                .Where(d => d.IsActive) // optional safety check
                .Select(d => new CabDriverBriefDTO(d.DriverId, d.Name))
                .ToList();
        }


        // ---------------- CREATE ----------------

        public async Task<DropPickRequestDTO> CreateAsync(DropPickRequestForCreationDTO dto)
        {
            // ✅ Map DTO → Entity
            var request = mapper.Map<DropPickRequest>(dto);

            // ✅ Business Rule: set requested time
            request.RequestedAt = DateTime.Now;

            // ✅ Business Rule: default status
            request.Status = DropPickStatus.Assigned;

            // ✅ Business Rule: check if driver is busy


            var availableDrivers =
                await repository.DropPickRequest.GetAvailableDriversAsync();

            if (!availableDrivers.Any(d => d.DriverId == request.DriverId))
                throw new CabDriverBusyException(request.DriverId);


            repository.DropPickRequest.Create(request);
            repository.Save();

            return mapper.Map<DropPickRequestDTO>(request);
        }

        // ---------------- UPDATE ----------------
        public async Task UpdateAsync(int requestId, DropPickRequestForUpdateDTO dto)
        {
            var request = await repository.DropPickRequest
                .GetByIdAsync(requestId, trackChanges: true);

            if (request == null)
                throw new DropPickRequestNotFoundException(requestId);

            // ❌ Completed / Cancelled requests are immutable
            if (request.Status == DropPickStatus.Completed ||
                request.Status == DropPickStatus.Cancelled)
                throw new InvalidDropPickRequestOperationException(
                    "Completed or cancelled requests cannot be modified.");

            // ✅ RequestedAt (allowed in Web App)
            if (dto.RequestedAt.HasValue)
                request.RequestedAt = dto.RequestedAt.Value;

            // ✅ Notes
            if (!string.IsNullOrWhiteSpace(dto.Notes))
                request.Notes = dto.Notes;

            // ✅ RequestType (only before InProgress)
            if (dto.RequestType.HasValue &&
                request.Status == DropPickStatus.Assigned)
            {
                request.RequestType = (RequestType)dto.RequestType.Value;
            }

            // ✅ Driver reassignment (only before InProgress)
            if (dto.DriverId.HasValue &&
                request.Status == DropPickStatus.Assigned)
            {
                var availableDrivers =
                    await repository.DropPickRequest.GetAvailableDriversAsync();

                if (!availableDrivers.Any(d => d.DriverId == dto.DriverId.Value))
                    throw new CabDriverBusyException(dto.DriverId.Value);

                request.DriverId = dto.DriverId.Value;
            }

            // ✅ Status transition
            if (dto.Status.HasValue &&
                dto.Status.Value != (int)request.Status)
            {
                if (!IsValidStatusTransition(request.Status, (DropPickStatus)dto.Status.Value))
                    throw new InvalidDropPickStatusTransitionException(
                        request.Status, (DropPickStatus)dto.Status.Value);
                request.Status = (DropPickStatus)dto.Status.Value;
            }

            repository.DropPickRequest.Update(request);
            repository.Save();
        }

        // ---------------- DELETE ----------------

        public async Task DeleteAsync(int requestId)
        {
            var request = await repository.DropPickRequest.GetByIdAsync(requestId, trackChanges: true);
            if (request == null)
                throw new DropPickRequestNotFoundException(requestId);


            request.Status = DropPickStatus.Cancelled;
            repository.DropPickRequest.Update(request);
            repository.Save();

        }

        // ---------------- PRIVATE HELPERS ----------------

        private static bool IsValidStatusTransition(
            DropPickStatus current,
            DropPickStatus next)
        {
            return current switch
            {
                DropPickStatus.Assigned =>
                    next == DropPickStatus.InProgress ||
                    next == DropPickStatus.Cancelled,

                DropPickStatus.InProgress =>
                    next == DropPickStatus.Completed ||
                    next == DropPickStatus.Cancelled,

                DropPickStatus.Completed => false,
                DropPickStatus.Cancelled => false,

                _ => false
            };
        }
    }
}
