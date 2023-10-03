using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Location
{
    public class LocationVillageDto : BaseEntity
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Mã xã/phường
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// Tên xã/phường
        /// </summary>
        public string LocationVillage { get; set; }
        /// <summary>
        /// Mã code cha (quận/huyện)
        /// </summary>
        public string? ParentCode { get; set; }
        /// <summary>
        /// Phân cấp
        /// </summary>
        public int Grade { get; set; }
    }
}
