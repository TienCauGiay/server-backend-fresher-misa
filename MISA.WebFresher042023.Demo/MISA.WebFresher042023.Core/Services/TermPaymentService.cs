using AutoMapper;
using MISA.WebFresher042023.Core.DTO.TermPayments;
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
    public class TermPaymentService : BaseService<TermPayment, TermPaymentDto, TermPaymentCreateDto, TermPaymentUpdateDto>, ITermPaymentService
    {
        private readonly ITermPaymentRepository _termPaymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public TermPaymentService(IUnitOfWork unitOfWork, ITermPaymentRepository termPaymentRepository, IMapper mapper) : base(unitOfWork, termPaymentRepository, mapper)
        {
            _termPaymentRepository = termPaymentRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<FilterTermPaymentDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            var termPaymentss = await _termPaymentRepository.GetFilterAsync(pageSize, pageNumber, textSearch);
            if(termPaymentss?.Data != null) 
            {
                var res = _mapper.Map<FilterTermPaymentDto>(termPaymentss);
                return res;
            }
            return null;
        }
    }
}
