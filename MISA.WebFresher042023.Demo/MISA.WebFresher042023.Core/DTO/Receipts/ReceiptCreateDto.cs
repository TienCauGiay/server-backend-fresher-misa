using MISA.WebFresher042023.Core.DTO.Accountants;
using MISA.WebFresher042023.Core.DTO.Employees.CustomValidate;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Receipts
{
    public class ReceiptCreateDto
    {
        /// <summary>
        /// Số chứng từ
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ReceiptVN), ErrorMessageResourceName = nameof(ReceiptVN.Validate_NotNull_ReceiptNumber))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ReceiptVN), ErrorMessageResourceName = nameof(ReceiptVN.Validate_MaxLength_ReceiptNumber))]
        public string ReceiptNumber { get; set; }
        /// <summary>
        /// Loại chứng từ (Phiếu thu hay chi)
        /// </summary>
        public bool ReceiptType { get; set; }
        /// <summary>
        /// Id nhà cung cấp
        /// </summary>
        [MaxLength(36, ErrorMessageResourceType = typeof(ReceiptVN), ErrorMessageResourceName = nameof(ReceiptVN.Validate_MaxLength_ProviderId))]
        public string? ProviderId { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public string? ProviderCode { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ReceiptVN), ErrorMessageResourceName = nameof(ReceiptVN.Validate_MaxLength_ProviderName))]
        public string? ProviderName { get; set; }
        /// <summary>
        /// Người nhận
        /// </summary>
        [MaxLength(100, ErrorMessageResourceType = typeof(ReceiptVN), ErrorMessageResourceName = nameof(ReceiptVN.Validate_MaxLength_ReceiveName))]
        public string? ReceiveName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ReceiptVN), ErrorMessageResourceName = nameof(ReceiptVN.Validate_MaxLength_Address))]
        public string? Address { get; set; }
        /// <summary>
        /// Lí do chi
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ReceiptVN), ErrorMessageResourceName = nameof(ReceiptVN.Validate_MaxLength_Reason))]
        public string? Reason { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ReceiptVN), ErrorMessageResourceName = nameof(ReceiptVN.Validate_MaxLength_EmployeeId))]
        public string? EmployeeId { get; set; }
        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Số lượng
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
        /// Trạng thái đã ghi sổ hay chưa
        /// </summary>
        public bool IsNoted { get; set; }
        /// <summary>
        /// Danh sách hạch toán
        /// </summary>
        public List<AccountantDto>? AccountantList { get; set; }
    }
}
