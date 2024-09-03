using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public VisitService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task AddAsync(CreateVisitDto model)
        {
            var visit = _mapper.Map<CreateVisitDto, Visit>(model);
            await _unitOfWork.VisitRepository.AddAsync(visit);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.VisitRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<VisitDto>> GetAllAsync()
        {
            var visits = await _unitOfWork.VisitRepository.GetAllAsync();
            if(visits == null)
            {
                throw new System.Exception("Visit not found");
            }

            return _mapper.Map<IEnumerable<Visit>, IEnumerable<VisitDto>>(visits);
            
        }

        public async Task<VisitDto> GetById(int id)
        {
            var visit = await _unitOfWork.VisitRepository.GetByIdAsync(id);
            if (visit == null)
            {
                throw new System.Exception("Visit not found");
            }
            return _mapper.Map<Visit, VisitDto>(visit);
        }

        public async Task<IEnumerable<VisitDto>> GetVisitByUserAndDate(int userId, DateTime date)
        {
            var visits = await _unitOfWork.VisitRepository.GetVisitsByUserIdAndDate(userId, date);
            if(visits == null)
            {
                throw new System.Exception("Visits not found");
            }
            return _mapper.Map<IEnumerable<Visit>, IEnumerable<VisitDto>>(visits);
        }

        public async Task<IEnumerable<VisitDto>> GetVisitsByTripId(int tripId)
        {
            var visits = await _unitOfWork.VisitRepository.GetVisitsByTripId(tripId);
            if (visits == null)
            {
                throw new System.Exception("Visits not found");
            }
            return _mapper.Map<IEnumerable<Visit>, IEnumerable<VisitDto>>(visits);
        }

        public async Task UpdateAsync(int id, UpdateVisitDto model)
        {
            var visit = await _unitOfWork.VisitRepository.GetByIdAsync(id);
            if (visit == null)
            {
                throw new System.Exception($"Visit with id {id} not found.");
            }

            var mappedVisit = _mapper.Map<UpdateVisitDto, Visit>(model);

            _unitOfWork.VisitRepository.Update(mappedVisit);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateWithPhotoAsync(int visitId, UpdateVisitWithPhotosDto model, List<IFormFile> photos)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var visit = await _unitOfWork.VisitRepository.GetByIdAsync(visitId);
                if (visit == null)
                    throw new ArgumentException("Visit not found", nameof(visitId));
                var mappedVisit = _mapper.Map<UpdateVisitWithPhotosDto, Visit>(model);

                _unitOfWork.VisitRepository.Update(mappedVisit);
                await _unitOfWork.SaveAsync();

                foreach (var photo in photos)
                {
                    if (photo.Length > 0)
                    {
                        using (var stream = photo.OpenReadStream())
                        {
                            var fileExtension = Path.GetExtension(photo.FileName);
                            await _photoService.AddAsync(visitId, stream, fileExtension);
                        }
                    }
                }
                await _unitOfWork.SaveAsync();
            });
        }
    }
}
