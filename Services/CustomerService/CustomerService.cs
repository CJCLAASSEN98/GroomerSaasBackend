using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos.Customer;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCustomerDto>>> AddCustomer(AddCustomerDto newCustomer)
        {
            var serviceResponse = new ServiceResponse<List<GetCustomerDto>>();
            Customer customer = _mapper.Map<Customer>(newCustomer);
            customer.Groomer = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Customers
                .Where(c => c.Groomer.Id == GetUserId())
                .Select(c => _mapper.Map<GetCustomerDto>(c))
                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCustomerDto>>> DeleteCustomer(int id)
        {
            ServiceResponse<List<GetCustomerDto>> response = new ServiceResponse<List<GetCustomerDto>>();

            try
            {

                Customer customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == id && c.Groomer.Id == GetUserId());
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                    response.Data = _context.Customers.Where(c => c.Groomer.Id == GetUserId()).Select(c => _mapper.Map<GetCustomerDto>(c)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Customer not found";
                }





            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCustomerDto>>> GetAllCustomers()
        {
            var response = new ServiceResponse<List<GetCustomerDto>>();
            var dbCustomers = await _context.Customers.Where(c => c.Groomer.Id == GetUserId()).ToListAsync();
            response.Data = dbCustomers.Select(c => _mapper.Map<GetCustomerDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCustomerDto>> GetCustomerById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCustomerDto>();
            var dbCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id && c.Groomer.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCustomerDto>(dbCustomer);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCustomerDto>> UpdateCustomer(UpdateCustomerDto updatedCustomer)
        {
            ServiceResponse<GetCustomerDto> response = new ServiceResponse<GetCustomerDto>();

            try
            {

                var customer = await _context.Customers
                    .Include(c => c.Groomer)
                    .FirstOrDefaultAsync(c => c.Id == updatedCustomer.Id);

                if (customer.Groomer.Id == GetUserId())
                {
                    customer.Name = updatedCustomer.Name;
                    customer.Surname = updatedCustomer.Surname;
                    customer.Email = updatedCustomer.Email;
                    customer.ContactNumber = updatedCustomer.ContactNumber;
                    customer.Day = updatedCustomer.Day;
                    customer.Frequency = updatedCustomer.Frequency;

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetCustomerDto>(customer);

                    //Reminder to self make sure to populate all fields on frontend with exsiting data so that it gets passed with updated values
                }
                else
                {
                    response.Success = false;
                    response.Message = "Customer not found";
                }


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}