using BusinessLayer.Dtos.JobFiles;

namespace BusinessLayer.Dtos.Job
{
    public class GetJobCardResponceDto
    {
        public string? JobId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? NickName { get; set; }

        public string? UserName { get; set; }

        public string? UserPhoto { get; set; }

        public string? SubsectionName { get; set; }

        public string? Price { get; set; }

        public DateTime DateOfCreation { get; set; }

        public bool IsFavourite { get; set; }

        public string? PhoneNumber { get; set; }

        public string? MainPhoto { get; set; }

        public List<GetJobFileResponceDto>? Files { get; set; }
    }
}
