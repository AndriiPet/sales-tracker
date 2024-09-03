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
    public class WorkRegionService : IWorkRegionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkRegionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateWorkRegionDto model)
        {
            var workRegion = _mapper.Map<CreateWorkRegionDto, WorkRegion>(model);

            await _unitOfWork.WorkRegionRepository.AddAsync(workRegion);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var workRegion = await _unitOfWork.WorkRegionRepository.GetByIdAsync(id);
            if(workRegion == null)
            {
                throw new System.Exception("Work Region not found");
            }
            await _unitOfWork.WorkRegionRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<WorkRegionDto>> GetAllAsync()
        {
            var workRegions = await _unitOfWork.WorkRegionRepository.GetAllAsync();
            if (workRegions == null)
            {
                throw new System.Exception("Work Regions not found");
            }

            return _mapper.Map<IEnumerable<WorkRegion>, IEnumerable<WorkRegionDto>>(workRegions);
        }

        public async Task<IEnumerable<WorkRegionDto>> GetAllWithUsers()
        {
            var workRegions = await _unitOfWork.WorkRegionRepository.GetAllWorkRegionsWithUsers();
            if (workRegions == null)
            {
                throw new System.Exception("Work Regions not found");
            }
            return _mapper.Map<IEnumerable<WorkRegion>, IEnumerable<WorkRegionDto>>(workRegions);
        }

        public async Task<WorkRegionDto> GetById(int id)
        {
            var workRegion = await _unitOfWork.WorkRegionRepository.GetByIdAsync(id);
            if (workRegion == null)
            {
                throw new System.Exception("Work Region not found");
            }
            return _mapper.Map<WorkRegion, WorkRegionDto>(workRegion);
        }

        public async Task UpdateAsync(int id, UpdateWorkRegionDto model)
        {
            var workRegion = await _unitOfWork.WorkRegionRepository.GetByIdAsync(id);
            if (workRegion == null)
            {
                throw new System.Exception("Work Region not found");
            }

            var updateWorkRegion = _mapper.Map<UpdateWorkRegionDto, WorkRegion>(model);
            _unitOfWork.WorkRegionRepository.Update(updateWorkRegion);
            await _unitOfWork.SaveAsync();
        }
    }
}
