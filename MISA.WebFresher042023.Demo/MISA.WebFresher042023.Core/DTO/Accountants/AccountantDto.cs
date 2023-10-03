using MISA.WebFresher042023.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Accountants
{
    public class AccountantDto
    {
        /// <summary>
        /// Id hạch toán
        /// </summary>
        public Guid AccountantId { get; set; }
        /// <summary>
        /// Id phiếu chi
        /// </summary>
        public Guid ReceiptId { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Id tài khoản nợ
        /// </summary>
        public Guid AccountDebtId { get; set; }
        /// <summary>
        /// Số tài khoản tài khoản nợ
        /// </summary>
        public string? AccountDebtNumber { get; set; }
        /// <summary>
        /// Tài khoản nợ có là cha hay không
        /// </summary>
        public int? IsParentDebt { get; set; }
        /// <summary>
        /// Đối tượng tài khoản nợ
        /// </summary>
        public E_UserObjectAccount? UserObjectDebt { get; set; }
        /// <summary>
        /// Id tài khoản có
        /// </summary>
        public Guid AccountBalanceId { get; set; }
        /// <summary>
        /// Số tài khoản tài khoản có
        /// </summary>
        public string? AccountBalanceNumber { get; set; }
        /// <summary>
        /// Tài  khoản có có là cha hay không
        /// </summary>
        public int? IsParentBalance { get; set; }
        /// <summary>
        /// ĐỐi tượng tài khoản có
        /// </summary>
        public E_UserObjectAccount? UserObjectBalance { get; set; }
        public decimal Money { get; set; }
        /// <summary>
        /// Cờ để xác định trạng thái các đối tượng DeliveryAddress là thêm mới, sửa, hay xóa
        /// Quy ước: 0: Không thay đổi, 1: Thêm, 2: Sửa, 3: Xóa
        /// </summary>
        public E_Flag ? Flag { get; set; }
    }
}
