using MISA.WebFresher042023.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    public class DeliveryAddress : BaseEntity
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
        /// <summary>
        /// Cờ để xác định trạng thái các đối tượng DeliveryAddress là thêm mới, sửa, hay xóa
        /// Quy ước: 0: Không thay đổi, 1: Thêm, 2: Sửa, 3: Xóa
        /// </summary>
        public E_Flag? Flag { get; set; }
    }
}
