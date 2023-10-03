using MISA.WebFresher042023.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Accounts
{
    public class AccountBalance
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid AccountBalanceId { get; set; }
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string AccountBalanceNumber { get; set; }
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Tên tiếng anh
        /// </summary>
        public string? AccountNameEnglish { get; set; }
        /// <summary>
        /// Tính chất
        /// </summary>
        public string Nature { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Explain { get; set; }
        /// <summary>
        /// Cấp
        /// </summary>
        public int? Grade { get; set; }
        /// <summary>
        /// Id cha
        /// </summary>
        public string? ParentId { get; set; }
        /// <summary>
        /// Có là cha hay không
        /// </summary>
        public BoolNumber? IsParentBalance { get; set; }
        /// <summary>
        /// Có là gốc hay không
        /// </summary>
        public BoolNumber? IsRoot { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public BoolNumber? State { get; set; }
        /// <summary>
        /// Đối tượng người dùng
        /// </summary>
        public E_UserObjectAccount? UserObjectBalance { get; set; }

        #endregion
    }
}
