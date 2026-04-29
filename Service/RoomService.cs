using AutoMapper;
using Contracts;
using DTOs.DataTransferObjects;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Services.Contracts;

namespace Services
{
    public sealed class RoomService : IRoomService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RoomService(
            IRepositoryManager repository,
            ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDTO>> GetAllRooms(bool trackChanges)
        {
            var rooms = await _repository.Room.GetAllRoomsAsync(trackChanges);
            var roomDtos = _mapper.Map<IEnumerable<RoomDTO>>(rooms);
            return roomDtos;
        }

        public async Task<RoomDTO> GetRoomByRoomNo(int roomNo, bool trackChanges)
        {
            var room = await _repository.Room
                .GetRoomByRoomNoAsync(roomNo, trackChanges);

            if (room is null)
                throw new RoomNotFoundException(roomNo);

            var roomDto = _mapper.Map<RoomDTO>(room);
            return roomDto;
        }

        public async Task<IEnumerable<RoomDTO>> GetRoomsByType(RoomType roomType, bool trackChanges)
        {
            var rooms = await _repository.Room.GetAllRoomsAsync(trackChanges);

            var filteredRooms = rooms
                .Where(r => r.RoomType == roomType);

            var roomDtos = _mapper.Map<IEnumerable<RoomDTO>>(filteredRooms);
            return roomDtos;
        }

        public async Task<RoomDTO> CreateRoom(RoomForCreationDTO room)
        {
            var roomEntity = _mapper.Map<Room>(room);

            _repository.Room.CreateRoom(roomEntity);
            _repository.Save();

            var roomDto = _mapper.Map<RoomDTO>(roomEntity);
            return roomDto;
        }

        public async Task UpdateRoom(int roomNo, RoomForUpdateDTO roomForUpdate, bool trackChanges)
        {
            var roomEntity = await _repository.Room
                .GetRoomByRoomNoAsync(roomNo, trackChanges);

            if (roomEntity is null)
                throw new RoomNotFoundException(roomNo);

            _mapper.Map(roomForUpdate, roomEntity);

            //_repository.Room.UpdateRoom(roomEntity);
            _repository.Save();
        }

        public async Task DeleteRoom(int roomNo, bool trackChanges)
        {
            var room = await _repository.Room
                .GetRoomByRoomNoAsync(roomNo, trackChanges);

            if (room is null)
                throw new RoomNotFoundException(roomNo);

            _repository.Room.DeleteRoom(room);
            _repository.Save();
        }
    }
}