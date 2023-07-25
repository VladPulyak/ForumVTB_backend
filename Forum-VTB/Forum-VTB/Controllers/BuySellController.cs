using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BuySellController : ControllerBase
    {
        private readonly IAdvertService _advertService;
        private readonly ICommentService _commentService;

        public BuySellController(IAdvertService advertService, ICommentService commentService)
        {
            _advertService = advertService;
            _commentService = commentService;
        }

        [HttpGet("/Adverts/GetUserAdverts")]
        public async Task<ActionResult> GetUserAdverts()
        {
            List<UserAdvertResponceDto> responceDtos;
            try
            {
                responceDtos = await _advertService.GetUserAdverts();

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

        [HttpPost("/Adverts/CreateAdvert")]
        public async Task<ActionResult> CreateAdvert(CreateAdvertRequestDto requestDto)
        {
            CreateAdvertResponceDto responceDto;
            try
            {
                responceDto = await _advertService.CreateAdvert(requestDto);

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

        [HttpPut("/Adverts/UpdateAdvert")]
        public async Task<ActionResult> UpdateAdvert(UpdateAdvertRequestDto requestDto)
        {
            UpdateAdvertResponceDto responceDto;
            try
            {
                responceDto = await _advertService.UpdateAdvert(requestDto);

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

        [HttpDelete("/Adverts/DeleteAdvert")]
        public async Task<ActionResult> DeleteAdvert(DeleteAdvertRequestDto requestDto)
        {
            try
            {
                await _advertService.DeleteAdvert(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Advert delete successfully!");
        }

        [HttpPost("/Adverts/GetAdvertCard")]
        public async Task<ActionResult> GetAdvertCard(GetAdvertCardRequestDto requestDto)
        {
            GetAdvertCardResponceDto responceDto;
            try
            {
                responceDto = await _advertService.GetAdvertCard(requestDto);

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

        [AllowAnonymous]
        [HttpGet("/Adverts/GetAllAdverts")]
        public async Task<ActionResult> GetAllAdverts()
        {
            List<UserAdvertResponceDto> adverts;

            try
            {
                adverts = await _advertService.GetAllAdverts();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(adverts);
        }

        [HttpPost("/Comments/CreateComment")]
        public async Task<ActionResult> CreateComment(CreateCommentRequestDto requestDto)
        {
            CreateCommentResponceDto responceDto;
            try
            {
                responceDto = await _commentService.CreateComment(requestDto);
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

        [HttpPut("/Comments/UpdateComment")]
        public async Task<ActionResult> UpdateComment(UpdateCommentRequestDto requestDto)
        {
            UpdateCommentResponceDto responceDto;

            try
            {
                responceDto = await _commentService.UpdateComment(requestDto);
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

        [HttpDelete("/Comments/DeleteComment")]
        public async Task<ActionResult> DeleteComment(DeleteCommentRequestDto requestDto)
        {
            try
            {
                await _commentService.DeleteComment(requestDto);
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
    }
}