using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Infrastructures
{
    public interface IProviderRepository : IBaseRepository<Provider>
    {
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách nhà cung cấp</returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<FilterProvider?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch);

        /// <summary>
        /// Lấy mã nhà cung cấp lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<string?> GetByCodeMaxAsync();

        /// <summary>
        /// Lấy danh sách xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<Provider>?> GetExportAsync(string? textSearch);

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="providers"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<MemoryStream> ExportExcelAsync(List<Provider>? providers);
    }
}
