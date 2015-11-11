using System.Threading.Tasks;

namespace CT.Logging.Models
{
    /// <summary>
    /// Interface for tracking details about HTTP calls.
    /// </summary>
    public interface IHttpLoggingStore
    {
        /// <summary>
        /// Persist details of an HTTP call into durable storage.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task InsertRecordAsync(HttpEntry record);
    }
}
