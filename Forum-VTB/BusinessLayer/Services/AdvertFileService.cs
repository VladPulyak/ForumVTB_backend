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

        public async Task<List<AdvertFile>> AddFiles(AddAdvertFileRequestDto requestDto)
        {
            var advertFiles = new List<AdvertFile>();
            foreach (var fileString in requestDto.FileStrings)
            {
                advertFiles.Add(new AdvertFile
                {
                    AdvertId = requestDto.AdvertId,
                    FileURL = fileString,
                    Id = Guid.NewGuid().ToString()
                });
            }

            await _advertFileRepository.AddRange(advertFiles);
            await _advertFileRepository.Save();
            return advertFiles;
        }

        public async Task<List<GetAdvertFileResponceDto>> GetAdvertFiles(GetAdvertFileRequestDto requestDto)
        {
            var advertFiles = await _advertFileRepository.GetByAdvertId(requestDto.AdvertId);
            var responceDtos = _mapper.Map<List<GetAdvertFileResponceDto>>(advertFiles);
            return responceDtos;
        }

        public async Task AddMissingFiles(AddMissingFilesRequestDto requestDto)
        {
            var advertFiles = await GetAdvertFiles(new GetAdvertFileRequestDto
            {
                AdvertId = requestDto.Advert.Id
            });
            var fileStrings = advertFiles.Select(q => q.FileString).ToList();
            var missingFiles = requestDto.FileStrings.Except(fileStrings).ToList();
            await AddFiles(new AddAdvertFileRequestDto
            {
                AdvertId = requestDto.Advert.Id,
                FileStrings = missingFiles
            });

        }
    }
}
