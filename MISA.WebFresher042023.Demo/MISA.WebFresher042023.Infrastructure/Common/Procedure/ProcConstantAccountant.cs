using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Common.Procedure
{
    public static class ProcConstantAccountant
    {
        /// <summary>
        /// Tên thủ tục tìm kiếm phân trang
        /// </summary>
        public const string GET_BY_RECEIPT_ID = "Proc_Accountant_GetByReceiptId";

        /// <summary>
        /// Tên thủ tục lấy hạch toán theo account id
        /// </summary>
        public const string GET_BY_ACCOUNT_ID = "Proc_Accountant_GetByAccountId";
    }
}
