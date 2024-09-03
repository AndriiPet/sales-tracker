using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Trip : BaseEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WorkRegionId { get; set; }
        public WorkRegion WorkRegion { get; set; }
        public ICollection<Visit> Visits { get; set; }  
    }
}
