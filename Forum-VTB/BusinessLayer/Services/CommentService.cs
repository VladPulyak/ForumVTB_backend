﻿using AutoMapper;
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

        public async Task<CreateAdvertCommentResponceDto> CreateComment(CreateAdvertCommentRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var comment = _mapper.Map<AdvertComment>(requestDto);
            comment.UserId = user.Id;
            comment.Id = Guid.NewGuid().ToString();
            comment.AdvertId = requestDto.AdvertId;
            comment.DateOfCreation = DateTime.UtcNow;
            var addedComment = await _advertCommentRepository.Add(comment);
            await _advertCommentRepository.Save();
            return new CreateAdvertCommentResponceDto
            {
                CommentId = addedComment.Id,
                AdvertId = addedComment.AdvertId,
                DateOfCreation = addedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = addedComment.Text
            };
        }

        public async Task<UpdateAdvertCommentResponceDto> UpdateComment(UpdateAdvertCommentRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var comment = await _advertCommentRepository.GetById(requestDto.CommentId);
            comment.Text = requestDto.Text;
            comment.DateOfCreation = DateTime.UtcNow;
            var updatedComment = _advertCommentRepository.Update(comment);
            await _advertCommentRepository.Save();
            return new UpdateAdvertCommentResponceDto
            {
                CommentId = updatedComment.Id,
                AdvertId = updatedComment.AdvertId,
                DateOfCreation = updatedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = updatedComment.Text
            };
        }

        public async Task DeleteComment(DeleteAdvertCommentRequestDto requestDto)
        {
            await _advertCommentRepository.Delete(requestDto.CommentId);
            await _advertCommentRepository.Save();
        }

        public async Task<List<GetAdvertCommentResponceDto>> GetCommentsByAdvertId(GetAdvertCommentRequestDto requestDto)
        {
            var comments = await _advertCommentRepository.GetByAdvertId(requestDto.AdvertId);
            var commentResponceDtos = _mapper.Map<List<GetAdvertCommentResponceDto>>(comments.OrderBy(q => q.DateOfCreation));
            return commentResponceDtos;
        }

        public async Task<ReplyAdvertCommentResponceDto> ReplyComment(ReplyAdvertCommentRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var parentCommnent = await _advertCommentRepository.GetById(requestDto.ParentCommentId);
            var comment = _mapper.Map<AdvertComment>(requestDto);
            comment.UserId = user.Id;
            comment.Id = Guid.NewGuid().ToString();
            comment.ParentCommentId = parentCommnent.Id;
            comment.ParentComment = parentCommnent;
            comment.DateOfCreation = DateTime.UtcNow;
            var addedComment = await _advertCommentRepository.Add(comment);
            await _advertCommentRepository.Save();
            return new ReplyAdvertCommentResponceDto
            {
                CommentId = addedComment.Id,
                AdvertId = addedComment.AdvertId,
                DateOfCreation = addedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = addedComment.Text
            };
        }
    }
}
