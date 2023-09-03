using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Job;
using BusinessLayer.Dtos.JobFavourites;
using BusinessLayer.Dtos.JobFiles;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IJobFileService _jobFileService;
        private readonly IJobFavouriteService _jobFavouriteService;

        public JobController(IJobService jobService, IJobFavouriteService jobFavouriteService, IJobFileService jobFileService)
        {
            _jobService = jobService;
            _jobFavouriteService = jobFavouriteService;
            _jobFileService = jobFileService;
        }

        [AllowAnonymous]
        [HttpGet("/Jobs/GetFourNewestJobs")]
        public async Task<ActionResult> GetFourNewestJobs()
        {
            List<JobResponceDto> responceDtos;
            try
            {
                responceDtos = await _jobService.GetFourNewestJobs();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(responceDtos);
        }

        [HttpPost("/Jobs/CreateJob")]
        public async Task<ActionResult> CreateJob(CreateJobRequestDto requestDto)
        {
            CreateJobResponceDto responceDto;
            try
            {
                responceDto = await _jobService.CreateJob(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(responceDto);
        }

        [HttpPut("/Jobs/UpdateJob")]
        public async Task<ActionResult> UpdateJob(UpdateJobRequestDto requestDto)
        {
            UpdateJobResponceDto responceDto;
            try
            {
                responceDto = await _jobService.UpdateJob(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(responceDto);
        }

        [HttpDelete("/Jobs/DeleteJob")]
        public async Task<ActionResult> DeleteJob(DeleteJobRequestDto requestDto)
        {
            try
            {
                await _jobService.DeleteJob(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Job delete successfully!");
        }

        [HttpPost("/Jobs/ChangeJobStatus")]
        public async Task<ActionResult> ChangeJobStatus(ChangeJobStatusRequestDto requestDto)
        {
            try
            {
                await _jobService.ChangeJobStatus(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok("Status changed successfully!");
        }

        [HttpDelete("/JobFiles/DeleteJobFile")]
        public async Task<ActionResult> DeleteJobFile(DeleteJobFileRequestDto requestDto)
        {
            try
            {
                await _jobFileService.DeleteJobFile(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("File deleted successfully!");
        }

        [AllowAnonymous]
        [HttpPost("/Jobs/GetJobCard")]
        public async Task<ActionResult> GetJobCard(GetJobCardRequestDto requestDto)
        {
            GetJobCardResponceDto responceDto;
            try
            {
                responceDto = await _jobService.GetJobCard(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(responceDto);
        }

        [HttpPost("/JobFavourites/AddToJobFavourites")]
        public async Task<ActionResult> AddToFavourites(AddToJobFavouritesRequestDto requestDto)
        {
            try
            {
                await _jobFavouriteService.AddToJobFavourites(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Job added to favourites successfully!");
        }

        [HttpDelete("/JobFavourites/DeleteFromJobFavourites")]
        public async Task<ActionResult> DeleteFromJobFavourites(DeleteFromJobFavouritesRequestDto requestDto)
        {
            try
            {
                await _jobFavouriteService.DeleteFromJobFavourites(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Job deleted from favourites successfully!");
        }


        [AllowAnonymous]
        [HttpGet("/Jobs/GetAllJobs")]
        public async Task<ActionResult> GetAllJobs()
        {
            List<UserJobResponceDto> adverts;

            try
            {
                adverts = await _jobService.GetAllJobs();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(adverts);
        }

        [AllowAnonymous]
        [HttpPost("/Jobs/FindBySectionName")]
        public async Task<ActionResult> FindBySectionName(FindBySectionNameRequestDto requestDto)
        {
            List<JobResponceDto> responceDtos;

            try
            {
                responceDtos = await _jobService.FindBySectionName(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(responceDtos);
        }

        [AllowAnonymous]
        [HttpPost("/Jobs/FindBySubsectionName")]
        public async Task<ActionResult> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            List<JobResponceDto> responceDtos;

            try
            {
                responceDtos = await _jobService.FindBySubsectionName(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(responceDtos);
        }

    }
}
