using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    public class Accountant : BaseEntity
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
