﻿using MISA.WebFresher042023.Core.DTO.TermPayments;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    public interface ITermPaymentService : IBaseService<TermPaymentDto, TermPaymentCreateDto, TermPaymentUpdateDto>
    {
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        Task<FilterTermPaymentDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch);
    }
}
