using MISA.WebFresher042023.Core.DTO.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Receipts
{
    public class FilterReceiptDto
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
        /// Danh sách phiếu thu/chi DTO
        /// </summary>
        public List<ReceiptDto> Data { get; set; }
        #endregion

        #region Constructor
        public FilterReceiptDto()
        {
            Data = new List<ReceiptDto>();
        }
        #endregion
    }
}
