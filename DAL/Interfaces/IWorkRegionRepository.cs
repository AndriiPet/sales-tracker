using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWorkRegionRepository : IRepository<WorkRegion>
    {
        Task<IEnumerable<WorkRegion>> GetAllWorkRegionsWithUsers();
        Task<WorkRegion> GetWorkRegionByName(string name);
    }
}
