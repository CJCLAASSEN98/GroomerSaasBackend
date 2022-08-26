using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Customer;

namespace backend.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<ServiceResponse<List<GetCustomerDto>>> GetAllCustomers();
        Task<ServiceResponse<GetCustomerDto>> GetCustomerById(int id);
        Task<ServiceResponse<List<GetCustomerDto>>> AddCustomer(AddCustomerDto newCustomer);
        Task<ServiceResponse<GetCustomerDto>> UpdateCustomer(UpdateCustomerDto updatedCustomer);
        Task<ServiceResponse<List<GetCustomerDto>>> DeleteCustomer(int id);
    }
}