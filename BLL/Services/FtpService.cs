using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FtpService : IFtpService
    {
        private readonly string _ftpServer;
        private readonly string _ftpUsername;
        private readonly string _ftpPassword;

        public FtpService(IConfiguration configuration)
        {
            _ftpServer = configuration["Ftp:Server"];
            _ftpUsername = configuration["Ftp:Username"];
            _ftpPassword = configuration["Ftp:Password"];
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            string ftpFilePath = $"ftp://{_ftpServer}/{fileName}";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFilePath);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(_ftpUsername, _ftpPassword);

            using (Stream ftpStream = await request.GetRequestStreamAsync())
            {
                await fileStream.CopyToAsync(ftpStream);
            }

            return ftpFilePath;
        }

        public async Task<Stream> DownloadFileAsync(string filePath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(_ftpUsername, _ftpPassword);

            FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
            return response.GetResponseStream();
        }
    }
}
