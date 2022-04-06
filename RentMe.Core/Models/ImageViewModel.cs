namespace RentMe.Core.Models
{
    public class ImageViewModel : IDisposable
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? DataUrl { get; set; }

        protected bool Disposed { get; private set; }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
    }
}
