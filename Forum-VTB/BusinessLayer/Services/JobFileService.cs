using AutoMapper;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.JobFiles;
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
    public class JobFileService : IJobFileService
    {
        private readonly IJobFileRepository _jobFileRepository;
        private readonly IMapper _mapper;

        public JobFileService(IJobFileRepository jobFileRepository, IMapper mapper)
        {
            _jobFileRepository = jobFileRepository;
            _mapper = mapper;
        }

        public async Task<List<JobFile>> AddFiles(AddJobFilesRequestDto requestDto)
        {
            var addedFiles = new List<JobFile>();

            foreach (var fileString in requestDto.FileStrings)
            {
                addedFiles.Add(await _jobFileRepository.Add(new JobFile
                {
                    JobId = requestDto.JobId,
                    FileURL = fileString,
                    Id = Guid.NewGuid().ToString(),
                    DateOfCreation = DateTime.Now
                }));
            }
            await _jobFileRepository.Save();
            return addedFiles.OrderBy(q => q.DateOfCreation).ToList();
        }

        public async Task<List<GetJobFileResponceDto>> GetJobFiles(GetJobFileRequestDto requestDto)
        {
            var jobFiles = await _jobFileRepository.GetByJobId(requestDto.JobId);
            var responceDtos = _mapper.Map<List<GetJobFileResponceDto>>(jobFiles.OrderBy(q => q.DateOfCreation).ToList());
            return responceDtos;
        }

        public async Task AddMissingFiles(AddMissingJobFilesRequestDto requestDto)
        {
            var jobFiles = await GetJobFiles(new GetJobFileRequestDto
            {
                JobId = requestDto.Job.Id
            });
            var fileStrings = jobFiles.Select(q => q.FileString).ToList();
            var missingFiles = requestDto.FileStrings.Except(fileStrings).ToList();
            await AddFiles(new AddJobFilesRequestDto
            {
                JobId = requestDto.Job.Id,
                FileStrings = missingFiles
            });
        }

        public async Task DeleteJobFile(DeleteJobFileRequestDto requestDto)
        {
            await _jobFileRepository.Delete(requestDto.FileId);
        }
    }
}
