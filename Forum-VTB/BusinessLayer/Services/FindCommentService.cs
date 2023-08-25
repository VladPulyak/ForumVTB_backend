using AutoMapper;
using BusinessLayer.Dtos.FindComments;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.Services
{
    public class FindCommentService : IFindCommentService
    {
        private readonly IFindCommentRepository _findCommentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;

        public FindCommentService(IFindCommentRepository findCommentRepository, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager)
        {
            _findCommentRepository = findCommentRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<CreateFindCommentResponceDto> CreateComment(CreateFindCommentRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var comment = _mapper.Map<FindComment>(requestDto);
            comment.UserId = user.Id;
            comment.Id = Guid.NewGuid().ToString();
            comment.FindId = requestDto.FindId;
            comment.DateOfCreation = DateTime.UtcNow;
            var addedComment = await _findCommentRepository.Add(comment);
            await _findCommentRepository.Save();
            return new CreateFindCommentResponceDto
            {
                CommentId = addedComment.Id,
                FindId = addedComment.FindId,
                DateOfCreation = addedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = addedComment.Text
            };
        }

        public async Task<UpdateFindCommentResponceDto> UpdateComment(UpdateFindCommentRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var comment = await _findCommentRepository.GetById(requestDto.CommentId);
            comment.Text = requestDto.Text;
            comment.DateOfCreation = DateTime.UtcNow;
            var updatedComment = _findCommentRepository.Update(comment);
            await _findCommentRepository.Save();
            return new UpdateFindCommentResponceDto
            {
                CommentId = updatedComment.Id,
                FindId = updatedComment.FindId,
                DateOfCreation = updatedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = updatedComment.Text
            };
        }

        public async Task DeleteComment(DeleteFindCommentRequestDto requestDto)
        {
            await _findCommentRepository.Delete(requestDto.CommentId);
            await _findCommentRepository.Save();
        }

        public async Task<List<GetFindCommentResponceDto>> GetCommentsByFindId(GetFindCommentRequestDto requestDto)
        {
            var comments = await _findCommentRepository.GetByFindId(requestDto.FindId);
            var commentResponceDtos = _mapper.Map<List<GetFindCommentResponceDto>>(comments.OrderBy(q => q.DateOfCreation));
            return commentResponceDtos;
        }

        public async Task<ReplyFindCommentResponceDto> ReplyComment(ReplyFindCommentRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var parentCommnent = await _findCommentRepository.GetById(requestDto.ParentCommentId);
            var comment = _mapper.Map<FindComment>(requestDto);
            comment.UserId = user.Id;
            comment.Id = Guid.NewGuid().ToString();
            comment.ParentCommentId = parentCommnent.Id;
            comment.ParentComment = parentCommnent;
            comment.DateOfCreation = DateTime.UtcNow;
            var addedComment = await _findCommentRepository.Add(comment);
            await _findCommentRepository.Save();
            return new ReplyFindCommentResponceDto
            {
                CommentId = addedComment.Id,
                FindId = addedComment.FindId,
                DateOfCreation = addedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = addedComment.Text
            };
        }
    }
}
