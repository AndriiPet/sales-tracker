using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IWorkRegionService : IService<WorkRegionDto, CreateWorkRegionDto, UpdateWorkRegionDto>
    {
        Task<IEnumerable<WorkRegionDto>> GetAllWithUsers();
    }
}
