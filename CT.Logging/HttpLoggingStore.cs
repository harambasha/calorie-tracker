using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CT.Logging.Models;
using Newtonsoft.Json;

namespace CT.Logging
{
    /// <summary>
    /// Dummy implementation of the <see cref="HttpEntry"/> interface to file, for illustration purposes.
    /// </summary>
    public sealed class HttpLoggingStore : IHttpLoggingStore
    {
        private readonly string _location = AppDomain.CurrentDomain.BaseDirectory.Replace("bin", "TrackingLogs");

        public async Task InsertRecordAsync(HttpEntry record)
        {
            var path = Path.Combine(_location, record.LoggingId.ToString("d"));
            using (var stream = File.OpenWrite(path))
            using (var writer = new StreamWriter(stream))
                await writer.WriteAsync(JsonConvert.SerializeObject(record));
        }
    }
}
