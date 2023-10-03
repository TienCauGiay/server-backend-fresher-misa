using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Location
{
    public class LocationCityDto : BaseEntity
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Mã tỉnh/thành phố
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// Tên tỉnh/thành phố
        /// </summary>
        public string LocationCity { get; set; }
        /// <summary>
        /// Mã code cha (quốc gia)
        /// </summary>
        public string? ParentCode { get; set; }
        /// <summary>
        /// Phân cấp
        /// </summary>
        public int Grade { get; set; }
    }
}
