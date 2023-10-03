using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.AccountProviders
{
    public class AccountProviderUpdateDto
    {
        /// <summary>
        /// Id tài khoản nhà cung cấp (Khóa chính)
        /// </summary>
        public Guid AccountProviderId { get; set; }
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? BankBranch { get; set; }
        /// <summary>
        /// Tỉnh/Thành phố của ngân hàng
        /// </summary>
        public string? CityOfBank { get; set; }
        /// <summary>
        /// Id nhà cung cấp
        /// </summary>
        public Guid ProviderId { get; set; }
    }
}
