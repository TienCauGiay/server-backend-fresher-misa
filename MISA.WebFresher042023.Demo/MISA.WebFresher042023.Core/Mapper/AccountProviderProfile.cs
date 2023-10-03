using AutoMapper;
using MISA.WebFresher042023.Core.DTO.AccountProviders;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu tài khoản ngân hàng nhà cung cấp sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (16/08/2023)
    public class AccountProviderProfile : Profile
    {
        public AccountProviderProfile() 
        {
            CreateMap<AccountProvider, AccountProviderDto>();
            CreateMap<AccountProviderCreateDto, AccountProvider>();
            CreateMap<AccountProviderUpdateDto, AccountProvider>();
        }
    }
}
