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
    public class TradingPointService : ITradingPointService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TradingPointService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateTradingPointDto model)
        {
            var existingTradingPoint = await _unitOfWork.TradingPointRepository.GetByNameAsync(model.Name);
            if(existingTradingPoint == null)
            {
                throw new InvalidOperationException("Trading Point with the same name already exist");
            }
            var tradingPoint = _mapper.Map<CreateTradingPointDto, TradingPoint>(model);
            await _unitOfWork.TradingPointRepository.AddAsync(tradingPoint);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.TradingPointRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<TradingPointDto> GetById(int id)
        {
            var tradingPoint = await _unitOfWork.TradingPointRepository.GetByIdAsync(id);
            if (tradingPoint == null)
            {
                throw new System.Exception("Trading Point not found");
            }
            return _mapper.Map<TradingPoint, TradingPointDto>(tradingPoint);
        }

        public async Task<IEnumerable<TradingPointDto>> GetAllAsync()
        {
            var tradingPoints = await _unitOfWork.TradingPointRepository.GetAllAsync();
            if(tradingPoints == null)
            {
                throw new System.Exception("Trading Points weren't found");
            }
            return _mapper.Map<IEnumerable<TradingPoint>, IEnumerable<TradingPointDto>>(tradingPoints);
        }

        public async Task<IEnumerable<TradingPointDto>> GetAllWithPaginationAsync(int page, int limit)
        {

            var tradingPoints = await _unitOfWork.TradingPointRepository.GetAllAsync();
            if (tradingPoints == null)
            {
                throw new System.Exception("Trading Points weren't found");
            }
            var paginatedTradingPoints = tradingPoints.Skip((page - 1) * limit).Take(limit);
            return _mapper.Map<IEnumerable<TradingPoint>, IEnumerable<TradingPointDto>>(paginatedTradingPoints);
        }

        public async Task<TradingPointDto> GetByUserIdAsync(int id)
        {
            var tradingPoint = await _unitOfWork.TradingPointRepository.GetByIdAsync(id);
            if(tradingPoint == null)
            {
                throw new System.Exception("Trading Point not found");
            }
            return _mapper.Map<TradingPoint, TradingPointDto>(tradingPoint);
        }

        public async Task<TradingPointDto> GetByNameAsync(string name)
        {
            var tradingPoint = await _unitOfWork.TradingPointRepository.GetByNameAsync(name);
            if (tradingPoint == null)
            {
                throw new System.Exception("Trading Point not found");
            }
            return _mapper.Map<TradingPoint, TradingPointDto>(tradingPoint);
        }

        public async Task<IEnumerable<TradingPointDto>> GetByWorkRegionAsync(int workRegionId)
        {
            var tradingPoints = await _unitOfWork.TradingPointRepository.GetByWorkRegionIdAsync(workRegionId);
            if (tradingPoints == null)
            {
                throw new System.Exception("Trading Point not found");
            }
            return _mapper.Map<IEnumerable<TradingPoint>, IEnumerable<TradingPointDto>>(tradingPoints);
        }

        public async Task UpdateAsync(int id, UpdateTradingPointDto model)
        {
            var tradingPoint = await _unitOfWork.TradingPointRepository.GetByIdAsync(id);
            if (tradingPoint == null)
            {
                throw new System.Exception($"Trading Point with ID {id} not found");
            }
            var mapperTradungPoint = _mapper.Map<UpdateTradingPointDto, TradingPoint>(model);
            _unitOfWork.TradingPointRepository.Update(mapperTradungPoint);
            await _unitOfWork.SaveAsync();
        }
    }
}
