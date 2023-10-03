using MISA.WebFresher042023.Core.DTO.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.GroupProviders
{
    /// <summary>
    /// Thực thể nhóm nhà cung cấp tìm kiếm phân trang DTO
    /// </summary>
    /// Created By: BNTIEN (27/07/2023)
    public class FilterGroupProviderDto
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
        /// Danh sách nhóm nhà cung cấp DTO
        /// </summary>
        public List<GroupProviderDto> Data { get; set; }
        #endregion

        #region Constructor
        public FilterGroupProviderDto()
        {
            Data = new List<GroupProviderDto>();
        }
        #endregion
    }
}
