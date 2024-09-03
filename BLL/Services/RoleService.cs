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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateRoleDto model)
        {
            var existingRole = await _unitOfWork.RoleRepository.GetByNameAsync(model.Name);
            if (existingRole != null)
            {
                throw new InvalidOperationException("Role with the same name already exist");
            }
            var role = _mapper.Map<CreateRoleDto, Role>(model);
            await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.RoleRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            if (roles == null)
            {
                throw new System.Exception("Roles weren't found");
            }

            return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetById(int id)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new System.Exception("Role wasn't found");
            }
            return _mapper.Map<Role, RoleDto>(role);
        }

        public async Task UpdateAsync(int id, UpdateRoleDto model)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new System.Exception($"Role with ID {id} not found");
            }
            var mapperRole = _mapper.Map<UpdateRoleDto, Role>(model);
            _unitOfWork.RoleRepository.Update(mapperRole);
            await _unitOfWork.SaveAsync();
        }
    }
}
