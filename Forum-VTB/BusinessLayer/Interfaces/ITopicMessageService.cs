using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.TopicMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITopicMessageService
    {
        Task<CreateTopicMessageResponceDto> CreateTopicMessage(CreateTopicMessageRequestDto requestDto);

        Task<UpdateTopicMessageResponceDto> UpdateTopicMessage(UpdateTopicMessageRequestDto requestDto);

        Task DeleteTopicMessage(DeleteTopicMessageRequestDto requestDto);

        Task<List<GetTopicMessageResponceDto>> GetTopicMessagesByTopicId(GetTopicMessageRequestDto requestDto);

        Task<ReplyTopicMessageResponceDto> ReplyTopicMessage(ReplyTopicMessageRequestDto requestDto);
    }
}
