using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Accountants;
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
    public class AccountantService : BaseService<Accountant, AccountantDto, AccountantCreateDto, AccountantUpdateDto>, IAccountantService
    {
        private readonly IAccountantRepository _accountantRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public AccountantService(IUnitOfWork unitOfWork, IAccountantRepository accountantRepository, IMapper mapper) : base(unitOfWork, accountantRepository, mapper)
        {
            _unitOfWork= unitOfWork;
            _accountantRepository = accountantRepository;
        }

        /// <summary>
        /// Lấy danh sách hạch toán theo id phiếu chi
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<AccountantDto>?> GetByReceiptIdAsync(Guid receiptId)
        {
            var accountants = await _accountantRepository.GetByReceiptIdAsync(receiptId);
            return accountants; 
        }
    }
}
