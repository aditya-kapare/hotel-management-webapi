using DTOs.DropPickRequest;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HMSWebApiProject.Controllers
{
    [ApiController]
    [Route("api/droppickrequests")]
    public class DropPickRequestsController : ControllerBase
    {
        private readonly IDropPickRequestService _service;

        public DropPickRequestsController(IDropPickRequestService service)
        {
            _service = service;
        }

        //View all drop-pick requests
        // GET /api/droppickrequests
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _service.GetAllAsync();
            return Ok(requests);
        }

        //View drop-pick request by request ID
        // GET /api/droppickrequests/{requestId:int}
        [HttpGet("{requestId:int}")]
        public async Task<IActionResult> GetById(int requestId)
        {
            var request = await _service.GetByIdAsync(requestId);
            if (request == null)
                return NotFound();

            return Ok(request);
        }

        //View drop-pick requests by stay ID
        // GET /api/droppickrequests/stay/{stayId:int}
        [HttpGet("stay/{stayId:int}")]
        public async Task<IActionResult> GetByStay(int stayId)
        {
            var requests = await _service.GetByStayIdAsync(stayId);
            return Ok(requests);
        }

        //View drop-pick requests by driver ID
        // GET /api/droppickrequests/driver/{driverId:int}
        [HttpGet("driver/{driverId:int}")]
        public async Task<IActionResult> GetByDriver(int driverId)
        {
            var requests = await _service.GetByDriverIdAsync(driverId);
            return Ok(requests);
        }

        //View available cab drivers for drop-pick
        // GET /api/droppickrequests/drivers/available

        [HttpGet("drivers/available")]
        public async Task<IActionResult> GetAvailableDrivers()
        {
            var drivers = await _service.GetAvailableDriversAsync();
            return Ok(drivers);
        }


        //Create a new drop-pick request
        // POST /api/droppickrequests
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] DropPickRequestForCreationDTO dto)
        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { requestId = created.RequestId },
                created);
        }

        //Update drop-pick request (status change)
        // PUT /api/droppickrequests/{requestId:int}
        [HttpPut("{requestId:int}")]
        public async Task<IActionResult> Update(
            int requestId,
            [FromBody] DropPickRequestForUpdateDTO dto)
        {
            
            await _service.UpdateAsync(requestId,dto);
            return NoContent();
        }

        //Delete drop-pick request by request ID
        // DELETE /api/droppickrequests/{requestId:int}
        [HttpDelete("{requestId:int}")]
        public async Task<IActionResult> Delete(int requestId)
        {
            await _service.DeleteAsync(requestId);
            return NoContent();
        }
    }
}