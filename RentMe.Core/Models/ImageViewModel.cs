namespace RentMe.Core.Models
{
    public class ImageViewModel : IDisposable
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? DataUrl { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Is this instance disposed?
        /// </summary>
        protected bool Disposed { get; private set; }

        /// <summary>
        /// Dispose worker method. See http://coding.abel.nu/2012/01/disposable
        /// </summary>
        /// <param name="disposing">Are we disposing? 
        /// Otherwise we're finalizing.</param>
        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
    }
}
