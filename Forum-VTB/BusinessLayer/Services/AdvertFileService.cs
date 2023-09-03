using AutoMapper;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AdvertFileService : IAdvertFileService
    {
        private readonly IAdvertFileRepository _advertFileRepository;
        private readonly IMapper _mapper;

        public AdvertFileService(IAdvertFileRepository advertFileRepository, IMapper mapper)
        {
            _advertFileRepository = advertFileRepository;
            _mapper = mapper;
        }

        public async Task AddFiles(AddAdvertFilesRequestDto requestDto)
        {
            foreach (var fileString in requestDto.FileStrings)
            {
                await _advertFileRepository.Add(new AdvertFile
                {
                    AdvertId = requestDto.AdvertId,
                    FileURL = fileString,
                    Id = Guid.NewGuid().ToString(),
                    DateOfCreation = DateTime.Now
                });
            }
            await _advertFileRepository.Save();
        }

        public async Task<List<GetAdvertFileResponceDto>> GetAdvertFiles(GetAdvertFileRequestDto requestDto)
        {
            var advertFiles = await _advertFileRepository.GetByAdvertId(requestDto.AdvertId);
            var responceDtos = _mapper.Map<List<GetAdvertFileResponceDto>>(advertFiles.OrderBy(q => q.DateOfCreation).ToList());
            return responceDtos;
        }

        public async Task AddMissingFiles(AddMissingAdvertFilesRequestDto requestDto)
        {
            var advertFiles = await GetAdvertFiles(new GetAdvertFileRequestDto
            {
                AdvertId = requestDto.Advert.Id
            });
            var fileStrings = advertFiles.Select(q => q.FileString).ToList();
            var missingFiles = requestDto.FileStrings.Except(fileStrings).ToList();
            await AddFiles(new AddAdvertFilesRequestDto
            {
                AdvertId = requestDto.Advert.Id,
                FileStrings = missingFiles
            });
        }

        public async Task DeleteAdvertFile(DeleteAdvertFileRequestDto requestDto)
        {
            await _advertFileRepository.Delete(requestDto.FileId);
        }

        private async Task DeleteFilesById(string advertId)
        {
            await _advertFileRepository.DeleteRange(advertId);
            await _advertFileRepository.Save();
        }

        public async Task UpdateAdvertFiles(UpdateAdvertFilesRequestDto requestDto)
        {
            await DeleteFilesById(requestDto.AdvertId);
            await AddFiles(new AddAdvertFilesRequestDto
            {
                AdvertId = requestDto.AdvertId,
                FileStrings = requestDto.FileStrings
            });
        }
    }
}
