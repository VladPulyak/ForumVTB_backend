using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.AdvertFavourites;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json.Serialization;
using BusinessLayer.Dtos.AdvertFiles;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BuySellController : ControllerBase
    {
        private readonly IAdvertService _advertService;
        private readonly IAdvertFileService _advertFileService;
        private readonly ICommentService _commentService;
        private readonly IAdvertFavouriteService _advertFavouriteService;

        public BuySellController(IAdvertService advertService, ICommentService commentService, IAdvertFileService advertFileService, IAdvertFavouriteService advertFavouriteService)
        {
            _advertService = advertService;
            _commentService = commentService;
            _advertFileService = advertFileService;
            _advertFavouriteService = advertFavouriteService;
        }


        [AllowAnonymous]
        [HttpGet("/Adverts/GetFourNewestAdverts")]
        public async Task<ActionResult> GetFourNewestAdverts()
        {
            List<AdvertResponceDto> responceDtos;
            try
            {
                responceDtos = await _advertService.GetFourNewestAdverts();
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

        [HttpPost("/Adverts/ChangeAdvertStatus")]
        public async Task<ActionResult> ChangeAdvertStatus(ChangeAdvertStatusRequestDto requestDto)
        {
            try
            {
                await _advertService.ChangeAdvertStatus(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok("Status changed successfully!");
        }

        [HttpDelete("/AdvertFiles/DeletePhoto")]
        public async Task<ActionResult> DeletePhoto(DeleteAdvertFileRequestDto requestDto)
        {
            try
            {
                await _advertFileService.DeleteAdvertFile(requestDto);
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

        [HttpPost("/Favourites/AddToFavourites")]
        public async Task<ActionResult> AddToFavourites(AddToAdvertFavouritesRequestDto requestDto)
        {
            try
            {
                await _advertFavouriteService.AddToAdvertFavourites(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Advert added to favourites successfully!");
        }

        [HttpDelete("/Favourites/DeleteFromFavourites")]
        public async Task<ActionResult> DeleteFromFavourites(DeleteFromAdvertFavouritesRequestDto requestDto)
        {
            try
            {
                await _advertFavouriteService.DeleteFromAdvertFavourites(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Advert deleted from favourites successfully!");
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

        [AllowAnonymous]
        [HttpPost("/Adverts/FindBySectionName")]
        public async Task<ActionResult> FindBySectionName(FindBySectionNameRequestDto requestDto)
        {
            List<AdvertResponceDto> responceDtos;

            try
            {
                responceDtos = await _advertService.FindBySectionName(requestDto);
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
        [HttpPost("/Adverts/FindBySubsectionName")]
        public async Task<ActionResult> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            List<AdvertResponceDto> responceDtos;

            try
            {
                responceDtos = await _advertService.FindBySubsectionName(requestDto);
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

        [HttpPost("/Comments/CreateComment")]
        public async Task<ActionResult> CreateComment(CreateAdvertCommentRequestDto requestDto)
        {
            CreateAdvertCommentResponceDto responceDto;
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
        public async Task<ActionResult> UpdateComment(UpdateAdvertCommentRequestDto requestDto)
        {
            UpdateAdvertCommentResponceDto responceDto;

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
        public async Task<ActionResult> DeleteComment(DeleteAdvertCommentRequestDto requestDto)
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

        [HttpPost("/Comments/ReplyComment")]
        public async Task<ActionResult> ReplyComment(ReplyAdvertCommentRequestDto requestDto)
        {
            ReplyAdvertCommentResponceDto responceDto;

            try
            {
                responceDto = await _commentService.ReplyComment(requestDto);

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