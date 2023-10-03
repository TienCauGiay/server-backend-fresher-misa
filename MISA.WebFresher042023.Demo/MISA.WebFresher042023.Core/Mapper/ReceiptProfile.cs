using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Accounts;
using MISA.WebFresher042023.Core.DTO.Receipts;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu phiếu thu/chi sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (16/08/2023)
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile() 
        {
            CreateMap<Receipt, ReceiptDto>();
            CreateMap<ReceiptCreateDto, Receipt>();
            CreateMap<ReceiptUpdateDto, Receipt>();
            CreateMap<FilterReceipt, FilterReceiptDto>();
        }
    }
}
