using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.FindComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IFindCommentService
    {
        Task<CreateFindCommentResponceDto> CreateComment(CreateFindCommentRequestDto requestDto);

        Task<UpdateFindCommentResponceDto> UpdateComment(UpdateFindCommentRequestDto requestDto);

        Task DeleteComment(DeleteFindCommentRequestDto requestDto);

        Task<List<GetFindCommentResponceDto>> GetCommentsByFindId(GetFindCommentRequestDto requestDto);

        Task<ReplyFindCommentResponceDto> ReplyComment(ReplyFindCommentRequestDto requestDto);
    }
}
