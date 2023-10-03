using AutoMapper;
using MISA.WebFresher042023.Core.DTO.TermPayments;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu điều khoản thanh toán sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (16/08/2023)
    public class TermPaymentProfile : Profile
    {
        public TermPaymentProfile() 
        {
            CreateMap<TermPayment, TermPaymentDto>();
            CreateMap<TermPaymentCreateDto, TermPayment>();
            CreateMap<TermPaymentUpdateDto, TermPayment>();
            CreateMap<FilterTermPayment, FilterTermPaymentDto>();
        } 
    }
}
