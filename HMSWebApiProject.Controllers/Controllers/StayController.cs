using DTOs.DataTransferObjects;
using DTOs.Stay;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HMSWebApiProject.Controllers
{
    [ApiController]
    [Route("api/stays")]
    public class StayController : ControllerBase
    {
        private readonly IStayService _stayService;

        public StayController(IStayService stayService)
        {
            _stayService = stayService;
        }

        // View List of Stays
        // GET /api/stays
        [HttpGet]
        public async Task<IActionResult> GetAllStays()
        {
            var stays = await _stayService.GetAllAsync();
            return Ok(stays);
        }

        // View Stay Details by Stay ID
        // GET /api/stays/{stayId:int}
        [HttpGet("{stayId:int}")]
        public async Task<IActionResult> GetStayById(int stayId)
        {
            var stay = await _stayService.GetByIdAsync(stayId);
            if (stay == null)
                return NotFound();

            return Ok(stay);
        }

        // View Room Associated with a Stay
        // GET /api/stays/{stayId:int}/rooms/{roomNo:int}
        [HttpGet("{stayId:int}/rooms/{roomNo:int}")]
        public async Task<IActionResult> GetRoomForStay(int stayId, int roomNo)
        {
            var stay = await _stayService.GetByIdAsync(stayId);
            if (stay == null || stay.RoomNo != roomNo)
                return NotFound();

            return Ok(new
            {
                stay.StayId,
                stay.RoomNo
            });
        }

        // View Customer Associated with a Stay
        // GET /api/stays/{stayId:int}/customers/{customerId}
        [HttpGet("{stayId:int}/customers/{customerId}")]
        public async Task<IActionResult> GetCustomerForStay(int stayId, string customerId)
        {
            var stay = await _stayService.GetByIdAsync(stayId);
            if (stay == null || stay.CustomerIdentityId != customerId)
                return NotFound();

            return Ok(new
            {
                stay.StayId,
                stay.Customer,


            });
        }

        // View Stays by Date

        // GET /api/stays/by-date/2024-05-06
        [HttpGet("by-date/{date}")]
        public async Task<IActionResult> GetStaysByDate(DateTime date)
        {
            var stays = await _stayService.GetByCheckInDateAsync(date);
            return Ok(stays);
        }


        // Create Stay (Check-in)
        // POST /api/stays
        [HttpPost]
        public async Task<IActionResult> CreateStay([FromBody] StayForCreationDTO dto)
        {
            var createdStay = await _stayService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetStayById),
                new { stayId = createdStay.StayId },
                createdStay
            );
        }

        // Update Stay Details (Check-out)
        // PUT /api/stays

        [HttpPut("{stayId:int}")]
        public async Task<IActionResult> UpdateStay(
            int stayId,
            [FromBody] StayForUpdateDTO dto)
        {
            var updatedStay = await _stayService.UpdateAndReturnAsync(stayId, dto);
            return Ok(updatedStay);
        }






        //Delete Stay by Stay ID
        // DELETE /api/stays/{stayId:int}
        [HttpDelete("{stayId:int}")]
        public async Task<IActionResult> DeleteStay(int stayId)
        {
            await _stayService.DeleteAsync(stayId);
            return NoContent();
        }

        // View Active Stays
        // GET /api/stays/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            return Ok(await _stayService.GetActiveAsync());
        }

        //View Past Stays
        // GET /api/stays/past
        [HttpGet("past")]
        public async Task<IActionResult> GetPast()
        {
            return Ok(await _stayService.GetPastAsync());
        }


        //Check-out a Stay
        // POST /api/stays/{stayId:int}/checkout
        [HttpPost("{stayId:int}/checkout")]
        public async Task<IActionResult> CheckOut(
            int stayId,
            [FromBody] CheckOutRequestDTO dto)
        {
            return Ok(await _stayService.CheckOutAsync(stayId, dto));
        }

        //Get Billing Summary for a Stay
        // GET /api/stays/{stayId:int}/billing
        [HttpGet("{stayId:int}/billing")]
        public async Task<IActionResult> GetBilling(int stayId)
        {
            return Ok(await _stayService.GetBillingSummaryAsync(stayId));
        }




    }
}