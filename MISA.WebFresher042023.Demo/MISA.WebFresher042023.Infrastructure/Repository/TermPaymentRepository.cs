using Dapper;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    public class TermPaymentRepository : BaseRepository<TermPayment>, ITermPaymentRepository
    {
        public TermPaymentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<FilterTermPayment?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@TextSearch", textSearch);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Connection.QueryAsync<TermPayment>(ProcConstantTermPayment.GET_FILTER, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            var totalRecord = parameters.Get<int>("@TotalRecord");

            var currentPageRecords = 0;
            if (pageNumber < Math.Ceiling((decimal)totalRecord / pageSize))
            {
                currentPageRecords = pageSize;
            }
            else if (pageNumber == Math.Ceiling((decimal)totalRecord / pageSize))
            {
                currentPageRecords = totalRecord - (pageNumber - 1) * pageSize;
            }

            return new FilterTermPayment
            {
                TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                TotalRecord = totalRecord,
                CurrentPage = pageNumber,
                CurrentPageRecords = currentPageRecords,
                Data = result.ToList()
            };
        }
    }
}
