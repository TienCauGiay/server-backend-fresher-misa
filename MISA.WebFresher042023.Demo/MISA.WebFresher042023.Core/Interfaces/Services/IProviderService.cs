using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.DTO.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    public interface IProviderService : IBaseService<ProviderDto, ProviderCreateDto, ProviderUpdateDto>
    {
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<FilterProviderDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch);

        /// <summary>
        /// Lấy mã nhà cung cấp lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<string?> GetByCodeMaxAsync();

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<MemoryStream> ExportExcelAsync(string? textSearch);
    }
}
