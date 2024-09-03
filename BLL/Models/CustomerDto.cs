using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TradingPointDto> TradingPoints { get; set; }
    }

    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TradingPointDto> TradingPoints { get; set; }
    }

    public class UpdateCustomerDto
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<TradingPointDto>? TradingPoints { get; set; }
    }
}
