﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{

    public class Group : BaseEntity
    {
        /// <summary>
        /// id nhóm
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// Mã nhóm
        /// </summary>
        public string GroupCode { get; set; }
        /// <summary>
        /// Tên nhóm
        /// </summary>
        public string GroupName { get; set; }
    }
}
