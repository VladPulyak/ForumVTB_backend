using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Topic;

namespace BusinessLayer.Interfaces
{
    public interface ITopicService
    {
        Task<CreateTopicResponceDto> CreateTopic(CreateTopicRequestDto requestDto);

        Task<UpdateTopicResponceDto> UpdateTopic(UpdateTopicRequestDto requestDto);

        Task DeleteTopic(DeleteTopicRequestDto requestDto);

        Task<List<TopicResponceDto>> GetUserTopics();

        Task<GetTopicCardResponceDto> GetTopicCard(GetTopicCardRequestDto requestDto);

        Task<List<UserTopicResponceDto>> GetAllTopics();

        Task<List<TopicResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto);

        Task<List<TopicResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto);

        Task<List<TopicResponceDto>> CheckFavourites(List<TopicResponceDto> responceDtos, string userId);
    }
}
