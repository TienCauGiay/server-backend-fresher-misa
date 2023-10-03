using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Common.Procedure
{
    public static class ProcConstantAccount
    {
        /// <summary>
        /// Tên thủ tục lấy hạch toán theo receiptid
        /// </summary>
        public const string GET_FILTER = "Proc_Account_GetFilter";

        /// <summary>
        /// Tên thủ tục đếm xem 1 node cha có bao nhiêu con
        /// </summary>
        public const string GET_COUNT_CHILDREN = "Proc_Account_CountChildren";

        /// <summary>
        /// Tên thủ tục lấy danh sách các con của tài khoản có số tài khoản là tham số truyền vào
        /// </summary>
        public const string GET_ALL_CHILDREN = "Proc_Account_GetChildren";

        /// <summary>
        /// Tên thủ tục lấy danh sách tài khoản tổng hợp
        /// </summary>
        public const string GET_BY_SEARCH_FILTER = "Proc_Account_GetBySearchFilter";

        /// <summary>
        /// Tên thủ tục lấy danh sách tài khoản công nợ phải thu
        /// </summary>
        public const string GET_RECEIVABLE = "Proc_Account_GetReceivable";

        /// <summary>
        /// Tên thủ tục lấy danh sách tài khoản công nợ phải trả
        /// </summary>
        public const string GET_PAYABLE = "Proc_Account_GetPayable";

        /// <summary>
        /// Tên thủ tục lấy danh sách tài khoản nợ
        /// </summary>
        public const string GET_DEBT = "Proc_Account_GetDebt";

        /// <summary>
        /// Tên thủ tục lấy danh sách tài khoản có
        /// </summary>
        public const string GET_BALANCE = "Proc_Account_GetBalance";

        /// <summary>
        /// Tên thủ tục lấy tất cả các tài khoản theo giá trị tìm kiếm
        /// </summary>
        public const string GET_BY_SEARCH = "Proc_Account_GetBySearch";

        /// <summary>
        /// Tên thủ tục cập nhật trạng thái cho các tài khoản
        /// </summary>
        public const string PUT_STATE = "Proc_Account_UpdateState";
    }
}
