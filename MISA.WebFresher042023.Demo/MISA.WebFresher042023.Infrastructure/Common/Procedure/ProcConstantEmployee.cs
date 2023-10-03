using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Common.Procedure
{
    public static class ProcConstantEmployee
    {
        /// <summary>
        /// Tên thủ tục lấy mã nhân viên lớn nhất trong hệ thống
        /// </summary>
        public const string GET_BY_CODE_MAX = "Proc_Employee_GetCodeMax";

        /// <summary>
        /// Tên thủ tục tìm kiếm phân trang
        /// </summary>
        public const string GET_FILTER = "Proc_Employee_GetFilter";
    }
}
