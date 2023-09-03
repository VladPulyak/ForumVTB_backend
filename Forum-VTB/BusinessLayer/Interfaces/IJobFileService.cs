using BusinessLayer.Dtos.JobFiles;
using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IJobFileService
    {
        Task<List<JobFile>> AddFiles(AddJobFilesRequestDto requestDto);

        Task<List<GetJobFileResponceDto>> GetJobFiles(GetJobFileRequestDto requestDto);

        Task DeleteJobFile(DeleteJobFileRequestDto requestDto);

        Task AddMissingFiles(AddMissingJobFilesRequestDto requestDto);

        Task UpdateJobFiles(UpdateJobFilesRequestDto requestDto);
    }
}
