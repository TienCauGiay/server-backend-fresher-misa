using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    public class TermPayment : BaseEntity
    {
        /// <summary>
        /// Id điều khoản thanh toán
        /// </summary>
        public Guid TermPaymentId { get; set; }
        /// <summary>
        /// Mã điều khoản thanh toán
        /// </summary>
        public string TermPaymentCode { get; set; }
        /// <summary>
        /// Tên điều khoản thanh toán
        /// </summary>
        public string TermPaymentName { get; set; }
        /// <summary>
        /// Số ngày được nợ
        /// </summary>
        public int? NumberDayOwed { get; set; }
    }
}
