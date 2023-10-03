using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    public class Receipt : BaseEntity
    {
        /// <summary>
        /// Id số chứng từ
        /// </summary>
        public Guid ReceiptId { get; set; }
        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string ReceiptNumber { get; set; }
        /// <summary>
        /// Loại chứng từ (thu/thi)
        /// </summary>
        public bool ReceiptType { get; set; }
        /// <summary>
        /// Id nhà cung cấp
        /// </summary>
        public string? ProviderId { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string? ProviderName { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public string? ProviderCode { get; set; }
        /// <summary>
        /// Người nhận
        /// </summary>
        public string? ReceiveName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Lí do chi
        /// </summary>
        public string? Reason { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string? EmployeeId { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Số lượng (kèm theo)
        /// </summary>
        public int QuantityAttach { get; set; }
        /// <summary>
        /// Ngày hạch toán
        /// </summary>
        public DateTime AccountingDate { get; set; }
        /// <summary>
        /// Ngày phiếu chi
        /// </summary>
        public DateTime ReceiptDate { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>
        public decimal TotalMoney { get; set; }
        /// <summary>
        /// Đã ghi sổ hay chưa
        /// </summary>
        public bool IsNoted { get; set; }
    }
}
