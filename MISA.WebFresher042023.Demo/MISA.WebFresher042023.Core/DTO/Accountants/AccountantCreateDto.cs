using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Accountants
{
    public class AccountantCreateDto
    {
        /// <summary>
        /// Id phiếu chi
        /// </summary>
        public Guid ReceiptId { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        public Guid AccountDebtId { get; set; }
        /// <summary>
        /// Tài khoản có
        /// </summary>
        public Guid AccountBalanceId { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal Money { get; set; }
    }
}
