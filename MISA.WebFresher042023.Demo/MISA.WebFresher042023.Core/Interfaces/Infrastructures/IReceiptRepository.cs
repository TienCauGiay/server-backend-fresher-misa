using MISA.WebFresher042023.Core.DTO.Receipts;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Infrastructures
{
    public interface IReceiptRepository : IBaseRepository<Receipt>
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
        Task<FilterReceipt?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch, bool? keyFilter);

        /// <summary>
        /// Lấy số phiếu chi lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<string?> GetByCodeMaxAsync();

        /// <summary> 
        /// Cập nhật trạng thái ghi sổ/bỏ ghi 1 bản ghi
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<int> UpdateNoteAsync(Receipt receipt);

        /// <summary>
        /// Cập nhật trạng thái ghi sổ/bỏ ghi nhiều bản ghi
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="typeUpdate"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<int> UpdateNoteMultipleAsync(List<Guid> ids, bool typeUpdate);

        /// <summary>
        /// Lấy danh sách phiếu chi đang ở trạng thái ghi sổ/bỏ ghi theo list receipt id (để thực hiên cập nhật trạng thái ghi sổ/bỏ ghi nhiều phiếu chi)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<List<Receipt>?> GetSatisfiedAsync(List<Guid> ids, ReceiptNote note);

        /// <summary>
        /// Lấy danh sách xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<List<Receipt>?> GetExportAsync(string? textSearch, bool? keyFilter);

        /// <summary>
        /// Xuất file excel
        /// </summary>
        /// <param name="receipts"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<MemoryStream> ExportExcelAsync(List<Receipt>? receipts);
    }
}
