using DTOs.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HMSWebApiProject.Controllers.Controllers
{
    [ApiController]
    [Route("api/cabdrivers")]
    public class CabDriversController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CabDriversController(IServiceManager service)
        {
            _service = service;
        }

        // ============================
        // GET
        // ============================

        // GET: api/cabdrivers
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _service.CabDriverService
                .GetAllDrivers(trackChanges: false);

            return Ok(drivers);
        }

        // GET: api/cabdrivers/5
        [HttpGet("{driverId:int}", Name = "CabDriverById")]
        public async Task<IActionResult> GetDriverById(int driverId)
        {
            var driver = await _service.CabDriverService
                .GetDriverById(driverId, trackChanges: false);

            return Ok(driver);
        }

        // GET: api/cabdrivers/by-govt-id/XXXX
        [HttpGet("by-govt-id/{govtId}", Name = "CabDriverByGovtId")]
        public async Task<IActionResult> GetDriverByGovtId(string govtId)
        {
            var driver = await _service.CabDriverService
                .GetDriverByGovtId(govtId, trackChanges: false);

            return Ok(driver);
        }

        // ============================
        // POST
        // ============================

        // POST: api/cabdrivers
        [HttpPost]
        public async Task<IActionResult> CreateDriver(
            [FromBody] CabDriverForCreationDTO driverForCreation)
        {
            if (driverForCreation is null)
                return BadRequest("Cab driver object is null.");

            var createdDriver = await _service.CabDriverService
                .CreateDriver(driverForCreation);

            return CreatedAtRoute(
                "CabDriverByGovtId",
                new { govtId = createdDriver.GovernmentId },
                createdDriver
            );
        }

        // ============================
        // PUT
        // ============================

        // PUT: api/cabdrivers/5
        [HttpPut("{driverId:int}")]
        public async Task<IActionResult> UpdateDriverById(
            int driverId,
            [FromBody] CabDriverForUpdateDTO driverForUpdate)
        {
            if (driverForUpdate is null)
                return BadRequest("Cab driver update object is null.");

            await _service.CabDriverService.UpdateDriver(
                driverId,
                driverForUpdate,
                trackChanges: true
            );

            return NoContent();
        }

        // PUT: api/cabdrivers/by-govt-id/XXXX
        //[HttpPut("by-govt-id/{govtId}")]
        //public async Task<IActionResult> UpdateDriverByGovtId(
        //    string govtId,
        //    [FromBody] CabDriverForUpdateDTO driverForUpdate)
        //{
        //    if (driverForUpdate is null)
        //        return BadRequest("Cab driver update object is null.");

        //    await _service.CabDriverService.UpdateDriver(
        //        govtId,
        //        driverForUpdate,
        //        trackChanges: true
        //    );

        //    return NoContent();
        //}

        // ============================
        // DELETE (soft delete)
        // ============================

        // DELETE: api/cabdrivers/5
        [HttpDelete("{driverId:int}")]
        public async Task<IActionResult> DeleteDriverById(int driverId)
        {
            await _service.CabDriverService
                .DeleteDriver(driverId, trackChanges: false);

            return NoContent();
        }

        // DELETE: api/cabdrivers/by-govt-id/XXXX
        //[HttpDelete("by-govt-id/{govtId}")]
        //public async Task<IActionResult> DeleteDriverByGovtId(string govtId)
        //{
        //    await _service.CabDriverService
        //        .DeleteDriver(govtId, trackChanges: false);

        //    return NoContent();
        //}
    }
}