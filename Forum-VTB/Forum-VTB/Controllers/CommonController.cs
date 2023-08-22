using BusinessLayer.Dtos.Common;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [ApiController]
    [Authorize]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [AllowAnonymous]
        [HttpGet("/SearchByKeyPhrase")]
        public async Task<ActionResult> SearchByKeyPhrase(string phrase)
        {
            SearchByKeyPhraseResponceDto responceDto;
            try
            {
                responceDto = await _commonService.GetByKeyPhrase(phrase);
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

        [HttpGet("/GetUserAdverts")]
        public async Task<ActionResult> GetAllUserAdverts()
        {
            GetAllUserAdvertsResponceDto responceDto;
            try
            {
                responceDto = await _commonService.GetUserAdverts();
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
