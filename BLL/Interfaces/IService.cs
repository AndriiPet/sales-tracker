using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<TModel, TCreateModel, TUpdateModel>
    {
        Task UpdateAsync(int id, TUpdateModel model);
        Task AddAsync(TCreateModel model);
        Task DeleteAsync(int id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetById(int id);
    }
}
