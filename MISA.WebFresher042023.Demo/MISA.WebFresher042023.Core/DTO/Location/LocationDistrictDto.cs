using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Location
{
    public class LocationDistrictDto : BaseEntity
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Mã quận/huyện
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// Tên quận/huyện
        /// </summary>
        public string LocationDistrict { get; set; }
        /// <summary>
        /// Mã code cha (tỉnh/thành phố)
        /// </summary>
        public string? ParentCode { get; set; }
        /// <summary>
        /// Phân cấp
        /// </summary>
        public int Grade { get; set; }
    }
}
