using AutoMapper;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.TopicFile;
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
    public class TopicFileService : ITopicFileService
    {
        private readonly ITopicFileRepository _topicFileRepository;
        private readonly IMapper _mapper;

        public TopicFileService(ITopicFileRepository topicFileRepository, IMapper mapper)
        {
            _topicFileRepository = topicFileRepository;
            _mapper = mapper;
        }

        public async Task AddFiles(AddTopicFilesRequestDto requestDto)
        {
            foreach (var fileString in requestDto.FileStrings)
            {
                await _topicFileRepository.Add(new TopicFile
                {
                    TopicId = requestDto.TopicId,
                    FileURL = fileString,
                    Id = Guid.NewGuid().ToString(),
                    DateOfCreation = DateTime.Now
                });
            }
            await _topicFileRepository.Save();
        }

        public async Task<List<GetTopicFileResponceDto>> GetTopicFiles(GetTopicFileRequestDto requestDto)
        {
            var topicFiles = await _topicFileRepository.GetByTopicId(requestDto.TopicId);
            var responceDtos = _mapper.Map<List<GetTopicFileResponceDto>>(topicFiles.OrderBy(q => q.DateOfCreation).ToList());
            return responceDtos;
        }

        public async Task AddMissingFiles(AddMissingTopicFilesRequestDto requestDto)
        {
            var advertFiles = await GetTopicFiles(new GetTopicFileRequestDto
            {
                TopicId = requestDto.Topic.Id
            });
            var fileStrings = advertFiles.Select(q => q.FileString).ToList();
            var missingFiles = requestDto.FileStrings.Except(fileStrings).ToList();
            await AddFiles(new AddTopicFilesRequestDto
            {
                TopicId = requestDto.Topic.Id,
                FileStrings = missingFiles
            });
        }

        public async Task DeleteTopicFile(DeleteTopicFileRequestDto requestDto)
        {
            await _topicFileRepository.Delete(requestDto.FileId);
        }

    }
}
