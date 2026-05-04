using DTOs.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HMSWebApiProject.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CustomersController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _service.CustomerService.GetAllCustomers(false);
            return Ok(customers);
        }

        [HttpGet("{identityId}")]
        public IActionResult GetCustomer(string identityId)
        {
            var customer = _service.CustomerService.GetCustomer(identityId, false);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(
            [FromBody] CustomerForCreationDTO customer)
        {
            if (customer == null)
                return BadRequest("Customer object is null");

            var createdCustomer =
                _service.CustomerService.CreateCustomer(customer);

            return CreatedAtAction(
                nameof(GetCustomer),
                new { identityId = createdCustomer.IdentityId },
                createdCustomer);
        }

        [HttpPut("{identityId}")]
        public IActionResult UpdateCustomer(
            string identityId,
            [FromBody] CustomerForUpdateDTO customer)
        {
            _service.CustomerService
                .UpdateCustomer(identityId, customer, true);

            return NoContent();
        }

        [HttpDelete("{identityId}")]
        public IActionResult DeleteCustomer(string identityId)
        {
            _service.CustomerService.DeleteCustomer(identityId, false);
            return NoContent();
        }
    }
}