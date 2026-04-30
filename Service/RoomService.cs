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
            var activeRooms = rooms.Where(r => r.DoesExist);
            return _mapper.Map<IEnumerable<RoomDTO>>(activeRooms);
        }

        public async Task<RoomDTO> GetRoomByRoomNo(int roomNo, bool trackChanges)
        {
            var room = await _repository.Room
                .GetRoomByRoomNoAsync(roomNo, trackChanges);

            if (room is null || !room.DoesExist)
                throw new RoomNotFoundException(roomNo);

            var roomDto = _mapper.Map<RoomDTO>(room);
            return roomDto;
        }

        public async Task<IEnumerable<RoomDTO>> GetRoomsByType(RoomType roomType, bool trackChanges)
        {
            var rooms = await _repository.Room.GetAllRoomsAsync(trackChanges);

            var filteredRooms = rooms
                .Where(r => r.DoesExist && r.RoomType == roomType);

            var roomDtos = _mapper.Map<IEnumerable<RoomDTO>>(filteredRooms);
            return roomDtos;
        }

        public async Task<RoomDTO> CreateRoom(RoomForCreationDTO room)
        {

            var existingRoom = await _repository.Room
                    .GetRoomByRoomNoAsync(room.RoomNo, trackChanges: true);

            if (existingRoom is not null)
            {
                if (existingRoom.DoesExist)
                    throw new RoomAlreadyExistsException(room.RoomNo);

                // restore soft-deleted room
                _mapper.Map(room, existingRoom);
                existingRoom.DoesExist = true;
                existingRoom.AvailabilityStatus = AvailabilityStatus.Available;
                _repository.Save();
                return _mapper.Map<RoomDTO>(existingRoom);
            }
            var roomEntity = _mapper.Map<Room>(room);
            _repository.Room.CreateRoom(roomEntity);
            _repository.Save();

            return _mapper.Map<RoomDTO>(roomEntity);
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

            var room = await _repository.Room.GetRoomByRoomNoAsync(roomNo, trackChanges);

            if (room is null || !room.DoesExist)
                throw new RoomNotFoundException(roomNo);

            room.DoesExist = false;
            room.AvailabilityStatus = AvailabilityStatus.Inavailable;
            _repository.Room.UpdateRoom(room);
            _repository.Save();

        }
    }
}