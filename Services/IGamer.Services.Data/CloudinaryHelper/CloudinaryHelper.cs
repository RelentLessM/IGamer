namespace IGamer.Services.Data.CloudinaryHelper
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using IGamer.Common;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryHelper : ICloudinaryHelper
    {
        public async Task<string> UploadAsync(Cloudinary cloudinary, IFormFile file)
        {
            if (file == null)
            {
                var defaultUrl = GlobalConstants.DefaultUserImage;

                return defaultUrl;
            }

            byte[] image;

            await using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                image = stream.ToArray();
            }

            await using var uploadStream = new MemoryStream(image);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, uploadStream),
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            var url = uploadResult.Uri.AbsoluteUri;
            return url;
        }
    }
}
