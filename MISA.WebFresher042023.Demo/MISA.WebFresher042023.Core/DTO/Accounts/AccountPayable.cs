using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Accounts
{
    public class AccountPayable
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid AccountPayableId { get; set; }
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string AccountPayableNumber { get; set; }
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string AccountName { get; set; }
        #endregion
    }
}
