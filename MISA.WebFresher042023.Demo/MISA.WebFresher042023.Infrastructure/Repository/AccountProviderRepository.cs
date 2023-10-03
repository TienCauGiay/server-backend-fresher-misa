using Dapper;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    public class AccountProviderRepository : BaseRepository<AccountProvider>, IAccountProviderRepository
    {
        public AccountProviderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Lấy danh sách tài khoản ngân hàng theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<AccountProvider>?> GetByProviderIdAsync(Guid providerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@providerId", providerId);

            var res = await _unitOfWork.Connection.QueryAsync<AccountProvider>(ProcConstantAccountProvider.GET_BY_PROVIDER_ID, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return (List<AccountProvider>?)res;
        }

        /// <summary>
        /// Xóa nhiều tài khoản ngân hàng theo nhiều providerId
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<int> DeleteMultipleByProviderId(List<Guid> providerIds)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@providerIds", providerIds);
            var sql = $"DELETE FROM account_provider WHERE ProviderId in @providerIds";
            var res = await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
            return res;
        }
    }
}
