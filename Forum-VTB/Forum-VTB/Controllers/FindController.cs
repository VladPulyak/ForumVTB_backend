using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.AdvertFavourites;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Find;
using BusinessLayer.Dtos.FindComments;
using BusinessLayer.Dtos.FindFavourites;
using BusinessLayer.Dtos.FindFIles;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FindController : ControllerBase
    {
        private readonly IFindService _findService;
        private readonly IFindFileService _findFileService;
        private readonly IFindCommentService _findCommentService;
        private readonly IFavouriteService _favouriteService;
        private readonly IFindFavouriteService _findFavouriteService;

        public FindController(IFindService findService, IFindCommentService findCommentService, IFavouriteService favouriteService, IFindFileService findFileService, IFindFavouriteService findFavouriteService)
        {
            _findService = findService;
            _findCommentService = findCommentService;
            _favouriteService = favouriteService;
            _findFileService = findFileService;
            _findFavouriteService = findFavouriteService;
        }


        [AllowAnonymous]
        [HttpGet("/Finds/GetFourNewestFinds")]
        public async Task<ActionResult> GetFourNewestFinds()
        {
            List<FindResponceDto> responceDtos;
            try
            {
                responceDtos = await _findService.GetFourNewestFinds();
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

        [HttpPost("/Finds/CreateFind")]
        public async Task<ActionResult> CreateFind(CreateFindRequestDto requestDto)
        {
            CreateFindResponceDto responceDto;
            try
            {
                responceDto = await _findService.CreateFind(requestDto);

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

        [HttpPut("/Finds/UpdateFind")]
        public async Task<ActionResult> UpdateFind(UpdateFindRequestDto requestDto)
        {
            UpdateFindResponceDto responceDto;
            try
            {
                responceDto = await _findService.UpdateFind(requestDto);

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

        [HttpDelete("/Finds/DeleteFind")]
        public async Task<ActionResult> DeleteFind(DeleteFindRequestDto requestDto)
        {
            try
            {
                await _findService.DeleteFind(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Find delete successfully!");
        }

        [HttpPost("/Finds/ChangeFindStatus")]
        public async Task<ActionResult> ChangeFindStatus(ChangeFindStatusRequestDto requestDto)
        {
            try
            {
                await _findService.ChangeFindStatus(requestDto);
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

        [HttpDelete("/FindFiles/DeletePhoto")]
        public async Task<ActionResult> DeletePhoto(DeleteFindFileRequestDto requestDto)
        {
            try
            {
                await _findFileService.DeleteFindFile(requestDto);
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
        [HttpPost("/Finds/GetFindCard")]
        public async Task<ActionResult> GetFindCard(GetFindCardRequestDto requestDto)
        {
            GetFindCardResponceDto responceDto;
            try
            {
                responceDto = await _findService.GetFindCard(requestDto);
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

        [HttpPost("/FindFavourites/AddToFindFavourites")]
        public async Task<ActionResult> AddToFindFavourites(AddToFindFavouritesRequestDto requestDto)
        {
            try
            {
                await _findFavouriteService.AddToFindFavourites(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Find added to favourites successfully!");
        }

        [HttpDelete("/FindFavourites/DeleteFromFindFavourites")]
        public async Task<ActionResult> DeleteFromFindFavourites(DeleteFromFindFavouritesRequestDto requestDto)
        {
            try
            {
                await _findFavouriteService.DeleteFromFindFavourites(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Find deleted from favourites successfully!");
        }

        [AllowAnonymous]
        [HttpGet("/Finds/GetAllFinds")]
        public async Task<ActionResult> GetAllFinds()
        {
            List<UserFindResponceDto> adverts;

            try
            {
                adverts = await _findService.GetAllFinds();
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
        [HttpPost("/Finds/FindBySectionName")]
        public async Task<ActionResult> FindBySectionName(FindBySectionNameRequestDto requestDto)
        {
            List<FindResponceDto> responceDtos;

            try
            {
                responceDtos = await _findService.FindBySectionName(requestDto);
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
        [HttpPost("/Finds/FindBySubsectionName")]
        public async Task<ActionResult> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            List<FindResponceDto> responceDtos;

            try
            {
                responceDtos = await _findService.FindBySubsectionName(requestDto);
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

        [HttpPost("/FindComments/CreateFindComment")]
        public async Task<ActionResult> CreateFindComment(CreateFindCommentRequestDto requestDto)
        {
            CreateFindCommentResponceDto responceDto;
            try
            {
                responceDto = await _findCommentService.CreateComment(requestDto);
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

        [HttpPut("/FindComments/UpdateFindComment")]
        public async Task<ActionResult> UpdateFindComment(UpdateFindCommentRequestDto requestDto)
        {
            UpdateFindCommentResponceDto responceDto;

            try
            {
                responceDto = await _findCommentService.UpdateComment(requestDto);
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

        [HttpDelete("/FindComments/DeleteFindComment")]
        public async Task<ActionResult> DeleteFindComment(DeleteFindCommentRequestDto requestDto)
        {
            try
            {
                await _findCommentService.DeleteComment(requestDto);
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

        [HttpPost("/FindComments/ReplyFindComment")]
        public async Task<ActionResult> ReplyFindComment(ReplyFindCommentRequestDto requestDto)
        {
            ReplyFindCommentResponceDto responceDto;

            try
            {
                responceDto = await _findCommentService.ReplyComment(requestDto);

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
