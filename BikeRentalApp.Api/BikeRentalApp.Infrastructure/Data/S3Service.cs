using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace BikeRentalApp.Infrastructure.Data {
    public class S3Service {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration config) {
            _s3Client = new AmazonS3Client(config["AWS:AccessKeyId"], config["AWS:SecretAccessKey"], Amazon.RegionEndpoint.GetBySystemName(config["AWS:Region"]));
            _bucketName = config["AWS:BucketName"];
        }

        public async Task UploadFileAsync(string fileName, Stream fileStream, string path) {
            var putRequest = new PutObjectRequest {
                BucketName = _bucketName,
                Key = Path.Combine(path, fileName),
                InputStream = fileStream,
                ContentType = "image/" + GetImageExtension(fileName)
            };

            await _s3Client.PutObjectAsync(putRequest);
        }

        public string GetFileUrl(string fileName) {
            return $"https://{_bucketName}.s3.{_s3Client.Config.RegionEndpoint.SystemName}.amazonaws.com/{fileName}";
        }

        private string GetImageExtension(string fileName) {
            return Path.GetExtension(fileName).TrimStart('.').ToLower(); // Ex.: "png", "bmp"
        }
    }

}
