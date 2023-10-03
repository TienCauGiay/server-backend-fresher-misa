using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Accountants;
using MISA.WebFresher042023.Core.DTO.Accounts;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu hạch toán sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (16/08/2023)
    public class AccountantProfile : Profile
    {
        public AccountantProfile() 
        {
            CreateMap<Accountant, AccountantDto>();
            CreateMap<AccountantDto, Accountant>();
            CreateMap<AccountantCreateDto, Accountant>();
            CreateMap<AccountantUpdateDto, Accountant>();
        }
    }
}
