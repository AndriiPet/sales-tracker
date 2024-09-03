using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IRoleRepository RoleRepository { get; }
        ITradingPointRepository TradingPointRepository { get; }
        ITripRepository TripRepository { get; }
        IUserRepository UserRepository { get; }
        IVisitRepository VisitRepository { get; }
        IWorkRegionRepository WorkRegionRepository { get; }
        Task<int> SaveAsync();
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}
