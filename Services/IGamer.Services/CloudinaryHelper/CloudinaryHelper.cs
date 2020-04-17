namespace IGamer.Services.CloudinaryHelper
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using IGamer.Common;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryHelper : ICloudinaryHelper
    {
        public async Task<string> UploadUserImageAsync(Cloudinary cloudinary, IFormFile file)
        {
            if (file == null)
            {
                var defaultUrl = GlobalConstants.DefaultUserImage;

                return defaultUrl;
            }


            var url = await this.UploadAsync(cloudinary, file);
            return url;
        }

        public async Task<string> UploadPostImageAsync(Cloudinary cloudinary, IFormFile file)
        {
            if (file == null)
            {
                var defaultUrl = GlobalConstants.DefaultPostImage;

                return defaultUrl;
            }

            var url = await this.UploadAsync(cloudinary, file);
            return url;
        }

        public async Task<string> UploadGuideImageAsync(Cloudinary cloudinary, IFormFile file)
        {
            if (file == null)
            {
                var defaultUrl = GlobalConstants.DefaultPostImage;

                return defaultUrl;
            }

            var url = await this.UploadAsync(cloudinary, file);
            return url;
        }

        public async Task<string> UploadSuggestionImageAsync(Cloudinary cloudinary, IFormFile file)
        {
            var url = await this.UploadAsync(cloudinary, file);
            return url;
        }

        private async Task<string> UploadAsync(Cloudinary cloudinary, IFormFile file)
        {
            byte[] image;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                image = stream.ToArray();
            }

            using var uploadStream = new MemoryStream(image);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, uploadStream),
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            var urlResult = uploadResult.SecureUri.AbsoluteUri.Replace(GlobalConstants.DefaultCloudinary, string.Empty);
            return urlResult;
        }
    }
}
