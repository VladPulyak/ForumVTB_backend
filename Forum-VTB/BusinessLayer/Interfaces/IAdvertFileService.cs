using BusinessLayer.Dtos.AdvertFiles;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAdvertFileService
    {
        Task AddFiles(AddAdvertFilesRequestDto requestDto);

        Task<List<GetAdvertFileResponceDto>> GetAdvertFiles(GetAdvertFileRequestDto requestDto);

        Task DeleteAdvertFile(DeleteAdvertFileRequestDto requestDto);

        Task AddMissingFiles(AddMissingFilesRequestDto requestDto);
    }
}
