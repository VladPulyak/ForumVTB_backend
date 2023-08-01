using BusinessLayer.Dtos.AdvertComments;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICommentService
    {
        Task<CreateCommentResponceDto> CreateComment(CreateCommentRequestDto requestDto);

        Task<UpdateCommentResponceDto> UpdateComment(UpdateCommentRequestDto requestDto);

        Task DeleteComment(DeleteCommentRequestDto requestDto);

        Task<List<GetCommentResponceDto>> GetCommentsByAdvertId(GetCommentsRequestDto requestDto);

        Task<ReplyCommentResponceDto> ReplyComment(ReplyCommentRequestDto requestDto);
    }
}
