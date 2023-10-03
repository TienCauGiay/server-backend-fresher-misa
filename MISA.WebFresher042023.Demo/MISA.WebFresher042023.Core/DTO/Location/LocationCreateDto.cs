using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Location
{
    public class LocationCreateDto
    {
        /// <summary>
        /// Mã vị trí địa lí
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// Tên vị trí địa lí
        /// </summary>
        public string LocationName { get; set; }
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
