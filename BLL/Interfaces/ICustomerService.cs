using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICustomerService : IService<CustomerDto, CreateCustomerDto, UpdateCustomerDto>
    {
        Task<IEnumerable<CustomerDto>> GetAllWithPaginationAsync(int page, int limit);
    }
}
