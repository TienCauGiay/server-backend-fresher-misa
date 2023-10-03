using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    /// <summary>
    /// Thực thể tài khoản hệ thống tìm kiếm phân trang
    /// </summary>
    /// Created By: BNTIEN (19/07/2023)
    public class FilterAccount
    {
        #region Property
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Số bản ghi trên trang hiện tại
        /// </summary>
        public int CurrentPageRecords { get; set; }
        /// <summary>
        /// Danh sách tài khoản
        /// </summary>
        public List<Account> Data { get; set; }
        #endregion

        /// <summary>
        /// Hàm tạo
        /// </summary>
        #region Constructor
        public FilterAccount() 
        {
            Data = new List<Account>();
        }
        #endregion
    }
}
