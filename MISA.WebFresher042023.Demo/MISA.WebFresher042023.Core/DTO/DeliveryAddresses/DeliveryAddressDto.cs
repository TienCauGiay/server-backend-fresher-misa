using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.DeliveryAddresses
{
    public class DeliveryAddressDto
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DeliveryAddressId { get; set; }
        /// <summary>
        /// Tên địa chỉ giao hàng
        /// </summary>
        public string DeliveryAddressName { get; set; }
        /// <summary>
        /// id nhà cung cấp
        /// </summary>
        public Guid ProviderId { get; set; }
    }
}
