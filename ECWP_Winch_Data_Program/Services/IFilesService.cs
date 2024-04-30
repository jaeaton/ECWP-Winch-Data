namespace Services
{
    public interface IFilesService
    {
        public Task<IStorageFile?> OpenFileAsync();
        public Task<IStorageFile?> SaveFileAsync(string extension, string windowTitle, string fileName);
    }
}
