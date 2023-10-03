using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    /// <summary>
    /// Thực thể nhóm nhà cung cấp
    /// </summary>
    /// Created By: BNTIEN (27/07/2023)
    public class GroupProvider : BaseEntity
    {
        /// <summary>
        /// Khai báo các Property của thực thể nhóm nhà cung cấp
        /// </summary>
        #region Property riêng

        /// <summary>
        /// Id Nhóm
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// Id nhà cung cấp
        /// </summary>
        public Guid ProviderId { get; set; }
        /// <summary>
        /// Mã nhóm
        /// </summary>
        public string GroupCode { get; set; }
        #endregion
    }
}
