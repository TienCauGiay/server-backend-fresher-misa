using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Accounts
{
    /// <summary>
    /// Lớp chuyển đổi đối tượng account, bỏ đi 1 số trường không cần thiết
    /// </summary>
    /// Created By: BNTIEN (19/07/2023)
    public class AccountReceivable
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid AccountReceivableId { get; set; }
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string AccountReceivableNumber { get; set; }
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string AccountName { get; set; }
        #endregion
    }
}
