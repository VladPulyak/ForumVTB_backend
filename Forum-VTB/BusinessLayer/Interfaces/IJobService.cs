using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Job;

namespace BusinessLayer.Interfaces
{
    public interface IJobService
    {
        Task<CreateJobResponceDto> CreateJob(CreateJobRequestDto requestDto);

        Task<UpdateJobResponceDto> UpdateJob(UpdateJobRequestDto requestDto);

        Task DeleteJob(DeleteJobRequestDto requestDto);

        Task<List<JobResponceDto>> GetUserJobs();

        Task<GetJobCardResponceDto> GetJobCard(GetJobCardRequestDto requestDto);

        Task<List<UserJobResponceDto>> GetAllJobs();

        Task<List<JobResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto);

        Task<List<JobResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto);

        Task<List<JobResponceDto>> GetFourNewestJobs();

        Task<List<JobResponceDto>> CheckFavourites(List<JobResponceDto> responceDtos, string userId);

        Task ChangeJobStatus(ChangeJobStatusRequestDto requestDto);
    }
}
