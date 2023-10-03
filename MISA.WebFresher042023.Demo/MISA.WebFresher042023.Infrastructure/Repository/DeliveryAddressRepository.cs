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
    public class DeliveryAddressRepository : BaseRepository<DeliveryAddress>, IDeliveryAddressRepository
    {
        public DeliveryAddressRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Lấy danh sách địa chỉ giao hàng theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<DeliveryAddress>?> GetByProviderIdAsync(Guid providerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@providerId", providerId);

            var res = await _unitOfWork.Connection.QueryAsync<DeliveryAddress>(ProcConstantDeliveryAddress.GET_BY_PROVIDER_ID, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return (List<DeliveryAddress>?)res;
        }

        /// <summary>
        /// Xóa nhiều địa chỉ giao hàng theo nhiều providerId
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<int> DeleteMultipleByProviderId(List<Guid> providerIds)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@providerIds", providerIds);
            var sql = $"DELETE FROM delivery_address WHERE ProviderId in @providerIds";
            var res = await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
            return res;
        }
    }
}
