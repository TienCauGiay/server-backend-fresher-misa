using MISA.WebFresher042023.Core.DTO.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Groups
{
    public class FilterGroupDto
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
        /// Danh sách group DTO
        /// </summary>
        public List<GroupDto> Data { get; set; }
        #endregion

        #region Constructor
        public FilterGroupDto()
        {
            Data = new List<GroupDto>();
        }
        #endregion
    }
}
