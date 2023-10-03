﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Groups
{
    public class GroupCreateDto
    {
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
