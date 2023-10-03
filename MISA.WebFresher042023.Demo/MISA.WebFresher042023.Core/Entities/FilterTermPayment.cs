using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    public class FilterTermPayment
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
        /// Danh sách điều khoản thanh toán
        /// </summary>
        public List<TermPayment> Data { get; set; }

        #endregion
        /// <summary>
        /// Hàm tạo
        /// </summary>
        #region Constructor
        public FilterTermPayment()
        {
            Data = new List<TermPayment>();
        }
        #endregion
    }
}
