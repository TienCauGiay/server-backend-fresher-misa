using MISA.WebFresher042023.Core.DTO.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    public interface IReceiptService : IBaseService<ReceiptDto, ReceiptCreateDto, ReceiptUpdateDto>
    {
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<FilterReceiptDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch, bool? keyFilter);

        /// <summary>
        /// Lấy mã phiếu chi lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<string?> GetByCodeMaxAsync();

        /// <summary>
        /// Cập nhật trạng thái ghi sổ/ bỏ ghi 1 phiếu chi
        /// </summary>
        /// <param name="receiptUpdateDto"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<int> UpdateNoteAsync(ReceiptUpdateDto receiptUpdateDto);

        /// <summary>
        /// Cập nhật trạng thái ghi sổ/bỏ ghi nhiều phiếu chi
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="typeUpdate"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<int> UpdateNoteMultipleAsync(List<Guid> ids, bool typeUpdate);

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<MemoryStream> ExportExcelAsync(string? textSearch, bool? keyFilter);
    }
}
