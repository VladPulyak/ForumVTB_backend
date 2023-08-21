using BusinessLayer.Dtos.JobFiles;

namespace BusinessLayer.Dtos.Job
{
    public class CreateJobResponceDto
    {
        public string? JobId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public string? MainPhoto { get; set; }

        public string? Status { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<GetJobFileResponceDto>? Files { get; set; }

    }
}
