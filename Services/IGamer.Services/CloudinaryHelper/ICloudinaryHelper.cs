namespace IGamer.Services.CloudinaryHelper
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryHelper
    {
        Task<string> UploadUserImageAsync(Cloudinary cloudinary, IFormFile file);

        Task<string> UploadPostImageAsync(Cloudinary cloudinary, IFormFile file);
    }
}
