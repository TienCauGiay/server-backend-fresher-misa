using Dapper;
using MISA.WebFresher042023.Core.DTO.Providers;
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
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    /// <summary>
    /// class triển khai các phương thức của thực thể nhóm nhà cung cấp truy vấn cơ sở dữ liệu
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: BNTIEN (27/07/2023)
    public class GroupProviderRepository : BaseRepository<GroupProvider>, IGroupProviderRepository
    {
        /// <summary>
        /// Hàm tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        public GroupProviderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Hàm cập nhật thông tin 1 nhóm nhà cung cấp
        /// </summary>
        /// <param name="groupProvider"></param>
        /// <param name="providerId"></param>
        /// <param name="groupId"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi cập nhật</returns>
        public async Task<int> UpdateAsync(GroupProvider groupProvider, Guid providerId, Guid groupId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@GroupIdOld", groupId);
            foreach (var prop in groupProvider.GetType().GetProperties())
            {
                if (prop.Name.Contains("ModifiedDate"))
                {
                    parameters.Add($"@ModifiedDate", DateTime.Now);
                }
                else
                {
                    parameters.Add($"@{prop.Name}", prop.GetValue(groupProvider));
                }
            }
            var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(ProcConstantGroupProvider.PUT_GROUP_PROVIDER, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return rowsAffected;
        }

        /// <summary>
        /// Lấy danh sách nhóm nhà cung cấp theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (27/07/2023)
        public async Task<List<GroupProvider>?> GetByProviderIdAsync(Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var res = await _unitOfWork.Connection.QueryAsync<GroupProvider>(ProcConstantGroupProvider.GET_BY_PROVIDER_ID, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return (List<GroupProvider>?)res;
        }

        /// <summary>
        /// Lấy danh sách nhóm nhà cung cấp theo id nhà cung cấp và list id group
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (27/07/2023)
        public async Task<List<GroupProvider>?> GetExistAsync(Guid providerId, List<Guid>? groupIds)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ProviderId", providerId);
            parameters.Add("GroupIds", groupIds?.ToArray());
            var sql = @"SELECT * FROM group_provider WHERE ProviderId = @ProviderId AND GroupId IN @GroupIds";
            var groupProviderExist = await _unitOfWork.Connection.QueryAsync<GroupProvider>(sql, parameters, _unitOfWork.Transaction);
            return (List<GroupProvider>?)groupProviderExist;
        }

        /// <summary>
        /// Xóa các nhóm nhà cung cấp theo id nhà cung cấp và list group id
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (27/07/2023)
        public async Task DeleteNotExistAsync(Guid providerId, List<Guid>? groupIds)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ProviderId", providerId);
            parameters.Add("GroupIds", groupIds?.ToArray());
            var sql = @"DELETE FROM group_provider WHERE ProviderId = @ProviderId AND GroupId NOT IN @GroupIds";
            await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
        }

        /// <summary>
        /// Xóa nhiều nhóm nhà cung cấp theo nhiều providerId
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ids", ids);
            var sql = @"DELETE FROM group_provider WHERE ProviderId in @ids";
            var res = await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
            return res;
        }
    }
}
