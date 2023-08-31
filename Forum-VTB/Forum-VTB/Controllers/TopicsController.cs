using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Topic;
using BusinessLayer.Dtos.TopicFavourite;
using BusinessLayer.Dtos.TopicFile;
using BusinessLayer.Dtos.TopicMessage;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ITopicFileService _topicFileService;
        private readonly ITopicMessageService _topicMessageService;
        private readonly ITopicFavouriteService _topicFavouriteService;

        public TopicsController(ITopicService topicService, ITopicMessageService topicMessageService, ITopicFileService topicFileService, ITopicFavouriteService topicFavouriteService)
        {
            _topicService = topicService;
            _topicMessageService = topicMessageService;
            _topicFileService = topicFileService;
            _topicFavouriteService = topicFavouriteService;
        }


        [HttpPost("/Topics/CreateTopic")]
        public async Task<ActionResult> CreateTopic(CreateTopicRequestDto requestDto)
        {
            CreateTopicResponceDto responceDto;
            try
            {
                responceDto = await _topicService.CreateTopic(requestDto);

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

        [HttpPut("/Topics/UpdateTopic")]
        public async Task<ActionResult> UpdateTopic(UpdateTopicRequestDto requestDto)
        {
            UpdateTopicResponceDto responceDto;
            try
            {
                responceDto = await _topicService.UpdateTopic(requestDto);

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

        [HttpDelete("/Topics/DeleteTopic")]
        public async Task<ActionResult> DeleteTopic(DeleteTopicRequestDto requestDto)
        {
            try
            {
                await _topicService.DeleteTopic(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Topic delete successfully!");
        }

        [HttpDelete("/TopicFiles/DeletePhoto")]
        public async Task<ActionResult> DeletePhoto(DeleteTopicFileRequestDto requestDto)
        {
            try
            {
                await _topicFileService.DeleteTopicFile(requestDto);
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
        [HttpPost("/Topics/GetTopicCard")]
        public async Task<ActionResult> GetTopicCard(GetTopicCardRequestDto requestDto)
        {
            GetTopicCardResponceDto responceDto;
            try
            {
                responceDto = await _topicService.GetTopicCard(requestDto);
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

        [HttpPost("/TopicFavourites/AddToTopicFavourites")]
        public async Task<ActionResult> AddToTopicFavourites(AddToTopicFavouritesRequestDto requestDto)
        {
            try
            {
                await _topicFavouriteService.AddToTopicFavourites(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Topic added to favourites successfully!");
        }

        [HttpDelete("/TopicFavourites/DeleteFromTopicFavourites")]
        public async Task<ActionResult> DeleteFromTopicFavourites(DeleteFromTopicFavouritesRequestDto requestDto)
        {
            try
            {
                await _topicFavouriteService.DeleteFromTopicFavourites(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Topic deleted from favourites successfully!");
        }

        [AllowAnonymous]
        [HttpGet("/Topics/GetAllTopics")]
        public async Task<ActionResult> GetAllTopics()
        {
            List<UserTopicResponceDto> responceDtos;

            try
            {
                responceDtos = await _topicService.GetAllTopics();
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
        [HttpPost("/Topics/FindBySectionName")]
        public async Task<ActionResult> FindBySectionName(FindBySectionNameRequestDto requestDto)
        {
            List<TopicResponceDto> responceDtos;

            try
            {
                responceDtos = await _topicService.FindBySectionName(requestDto);
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
        [HttpPost("/Topics/FindBySubsectionName")]
        public async Task<ActionResult> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            List<TopicResponceDto> responceDtos;

            try
            {
                responceDtos = await _topicService.FindBySubsectionName(requestDto);
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

        [HttpPost("/TopicMessages/CreateTopicMessage")]
        public async Task<ActionResult> CreateTopicMessage(CreateTopicMessageRequestDto requestDto)
        {
            CreateTopicMessageResponceDto responceDto;
            try
            {
                responceDto = await _topicMessageService.CreateTopicMessage(requestDto);
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

        [HttpPut("/TopicMessages/UpdateTopicMessage")]
        public async Task<ActionResult> UpdateTopicMessage(UpdateTopicMessageRequestDto requestDto)
        {
            UpdateTopicMessageResponceDto responceDto;

            try
            {
                responceDto = await _topicMessageService.UpdateTopicMessage(requestDto);
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

        [HttpDelete("/TopicMessages/DeleteTopicMessage")]
        public async Task<ActionResult> DeleteTopicMessage(DeleteTopicMessageRequestDto requestDto)
        {
            try
            {
                await _topicMessageService.DeleteTopicMessage(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Comment delete successfully!");
        }

        [HttpPost("/TopicMessages/ReplyTopicMessage")]
        public async Task<ActionResult> ReplyTopicMessage(ReplyTopicMessageRequestDto requestDto)
        {
            ReplyTopicMessageResponceDto responceDto;

            try
            {
                responceDto = await _topicMessageService.ReplyTopicMessage(requestDto);

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

    }
}
