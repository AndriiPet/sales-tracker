using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateCustomerDto model)
        {
            var existingCustomer = await _unitOfWork.CustomerRepository.GetByNameAsync(model.Name);
            if(existingCustomer != null)
            {
                throw new InvalidOperationException("Customer with the same name already exist");
            }

            var customer = _mapper.Map<CreateCustomerDto, Customer>(model);
            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.CustomerRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            if(customers == null)
            {
                throw new System.Exception("Customers weren't found");
            }

            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllWithPaginationAsync(int page, int limit)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            if (customers == null)
            {
                throw new System.Exception("Customers weren't found");
            }
            var paginatedCustomers = customers.Skip((page - 1) * limit).Take(limit);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(paginatedCustomers);
        }

        public async Task<CustomerDto> GetById(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if(customer == null)
            {
                throw new System.Exception("Customer wasn't found");
            }
            return _mapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task UpdateAsync(int id, UpdateCustomerDto model)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if(customer == null)
            {
                throw new System.Exception($"Customer with ID {id} not found");
            }
            var mapperCutomer = _mapper.Map<UpdateCustomerDto, Customer>(model);
            _unitOfWork.CustomerRepository.Update(mapperCutomer);
            await _unitOfWork.SaveAsync();
        }
    }
}
