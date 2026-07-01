using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public interface IFileUploader
    {
        public string UploadFile(IFormFile file,string path);
    }
}
