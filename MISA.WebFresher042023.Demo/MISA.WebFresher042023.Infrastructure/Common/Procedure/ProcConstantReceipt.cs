using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Common.Procedure
{
    public static class ProcConstantReceipt
    {
        /// <summary>
        /// Tên thủ tục lấy số phiếu chi lớn nhất trong hệ thống
        /// </summary>
        public const string GET_BY_CODE_MAX = "Proc_Receipt_GetCodeMax";

        /// <summary>
        /// Tên thủ tục tìm kiếm phân trang
        /// </summary>
        public const string GET_FILTER = "Proc_Receipt_GetFilter";

        /// <summary>
        /// Tên thủ tục lấy danh sách xuất excel
        /// </summary>
        public const string GET_EXPORT = "Proc_Receipt_Export";

        /// <summary>
        /// Tên thủ tục cập nhật trạng thái ghi sổ/bỏ ghi 1 bản ghi
        /// </summary>
        public const string PUT_NOTE = "Proc_Receipt_UpdateNote";
    }
}
