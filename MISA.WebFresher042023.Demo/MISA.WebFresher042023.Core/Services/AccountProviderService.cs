using AutoMapper;
using MISA.WebFresher042023.Core.DTO.AccountProviders;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Services
{
    public class AccountProviderService : BaseService<AccountProvider, AccountProviderDto, AccountProviderCreateDto, AccountProviderUpdateDto>, IAccountProviderService
    {
        private readonly IAccountProviderRepository _accountProviderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountProviderService(IUnitOfWork unitOfWork, IAccountProviderRepository accountProviderRepository, IMapper mapper) : base(unitOfWork, accountProviderRepository, mapper)
        {
            _accountProviderRepository = accountProviderRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lấy danh sách tài khoản ngân hàng theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<AccountProviderDto>?> GetByProviderIdAsync(Guid providerId)
        {
            var accountProviders = await _accountProviderRepository.GetByProviderIdAsync(providerId);
            if (accountProviders != null)
            {
                var res = _mapper.Map<List<AccountProviderDto>>(accountProviders);
                return res;
            }
            return null;
        }
    }
}
