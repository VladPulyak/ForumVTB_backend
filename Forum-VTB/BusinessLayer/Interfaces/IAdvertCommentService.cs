using BusinessLayer.Dtos.AdvertComments;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAdvertCommentService
    {
        Task<CreateAdvertCommentResponceDto> CreateComment(CreateAdvertCommentRequestDto requestDto);

        Task<UpdateAdvertCommentResponceDto> UpdateComment(UpdateAdvertCommentRequestDto requestDto);

        Task DeleteComment(DeleteAdvertCommentRequestDto requestDto);

        Task<List<GetAdvertCommentResponceDto>> GetCommentsByAdvertId(GetAdvertCommentRequestDto requestDto);

        Task<ReplyAdvertCommentResponceDto> ReplyComment(ReplyAdvertCommentRequestDto requestDto);
    }
}
