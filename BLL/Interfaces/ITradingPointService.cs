using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITradingPointService : IService< TradingPointDto, CreateTradingPointDto, UpdateTradingPointDto>
    {
        Task<IEnumerable<TradingPointDto>> GetAllWithPaginationAsync(int page, int limit);
        Task<TradingPointDto> GetByNameAsync(string name);
        Task<TradingPointDto> GetByUserIdAsync(int userId);
        Task<IEnumerable<TradingPointDto>> GetByWorkRegionAsync(int userId);
    }
}
