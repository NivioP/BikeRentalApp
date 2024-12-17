namespace BikeRentalApp.Application.Interfaces {
    public interface IS3Service {
        Task UploadFileAsync(string fileName, Stream fileStream, string storagePath);
        string GetFileUrl(string fileName);
    }
}
