using MISA.WebFresher042023.Core.DTO.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    public interface IGroupService : IBaseService<GroupDto, GroupCreateDto, GroupUpdateDto>
    {
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<FilterGroupDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch);
    }
}
