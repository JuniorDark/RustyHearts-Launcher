using RHLauncher.RHLauncher.Helper;
using System.Net;

namespace RHLauncher.RHLauncher.Http
{
    public class DownloadHelper
    {
        public static async Task<long> GetFileSizeAsync(HttpClient client, string fileUrl, CancellationToken cancellationToken)
        {
            using HttpRequestMessage request = new(HttpMethod.Get, fileUrl);

            try
            {
                using HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.Headers.ContentLength ?? throw new Exception("Content-Length header not found in response headers.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Logger.WriteLog($"File not found: {fileUrl}");
                    return 0;
                }
                else
                {
                    Logger.WriteLog($"Error getting file size for {fileUrl}: {response.StatusCode}");
                    return 0;
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                Logger.WriteLog($"File not found: {fileUrl}");
                return 0;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"Error getting file size for {fileUrl}: {ex.Message}");
                return 0;
            }
        }

        public static string FormatDownloadSpeed(double totalDownloadSpeed)
        {
            string[] sizes = { "B/s", "KB/s", "MB/s", "GB/s" };
            int order = 0;
            while (totalDownloadSpeed >= 1024 && order < sizes.Length - 1)
            {
                order++;
                totalDownloadSpeed /= 1024;
            }

            return $"{totalDownloadSpeed:0.#} {sizes[order]}";
        }

        public static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                bytes /= 1024;
            }

            return $"{bytes:0.#} {sizes[order]}";
        }
    }
}
