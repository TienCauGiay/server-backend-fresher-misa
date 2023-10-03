using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Location
{
    public class LocationCountryDto : BaseEntity
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Mã quốc gia
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// Tên quốc gia
        /// </summary>
        public string LocationCountry { get; set; }
        /// <summary>
        /// Mã code cha 
        /// </summary>
        public string? ParentCode { get; set; }
        /// <summary>
        /// Phân cấp
        /// </summary>
        public int Grade { get; set; }
    }
}
