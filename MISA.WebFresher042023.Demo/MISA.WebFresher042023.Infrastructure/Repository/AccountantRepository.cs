using Dapper;
using MISA.WebFresher042023.Core.DTO.Accountants;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Core.Resources;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    public class AccountantRepository : BaseRepository<Accountant>, IAccountantRepository
    {
        public AccountantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Lấy danh sách hạch toán theo id phiếu chi
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<AccountantDto>?> GetByReceiptIdAsync(Guid receiptId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReceiptId", receiptId);

            var res = await _unitOfWork.Connection.QueryAsync<AccountantDto>(ProcConstantAccountant.GET_BY_RECEIPT_ID, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return (List<AccountantDto>?)res;
        }
        
        /// <summary>
        /// Kiểm tra 1 tài khoản có nằm trong danh sách hạch toán hay không
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<Accountant?> GetByAccountIdAsync(Guid accountId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccountId", accountId);

            var res = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Accountant>(ProcConstantAccountant.GET_BY_ACCOUNT_ID, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return res;
        }

        /// <summary>
        /// Xóa nhiều hạch toán theo nhiều id phiếu chi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<int> DeleteMultipleByReceiptIdAsync(List<Guid> ids)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ids", ids);
            string query = $"DELETE FROM accountant WHERE ReceiptId IN @ids";
            var res = await _unitOfWork.Connection.ExecuteAsync(query, parameters, _unitOfWork.Transaction);
            return res;
        }
    }
}
