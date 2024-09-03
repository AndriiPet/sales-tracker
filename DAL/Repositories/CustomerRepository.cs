using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TorgDBContext _context;

        public CustomerRepository(TorgDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer entity)
        {
            await _context.Customer.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customer.ToListAsync();
        }


        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        public async Task<Customer> GetByNameAsync(string name)
        {
            return await _context.Customer.FirstOrDefaultAsync(c => c.Name == name);
        }

        public void Update(Customer entity)
        {
            _context.Customer.Update(entity);
        }
    }
}
