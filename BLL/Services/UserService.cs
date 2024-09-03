using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateUserDto model)
        {
            var user = await _unitOfWork.UserRepository.GetUserByName(model.Name);
            if (user != null)
            {
                throw new InvalidOperationException("User with the same name already exist");
            }
            var userEntity = _mapper.Map<CreateUserDto, User>(model);
            await _unitOfWork.UserRepository.AddAsync(userEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if(user == null)
            {
                throw new System.Exception("User not found");
            }
            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users =  await _unitOfWork.UserRepository.GetAllAsync();
            if (users == null)
            {
                throw new System.Exception("Users not found");
            }
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> GetAllManagers()
        {
            var managers = await _unitOfWork.UserRepository.GetAllManagers();
            if (managers == null)
            {
                throw new System.Exception("Managers not found");
            }
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(managers);
        }

        public async Task<IEnumerable<UserDto>> GetAllSubordinates(int userId)
        {
            var subordinate = await _unitOfWork.UserRepository.GetAllSubordinates(userId);
            if (subordinate == null)
            {
                throw new System.Exception("Subordinates not found");
            }
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(subordinate);
        }

        public async Task<IEnumerable<UserDto>> GetAllWithPagination(int page, int limit)
        {
            var users = await _unitOfWork.UserRepository.GetAllUsersWithPagination(page, limit);
            if(users == null)
            {
                throw new System.Exception("Users not found");
            }
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new System.Exception("User not found");
            }
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetByName(string name)
        {
            var user = await _unitOfWork.UserRepository.GetUserByName(name);
            if (user == null)
            {
                throw new System.Exception("User not found");
            }
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<IGrouping<WorkRegionDto, UserDto>>> GetSubordinatesByRegion(int userId)
        {
            var manager = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (manager == null)
                throw new System.Exception("Manager not found");

            var subordinatesByRegion = await _unitOfWork.UserRepository.GetSubordinatesByRegion(userId);
           if (!subordinatesByRegion.Any())
                throw new System.Exception("Subordinates not found");

            return subordinatesByRegion
             .SelectMany(group => group.Select(user => new
                  {
                     Region = _mapper.Map<WorkRegion, WorkRegionDto>(group.Key),
                     User = _mapper.Map<User, UserDto>(user)
                  }))
             .GroupBy(x => x.Region, x => x.User);
        }

        public async Task<IEnumerable<UserDto>> GetSubordinatesInRegion(int regionId, int managerId)
        {
            var manager = await _unitOfWork.UserRepository.GetByIdAsync(managerId);
            if (manager == null)
                throw new System.Exception("Manager not found");
            return _mapper
                .Map<IEnumerable<User>, IEnumerable<UserDto>>
                (await _unitOfWork.UserRepository.GetSubordinatesInRegion(regionId, managerId));
        }

        public async Task<WorkRegionDto> GetUserLocation(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if(user == null)
                throw new System.Exception("User not found");

            var location = await _unitOfWork.UserRepository.GetUserLocation(id);
            if (location == null)
                throw new System.Exception("User location not found");

            return _mapper.Map<WorkRegion, WorkRegionDto>(location);

        }

        public async Task<IEnumerable<UserDto>> GetUsersWithoutRegion()
        {
            var users = await _unitOfWork.UserRepository.GetUsersWithoutRegion();
            if (users == null)
                throw new System.Exception("Users not found");
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<VisitDto>> GetVisitsForUser(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new System.Exception("User not found");
            return _mapper
                .Map<IEnumerable<Visit>, IEnumerable<VisitDto>>
                (await _unitOfWork.UserRepository.GetVisitsForUser(userId));
        }

        public async Task<(int CompletedVisits, int TotalVisits)> GetVisitsStats(int userId, DateTime currentDate)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new System.Exception("User not found");
            return await _unitOfWork.UserRepository.GetVisitedVisitsStats(userId, currentDate);
        }

        public async Task UpdateAsync(int id, UpdateUserDto model)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new System.Exception("User not found");
            _unitOfWork.UserRepository.Update(_mapper.Map<UpdateUserDto, User>(model));
            _unitOfWork.SaveAsync();
        }
    }
}
