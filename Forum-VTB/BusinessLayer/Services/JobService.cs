using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Job;
using BusinessLayer.Dtos.JobFiles;
using BusinessLayer.Interfaces;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IJobFavouriteRepository _jobFavouriteRepository;
        private readonly IJobFileService _jobFileService;

        public JobService(IJobRepository jobRepository, ISubsectionRepository subsectionRepository, IHttpContextAccessor contextAccessor, IMapper mapper, UserManager<UserProfile> userManager, IJobFavouriteRepository jobFavouriteRepository, IJobFileService jobFileService)
        {
            _jobRepository = jobRepository;
            _subsectionRepository = subsectionRepository;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _jobFavouriteRepository = jobFavouriteRepository;
            _jobFileService = jobFileService;
        }

        public async Task<CreateJobResponceDto> CreateJob(CreateJobRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var job = _mapper.Map<Job>(requestDto);
            var subsection = await _subsectionRepository.GetBySubsectionAndSectionNames(requestDto.SectionName, requestDto.SubsectionName);
            job.Subsection = subsection;
            job.UserId = user.Id;
            job.Status = Status.Active.ToString();
            job.DateOfCreation = DateTime.UtcNow;
            job.Id = Guid.NewGuid().ToString();
            job.PhoneNumber = requestDto.PhoneNumber;
            var addedJob = await _jobRepository.Add(job);
            await _jobRepository.Save();
            var addedFiles = await _jobFileService.AddFiles(new AddJobFilesRequestDto
            {
                JobId = job.Id,
                FileStrings = requestDto.FileStrings
            });

            return new CreateJobResponceDto
            {
                JobId = addedJob.Id,
                Title = addedJob.Title,
                Description = addedJob.Description,
                Price = addedJob.Price,
                DateOfCreation = job.DateOfCreation,
                MainPhoto = addedJob.MainPhoto,
                Status = addedJob.Status,
                Files = _mapper.Map<List<GetJobFileResponceDto>>(addedFiles)
            };
        }

        public async Task<UpdateJobResponceDto> UpdateJob(UpdateJobRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var job = await _jobRepository.GetActiveById(requestDto.JobId);
            job.Title = requestDto.Title;
            job.Description = requestDto.Description;
            job.Price = requestDto.Price;
            job.MainPhoto = requestDto.MainPhoto;
            job.DateOfCreation = DateTime.UtcNow;
            job.Status = Status.Active.ToString();
            await _jobFileService.AddMissingFiles(new AddMissingJobFilesRequestDto
            {
                Job = job,
                FileStrings = requestDto.FileStrings
            });
            var updatedJob = _jobRepository.Update(job);
            await _jobRepository.Save();
            var favourite = await _jobFavouriteRepository.GetByJobAndUserIds(updatedJob.Id, user.Id);
            bool isFavourite = favourite is not null ? true : false;
            return new UpdateJobResponceDto
            {
                JobId = updatedJob.Id,
                Title = updatedJob.Title,
                Description = updatedJob.Description,
                Price = updatedJob.Price,
                DateOfCreation = updatedJob.DateOfCreation,
                Status = updatedJob.Status,
                MainPhoto = updatedJob.MainPhoto,
                Files = await _jobFileService.GetJobFiles(new GetJobFileRequestDto
                {
                    JobId = updatedJob.Id
                }),
                IsFavourite = isFavourite
            };
        }

        public async Task<List<JobResponceDto>> GetFourNewestJobs()
        {
            var jobs = await _jobRepository.GetAll().OrderByDescending(q => q.DateOfCreation).Take(4).ToListAsync();
            var responceDtos = _mapper.Map<List<JobResponceDto>>(jobs);
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task<List<JobResponceDto>> CheckFavourites(List<JobResponceDto> responceDtos, string userId)
        {
            var favourites = await _jobFavouriteRepository.GetByUserId(userId);
            foreach (var responceDto in responceDtos)
            {
                if (favourites.SingleOrDefault(q => q.Id == responceDto.JobId) is not null)
                {
                    responceDto.IsFavourite = true;
                }
            }

            return responceDtos;
        }

        public async Task DeleteJob(DeleteJobRequestDto requestDto)
        {
            await _jobRepository.Delete(requestDto.JobId);
            await _jobRepository.Save();
        }

        public async Task<List<JobResponceDto>> GetUserJobs()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userJobs = await _jobRepository.GetByUserId(user.Id);
            var responceDtos = _mapper.Map<List<JobResponceDto>>(userJobs);
            return await CheckFavourites(responceDtos, user.Id);
        }

        public async Task<GetJobCardResponceDto> GetJobCard(GetJobCardRequestDto requestDto)
        {
            bool isFavourite = false;
            var userJob = await _jobRepository.GetActiveById(requestDto.JobId);
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                var favourite = await _jobFavouriteRepository.GetByJobAndUserIds(userJob.Id, user.Id);
                isFavourite = favourite is not null ? true : false;
            }

            return new GetJobCardResponceDto
            {
                JobId = userJob.Id,
                Title = userJob.Title,
                Description = userJob.Description,
                DateOfCreation = userJob.DateOfCreation,
                Price = userJob.Price,
                Files = await _jobFileService.GetJobFiles(new GetJobFileRequestDto
                {
                    JobId = userJob.Id
                }),
                NickName = userJob.User.NickName,
                UserName = userJob.User.UserName,
                UserPhoto = userJob.User.Photo,
                SubsectionName = userJob.Subsection.Name,
                SectionName = userJob.Subsection.Section.Name,
                IsFavourite = isFavourite,
                PhoneNumber = userJob.PhoneNumber,
                MainPhoto = userJob.MainPhoto
            };
        }

        public async Task<List<UserJobResponceDto>> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAll().ToListAsync();
            if (!jobs.Any())
            {
                throw new ObjectNotFoundException("Jobs not found");
            }

            var responceDtos = _mapper.Map<List<UserJobResponceDto>>(jobs);
            return responceDtos;
        }

        public async Task<List<JobResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto)
        {
            var jobs = await _jobRepository.GetBySectionName(requestDto.SectionName);
            var responceDtos = _mapper.Map<List<JobResponceDto>>(jobs.OrderByDescending(q => q.DateOfCreation).ToList());
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task<List<JobResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            var jobs = await _jobRepository.GetBySubsectionName(requestDto.SubsectionName, requestDto.SectionName);
            var responceDtos = _mapper.Map<List<JobResponceDto>>(jobs.OrderByDescending(q => q.DateOfCreation).ToList());
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task ChangeJobStatus(ChangeJobStatusRequestDto requestDto)
        {
            var userJob = await _jobRepository.GetActiveById(requestDto.JobId);

            if (userJob.Status == "Active")
            {
                userJob.Status = Status.Disabled.ToString();
            }
            else
            {
                userJob.Status = Status.Active.ToString();
            }
            _jobRepository.Update(userJob);
            await _jobRepository.Save();
        }
    }
}