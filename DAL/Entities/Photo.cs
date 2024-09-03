using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Photo : BaseEntity
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public Visit Visit { get; set; }
        public int VisitId { get; set; }
    }
}
