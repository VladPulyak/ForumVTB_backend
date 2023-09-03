using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.FindFIles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IFindFileService
    {
        Task AddFiles(AddFindFilesRequestDto requestDto);

        Task<List<GetFindFileResponceDto>> GetFindFiles(GetFindFileRequestDto requestDto);

        Task DeleteFindFile(DeleteFindFileRequestDto requestDto);

        Task AddMissingFiles(AddMissingFindFilesRequestDto requestDto);

        Task UpdateFindFiles(UpdateFindFilesRequestDto requestDto);
    }
}
