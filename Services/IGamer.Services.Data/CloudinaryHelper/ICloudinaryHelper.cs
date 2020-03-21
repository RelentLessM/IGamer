namespace IGamer.Services.Data.CloudinaryHelper
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryHelper
    {
        Task<string> UploadAsync(Cloudinary cloudinary, IFormFile file);
    }
}
