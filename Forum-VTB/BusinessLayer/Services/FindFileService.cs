using AutoMapper;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.FindFIles;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class FindFileService : IFindFileService
    {
        private readonly IFindFileRepository _findFileRepository;
        private readonly IMapper _mapper;

        public FindFileService(IFindFileRepository findFileRepository, IMapper mapper)
        {
            _findFileRepository = findFileRepository;
            _mapper = mapper;
        }

        public async Task AddFiles(AddFindFilesRequestDto requestDto)
        {
            foreach (var fileString in requestDto.FileStrings)
            {
                await _findFileRepository.Add(new FindFile
                {
                    FindId = requestDto.FindId,
                    FileURL = fileString,
                    Id = Guid.NewGuid().ToString(),
                    DateOfCreation = DateTime.Now
                });
            }
            await _findFileRepository.Save();
        }

        public async Task<List<GetFindFileResponceDto>> GetFindFiles(GetFindFileRequestDto requestDto)
        {
            var findFiles = await _findFileRepository.GetByFindId(requestDto.FindId);
            var responceDtos = _mapper.Map<List<GetFindFileResponceDto>>(findFiles.OrderBy(q => q.DateOfCreation).ToList());
            return responceDtos;
        }

        public async Task AddMissingFiles(AddMissingFindFilesRequestDto requestDto)
        {
            var findFiles = await GetFindFiles(new GetFindFileRequestDto
            {
                FindId = requestDto.Find.Id
            });
            var fileStrings = findFiles.Select(q => q.FileString).ToList();
            var missingFiles = requestDto.FileStrings.Except(fileStrings).ToList();
            await AddFiles(new AddFindFilesRequestDto
            {
                FindId = requestDto.Find.Id,
                FileStrings = missingFiles
            });
        }

        public async Task DeleteFindFile(DeleteFindFileRequestDto requestDto)
        {
            await _findFileRepository.Delete(requestDto.FileId);
        }

        private async Task DeleteFilesById(string findId)
        {
            await _findFileRepository.DeleteRange(findId);
            await _findFileRepository.Save();
        }

        public async Task UpdateFindFiles(UpdateFindFilesRequestDto requestDto)
        {
            await DeleteFilesById(requestDto.FindId);
            await AddFiles(new AddFindFilesRequestDto
            {
                FindId = requestDto.FindId,
                FileStrings = requestDto.FileStrings
            });
        }
    }
}
