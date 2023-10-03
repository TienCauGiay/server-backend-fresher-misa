using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.UnitOfWork
{
    /// <summary>
    /// Interface xử lí transaction
    /// </summary>
    /// Created By: BNTIEN (22/07/2023)
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Đối tượng kết nối cơ sở dữ liệu
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        DbConnection Connection { get; }

        /// <summary>
        /// Đối tượng thực hiện các giao dịch trong database
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        DbTransaction Transaction { get; }

        /// <summary>
        /// Phương thức bắt đầu 1 giao dịch
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        void BeginTransaction();

        /// <summary>
        /// Phương thức bất đồng bộ để bắt đầu 1 giao dịch
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (22/07/2023)
        Task BeginTransactionAsync();

        /// <summary>
        /// Phương thức xác nhận và lưu thay đổi trong giao dịch
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        void Commit();

        /// <summary>
        /// Phương thức bất đồng bộ xác nhận và lưu thay đổi trong giao dịch
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (22/07/2023)
        Task CommitAsync();

        /// <summary>
        /// Phương thức để hủy bỏ các thay đổi trong giao dịch
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        void Rollback();

        /// <summary>
        /// Phương thức bất đồng bộ để hủy bỏ các thay đổi trong giao dịch
        /// </summary>
        /// Created By: BNTIEN (22/07/2023)
        Task RollbackAsync();
    }
}
