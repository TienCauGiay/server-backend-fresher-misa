using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MISA.WebFresher042023.Infrastructure.UnitOfWork
{
    /// <summary>
    /// Class triển khai các phương thức xử lí transaction
    /// </summary>
    /// Created By: BNTIEN (22/07/2023)
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;
        private DbTransaction _transaction = null;

        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }
        public DbConnection Connection => _connection;
        public DbTransaction Transaction => _transaction;

        /// <summary>
        /// Phương thức bắt đầu 1 giao dịch
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        public void BeginTransaction()
        {
            if (_connection.State == ConnectionState.Open)
            {
                if(_transaction == null)
                {
                    _transaction = _connection.BeginTransaction();
                }
            }
            else
            {
                _connection.Open();
                if (_transaction == null)
                {
                    _transaction = _connection.BeginTransaction();
                }

            }
        }

        /// <summary>
        /// Phương thức bất đồng bộ để bắt đầu 1 giao dịch
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (22/07/2023)
        public async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Open)
            {
                if(_transaction == null)
                {
                _transaction = await _connection.BeginTransactionAsync();
                }
            }
            else
            {
                await _connection.OpenAsync();
                if (_transaction == null)
                {
                    _transaction = await _connection.BeginTransactionAsync();
                }

            }
        }

        /// <summary>
        /// Phương thức xác nhận và lưu thay đổi trong giao dịch
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        public void Commit()
        {
            _transaction?.Commit();
        }

        /// <summary>
        /// Phương thức bất đồng bộ xác nhận và lưu thay đổi trong giao dịch
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (22/07/2023)
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }

            await DisposeAsync();
        }

        /// <summary>
        /// Phương thức giải phóng tài nguyên, đóng kết nối cơ sở dữ liệu và giải phóng giao dịch nếu có
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            _connection.Close();
        }

        /// <summary>
        /// Phương thức bất đồng bộ giải phóng tài nguyên, đóng kết nối cơ sở dữ liệu và giải phóng giao dịch nếu có
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            _transaction = null;
            await _connection.CloseAsync();
        }

        /// <summary>
        /// Phương thức để hủy bỏ các thay đổi trong giao dịch
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }

        /// <summary>
        /// Phương thức bất đồng bộ để hủy bỏ các thay đổi trong giao dịch
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();
        }
    }
}
