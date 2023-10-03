using AutoMapper;
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
    /// class chuyển đổi dữ liệu account sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (19/07/2023)
    public class AccountProfile : Profile
    {
        public AccountProfile() 
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountCreateDto, Account>();
            CreateMap<AccountUpdateDto, Account>();
            CreateMap<FilterAccount, FilterAccountDto>();
        }
    }
}
