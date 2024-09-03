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
    public class TripService : ITripService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TripService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateTripWithVisitsDto model)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var trip = _mapper.Map<CreateTripDto, Trip>(model.TripDto);
                await _unitOfWork.TripRepository.AddAsync(trip);
                await _unitOfWork.SaveAsync();

                foreach (var visitDto in model.VisitDtos)
                {
                    var visit = _mapper.Map<CreateVisitDto, Visit>(visitDto);
                    visit.TripId = trip.Id;
                    await _unitOfWork.VisitRepository.AddAsync(visit);
                }
                await _unitOfWork.SaveAsync();
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.TripRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TripDto>> GetAllAsync()
        {
            var trips = await _unitOfWork.TripRepository.GetAllAsync();
            if (trips == null)
            {
                throw new System.Exception("Trips not found");
            }

            return _mapper.Map<IEnumerable<Trip>, IEnumerable<TripDto>>(trips);
        }

        public async Task<TripDto> GetById(int id)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(id);
            if (trip == null)
            {
                throw new System.Exception("Trip wasn't found");
            }
            return _mapper.Map<Trip, TripDto>(trip);
        }

        public async Task<TripDto> getCurrentTripForUser(int userId, DateTime date)
        {
            var trip = await _unitOfWork.TripRepository.GetTripsByUserIdAndDate(userId, date);
            if (trip == null)
            {
                throw new System.Exception("Trip wasn't found");
            }
            return _mapper.Map<Trip, TripDto>(trip);
        }

        async Task<IEnumerable<TripDto>> ITripService.getTripByWorkRegion(int workRegionId)
        {
            var trip = await _unitOfWork.TripRepository.GetTripsByWorkRegion(workRegionId);
            if (trip == null)
            {
                throw new System.Exception("Trips wasn't found");
            }
            return _mapper.Map<IEnumerable<Trip>, IEnumerable<TripDto>>(trip);
        }

        public async Task<IEnumerable<TripDto>> getTripForUser(int userId)
        {
            var trips = await _unitOfWork.TripRepository.GetTripsByUser(userId);
            if (trips == null)
            {
                throw new System.Exception("Trips wasn't found");
            }
            return _mapper.Map<IEnumerable<Trip>, IEnumerable<TripDto>>(trips);

        }

        public async Task UpdateAsync(int id, UpdateTripDto model)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(id);
            if (trip == null)
            {
                throw new System.Exception($"Trip with id {id} not found.");
            }
            _mapper.Map(model, trip);
            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.SaveAsync();
        }
    }
}
