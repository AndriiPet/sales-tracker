using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOFWork : IUnitOfWork, IDisposable
    {
        private readonly TorgDBContext _torgDBContext;

        public ICustomerRepository CustomerRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public ITradingPointRepository TradingPointRepository { get; }
        public ITripRepository TripRepository { get; }
        public IUserRepository UserRepository { get; }
        public IVisitRepository VisitRepository { get; }
        public IWorkRegionRepository WorkRegionRepository { get; }

        public UnitOFWork(TorgDBContext torgDBContext,
             ICustomerRepository customerRepository,
            IPhotoRepository photoRepository,
            IRoleRepository roleRepository,
            ITradingPointRepository tradingPointRepository,
            ITripRepository tripRepository,
            IUserRepository userRepository,
            IVisitRepository visitRepository,
            IWorkRegionRepository workRegionRepository)
        {
            _torgDBContext = torgDBContext;
            CustomerRepository = customerRepository;
            PhotoRepository = photoRepository;
            RoleRepository = roleRepository;
            TradingPointRepository = tradingPointRepository;
            TripRepository = tripRepository;
            UserRepository = userRepository;
            VisitRepository = visitRepository;
            WorkRegionRepository = workRegionRepository;
        }

        public async Task<int> SaveAsync()
        {
            UpdateTimestamps();
            return await _torgDBContext.SaveChangesAsync();
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using (var transaction = await _torgDBContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await action();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private void UpdateTimestamps()
        {
            var entries = _torgDBContext.ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }

                entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        public void Dispose()
        {
            _torgDBContext.Dispose();
        }
    }
}
