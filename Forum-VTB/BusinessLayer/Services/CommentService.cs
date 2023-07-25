using AutoMapper;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IAdvertCommentRepository _advertCommentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;

        public CommentService(IAdvertCommentRepository advertCommentRepository, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager)
        {
            _advertCommentRepository = advertCommentRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<CreateCommentResponceDto> CreateComment(CreateCommentRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var comment = _mapper.Map<AdvertComment>(requestDto);
            comment.UserId = user.Id;
            comment.Id = Guid.NewGuid().ToString();
            comment.AdvertId = requestDto.AdvertId;
            comment.DateOfCreation = DateTime.Now;
            var addedComment = await _advertCommentRepository.Add(comment);
            await _advertCommentRepository.Save();
            return new CreateCommentResponceDto
            {
                AdvertId = addedComment.AdvertId,
                DateOfCreation = addedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = addedComment.Text
            };
        }

        public async Task<UpdateCommentResponceDto> UpdateComment(UpdateCommentRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var comment = await _advertCommentRepository.GetById(requestDto.CommentId);
            comment.Text = requestDto.Text;
            comment.DateOfCreation = DateTime.Now;
            var updatedComment = _advertCommentRepository.Update(comment);
            await _advertCommentRepository.Save();
            return new UpdateCommentResponceDto
            {
                AdvertId = updatedComment.AdvertId,
                DateOfCreation = updatedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = updatedComment.Text
            };
        }

        public async Task DeleteComment(DeleteCommentRequestDto requestDto)
        {
            await _advertCommentRepository.Delete(requestDto.CommentId);
            await _advertCommentRepository.Save();
        }

        public async Task<List<GetCommentResponceDto>> GetCommentsByAdvertId(GetCommentsRequestDto requestDto)
        {
            var comments = await _advertCommentRepository.GetByAdvertId(requestDto.AdvertId);
            var commentResponceDtos = _mapper.Map<List<GetCommentResponceDto>>(comments);
            return commentResponceDtos;
        }
    }
}
