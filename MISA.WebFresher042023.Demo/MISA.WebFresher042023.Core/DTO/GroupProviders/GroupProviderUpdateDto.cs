using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.GroupProviders
{
    /// <summary>
    /// Lớp chuyển đổi đối tượng nhóm nhà cung cấp sửa, bỏ đi 1 số trường không cần thiết
    /// </summary>
    /// Created By: BNTIEN (27/07/2023)
    public class GroupProviderUpdateDto
    {
        #region Property
        /// <summary>
        /// Mã nhóm cũ
        /// </summary>
        public Guid GroupIdOld { get; set; }
        /// <summary>
        /// Mã nhóm mới
        /// </summary>
        public Guid GroupIdNew { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public Guid ProviderId { get; set; }
        #endregion
    }
}
