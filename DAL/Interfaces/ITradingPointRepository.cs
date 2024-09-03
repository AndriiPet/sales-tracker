using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITradingPointRepository : IRepository<TradingPoint>
    {
        Task<TradingPoint> GetByIdWithRelationsAsync(int id);
        Task<TradingPoint> GetByNameWithRelationsAsync(string name);
        Task<(IEnumerable<TradingPoint> TradingPoints, int Total)> GetAllWithPaginationAsync(int skip, int take);
        Task<IEnumerable<TradingPoint>> GetByUserIdAsync(int userId);
        Task<IEnumerable<TradingPoint>> GetByWorkRegionIdAsync(int workRegionId);
        Task<TradingPoint> GetByNameAsync(string name);
    }
}
