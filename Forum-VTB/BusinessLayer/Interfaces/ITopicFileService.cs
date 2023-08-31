using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.TopicFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITopicFileService
    {
        Task AddFiles(AddTopicFilesRequestDto requestDto);

        Task<List<GetTopicFileResponceDto>> GetTopicFiles(GetTopicFileRequestDto requestDto);

        Task DeleteTopicFile(DeleteTopicFileRequestDto requestDto);

        Task AddMissingFiles(AddMissingTopicFilesRequestDto requestDto);
    }
}
