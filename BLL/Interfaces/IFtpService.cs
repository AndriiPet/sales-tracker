using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFtpService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
        Task<Stream> DownloadFileAsync(string filePath);
    }
}
