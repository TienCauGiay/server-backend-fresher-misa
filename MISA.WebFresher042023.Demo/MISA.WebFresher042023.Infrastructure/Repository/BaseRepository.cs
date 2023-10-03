using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static Dapper.SqlMapper;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    /// <summary>
    /// class triển khai các phương thức chung truy vấn cơ sở dữ liệu
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: BNTIEN (17/06/2023)
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly IUnitOfWork _unitOfWork;

        private string className = typeof(TEntity).Name;

        /// <summary>
        /// Hàm tạo, tiêm DI
        /// </summary>
        /// <param name="configuration"></param>
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Method chung
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>danh sách entities</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            var entities = await _unitOfWork.Connection.QueryAsync<TEntity>(string.Format(ProcConstantBase.GET_ALL, className), null, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return entities;
        }

        /// <summary>
        /// Lấy thông tin entities theo code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>entities theo code</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<TEntity?> GetByCodeAsync(string code)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var entities = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(string.Format(ProcConstantBase.GET_BY_CODE, className), parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return entities;
        }

        /// <summary>
        /// Lấy thông tin entities theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entities theo id</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var entities = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(string.Format(ProcConstantBase.GET_BY_ID, className), parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return entities;
        }

        /// <summary>
        /// Thêm mới 1 entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi thêm</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<int> InsertAsync(TEntity entity)
        {
            var parameters = new DynamicParameters();
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name.Contains($"{className}Id"))
                {
                    parameters.Add($"@{className}Id", Guid.NewGuid());
                }
                else if (prop.Name.Contains("CreatedDate"))
                {
                    parameters.Add($"@CreatedDate", DateTime.Now);
                }
                else
                {
                    parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                }
            }

            var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(string.Format(ProcConstantBase.POST_OBJECT, className), parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return rowsAffected;
        }

        /// <summary>
        /// Thêm mới nhiều entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (17/06/2023) 
        public virtual async Task InsertMultipleAsync(IEnumerable<TEntity> entities)
        {
            var parameters = new DynamicParameters();
            var query = "";
            var index = 0;
            // Chuyển tên đối tượng thành tên bảng trong database
            var tableName = ConvertSnakeCase(className);
            // Tạo câu truy vấn
            foreach(var entity in entities)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null);
                query += $"INSERT INTO {tableName} (";
                query += string.Join(", ", notNullProps.Select(prop => prop.Name));
                query += ") Values (";
                query += string.Join(", ", notNullProps.Select(prop => $"@{prop.Name}_{index}"));
                query += ");";

                foreach (var prop in notNullProps)
                {
                    parameters.Add($"{prop.Name}_{index}", prop.GetValue(entity));
                }

                parameters.Add($"{tableName}_id_{index}", Guid.NewGuid());
                index++;
            }

            await _unitOfWork.Connection.ExecuteAsync(query, parameters, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// Cập nhật thông tin entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi sửa</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<int> UpdateAsync(TEntity entity, Guid id)
        {
            var parameters = new DynamicParameters();
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name.Contains("ModifiedDate"))
                {
                    parameters.Add($"@ModifiedDate", DateTime.Now);
                }
                else
                {
                    parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                }
            }
            var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(string.Format(ProcConstantBase.PUT_OBJECT, className), parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return rowsAffected;
        }

        /// <summary>
        /// Cập nhật thông tin nhiều thực thể
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task UpdateMultipleAsync(IEnumerable<TEntity> entities)
        {
            var parameters = new DynamicParameters();
            var queryBuilder = new StringBuilder();

            var tableName = ConvertSnakeCase(className);

            var index = 0;
            foreach (var entity in entities)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null && prop.Name != "Id");

                queryBuilder.Append($"UPDATE {tableName} SET ");
                queryBuilder.Append(string.Join(", ", notNullProps.Select(prop => $"{prop.Name} = @{prop.Name}_{index}")));

                // Assuming you have a key property named "Id"
                queryBuilder.Append($" WHERE {className}Id = @Id_{index};");

                foreach (var prop in notNullProps)
                {
                    parameters.Add($"{prop.Name}_{index}", prop.GetValue(entity));
                }

                // Assuming you have a key property named "Id"
                parameters.Add($"Id_{index}", entity.GetType().GetProperty($"{className}Id")?.GetValue(entity));

                index++;
            }

            await _unitOfWork.Connection.ExecuteAsync(queryBuilder.ToString(), parameters, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// Xóa thực thể theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi xóa</returns>
        /// Created By: BNTIEN (17/06/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(string.Format(ProcConstantBase.DELETE_OBJECT, className), parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return rowsAffected;
        }

        /// <summary>
        /// Xóa nhiều thực thể theo các id tương ứng
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>số hàng bị ảnh hưởng sau khi xóa</returns>
        /// Created By: BNTIEN (17/06/2023)
        public virtual async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var tableName = ConvertSnakeCase(className);
            var parameters = new DynamicParameters();
            parameters.Add("@ids", ids);
            string query = $"DELETE FROM {tableName} WHERE {className}Id IN @ids";
            var res = await _unitOfWork.Connection.ExecuteAsync(query, parameters, _unitOfWork.Transaction);

            return res;
        }
        #endregion

        #region Methods static
        /// <summary>
        /// Chuyển chữ từ PacalCase sang Camel case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (17/06/2023)
        public static string ConvertSnakeCase(string input)
        {
            // Thay thế tất cả các chữ cái viết hoa bằng "_chữ_cái_viết_thường"
            string snakeCase = Regex.Replace(input, "(?<!^)([A-Z])", "_$1").ToLower();
            return snakeCase;
        }
        #endregion
    }
}
