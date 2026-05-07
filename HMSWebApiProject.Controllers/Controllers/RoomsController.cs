using DTOs.DataTransferObjects;
using Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HMSWebApiProject.Controllers.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RoomsController(IServiceManager service)
        {
            _service = service;
        }

        // GET: api/rooms
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _service.RoomService.GetAllRooms(trackChanges: false);
            return Ok(rooms);
        }

        // GET: api/rooms/101
        [HttpGet("{roomNo:int}", Name = "RoomByRoomNo")]
        public async Task<IActionResult> GetRoomByRoomNo(int roomNo)
        {
            var room = await _service.RoomService.GetRoomByRoomNo(roomNo, trackChanges: false);
            return Ok(room);
        }

        // GET: api/rooms/by-type/Deluxe
        [HttpGet("by-type/{roomType}")]
        public async Task<IActionResult> GetRoomsByType(RoomType roomType)
        {
            var rooms = await _service.RoomService.GetRoomsByType(roomType, trackChanges: false);
            return Ok(rooms);
        }

        // POST: api/rooms
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomForCreationDTO roomForCreation)
        {
            if (roomForCreation is null)
                return BadRequest("Room object is null.");

            var createdRoom = await _service.RoomService.CreateRoom(roomForCreation);

            return CreatedAtRoute(
                "RoomByRoomNo",
                new { roomNo = createdRoom.RoomNo },
                createdRoom
            );
        }

        // PUT: api/rooms/101
        [HttpPut("{roomNo:int}")]
        public async Task<IActionResult> UpdateRoom(
            int roomNo,
            [FromBody] RoomForUpdateDTO roomForUpdate)
        {
            if (roomForUpdate is null)
                return BadRequest("Room update object is null.");

            await _service.RoomService.UpdateRoom(
                roomNo,
                roomForUpdate,
                trackChanges: true
            );

            return NoContent();
        }

        // DELETE: api/rooms/101
        [HttpDelete("{roomNo:int}")]
        public async Task<IActionResult> DeleteRoom(int roomNo)
        {
            await _service.RoomService.DeleteRoom(roomNo, trackChanges: false);
            return NoContent();
        }
    }
}
