using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.GroupProviders
{
    /// <summary>
    /// Lớp chuyển đổi đối tượng nhóm nhà cung cấp thêm, bỏ đi 1 số trường không cần thiết
    /// </summary>
    /// Created By: BNTIEN (27/07/2023)
    public class GroupProviderCreateDto
    {
        #region Property
        /// <summary>
        /// Mã nhóm
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public Guid ProviderId { get; set; }
        #endregion
    }
}
