﻿using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Method riêng (Department)
        /// <summary>
        /// Tìm kiếm phòng ban theo tên
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<IEnumerable<Department>?> GetByName(string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@TextSearch", textSearch);

            var res = await _unitOfWork.Connection.QueryAsync<Department>(ProcConstantDepartment.GET_BY_NAME, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return res;
        }
        #endregion
    }
}
