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
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFtpService _ftpService;

        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper, IFtpService ftpService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ftpService = ftpService;
        }

        public async Task AddAsync(int VisitId, Stream fileStream, string fileExtension)
        {
            if (VisitId == null)
                throw new ArgumentNullException(nameof(VisitId));

            if (fileStream == null)
                throw new ArgumentNullException(nameof(fileStream));

            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentException("File extension must be provided", nameof(fileExtension));

            string fileName = $"{Guid.NewGuid()}{fileExtension}";

            string ftpFilePath = await _ftpService.UploadFileAsync(fileStream, fileName);

            var photo = new Photo
            {
                FilePath = ftpFilePath,
                VisitId = VisitId
            };

            await _unitOfWork.PhotoRepository.AddAsync(photo);
            await _unitOfWork.SaveAsync();
        }


        public async Task DeleteByIdAsync(int id)
        {
            var photo = await _unitOfWork.PhotoRepository.GetByIdAsync(id);
            if (photo != null)
            {
                await _unitOfWork.PhotoRepository.DeleteByIdAsync(id);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<PhotoDto> GetByIdAsync(int id)
        {
            var photo = await _unitOfWork.PhotoRepository.GetByIdAsync(id);
            if (photo == null)
                throw new System.Exception("Photo not found");

            var photoDto = _mapper.Map<PhotoDto>(photo);
            using (var stream = await _ftpService.DownloadFileAsync(photo.FilePath))
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                photoDto.FileContent = memoryStream.ToArray();
            }

            return photoDto;
        }

        public async Task<IEnumerable<PhotoDto>> GetByVisitIdAsync(int visitId)
        {
            var photos = await _unitOfWork.PhotoRepository.GetByVisitIdAsync(visitId);
            var photoDtos = new List<PhotoDto>();

            foreach (var photo in photos)
            {
                var photoDto = _mapper.Map<PhotoDto>(photo);
                using (var stream = await _ftpService.DownloadFileAsync(photo.FilePath))
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    photoDto.FileContent = memoryStream.ToArray();
                }
                photoDtos.Add(photoDto);
            }

            return photoDtos;
        }
    }
}
