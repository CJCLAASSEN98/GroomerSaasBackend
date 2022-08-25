using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos.Customer;

namespace backend.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private static List<Customer> Customers = new List<Customer>{
            new Customer(),
            new Customer {Id = 1,Name = "Sam"}
        };
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCustomerDto>>> AddCustomer(AddCustomerDto newCustomer)
        {
            var serviceResponse = new ServiceResponse<List<GetCustomerDto>>();
            Customer customer = _mapper.Map<Customer>(newCustomer);
            customer.Id = Customers.Max(c => c.Id) + 1;
            Customers.Add(customer);
            serviceResponse.Data = Customers.Select(c => _mapper.Map<GetCustomerDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCustomerDto>>> GetAllCustomers()
        {
            return new ServiceResponse<List<GetCustomerDto>> { Data = Customers.Select(c => _mapper.Map<GetCustomerDto>(c)).ToList() };
        }

        public async Task<ServiceResponse<GetCustomerDto>> GetCustomerById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCustomerDto>();
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCustomerDto>(customer);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCustomerDto>> UpdateCustomer(UpdateCustomerDto updatedCustomer)
        {
            ServiceResponse<GetCustomerDto> response = new ServiceResponse<GetCustomerDto>();

            try
            {

                Customer customer = Customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);

                customer.Name = updatedCustomer.Name;
                customer.Surname = updatedCustomer.Surname;
                customer.ContactNumber = updatedCustomer.ContactNumber;

                response.Data = _mapper.Map<GetCustomerDto>(customer);
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