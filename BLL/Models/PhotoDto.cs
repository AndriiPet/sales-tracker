using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int VisitId { get; set; }
        public VisitDto Visit { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class CreatePhotoDto
    {
        public int VisitId { get; set; }
    }

    public class UpdatePhotoDto
    {
        public string FilePath { get; set; }
        public int VisitId { get; set; }
    }
}
