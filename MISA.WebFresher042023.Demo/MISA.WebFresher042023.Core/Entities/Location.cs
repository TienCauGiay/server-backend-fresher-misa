using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    public class Location : BaseEntity
    {
        public Guid LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string? ParentCode { get; set; }
        public int Grade { get; set; }
    }
}
