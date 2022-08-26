using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Customer;
using backend.Services.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCustomerDto>>>> Get()
        {
            return Ok(await _customerService.GetAllCustomers());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCustomerDto>>> GetSingle(int id)
        {
            return Ok(await _customerService.GetCustomerById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCustomerDto>>>> AddCustomer(AddCustomerDto newCustomer)
        {
            return Ok(await _customerService.AddCustomer(newCustomer));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCustomerDto>>> UpdateCustomer(UpdateCustomerDto updatedCustomer)
        {
            var response = await _customerService.UpdateCustomer(updatedCustomer);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCustomerDto>>> Delete(int id)
        {
            var response = await _customerService.DeleteCustomer(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}