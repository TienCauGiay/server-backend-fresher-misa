using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Common.Procedure
{
    public static class ProcConstantProvider
    {
        /// <summary>
        /// Tên thủ tục lấy mã nhà cung cấp lớn nhất trong hệ thống
        /// </summary>
        public const string GET_BY_CODE_MAX = "Proc_Provider_GetCodeMax";

        /// <summary>
        /// Tên thủ tục lấy danh sách nhà cung cấp xuất excel
        /// </summary>
        public const string GET_EXPORT = "Proc_Provider_Export";

        /// <summary>
        /// Tên thủ tục tìm kiếm phân trang
        /// </summary>
        public const string GET_FILTER = "Proc_Provider_GetFilter";
    }
}
