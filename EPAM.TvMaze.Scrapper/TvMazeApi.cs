using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;

namespace EPAM.TvMaze.Scrapper
{
    public class TvMazeApi : ITvMazeApi
    {
        private const int DelayAfterTooManyRequestsException = 20000;
        private const int MaxRequestRetries = 3;

        private static readonly ILogger Logger = Log.ForContext<TvMazeApi>();

        private readonly HttpClient _httpClient;

        public TvMazeApi(string host)
        {
            if(string.IsNullOrEmpty(host))
                throw new ArgumentException(nameof(host));
            
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"{host.TrimEnd('/')}/")
            };
        }

        public async Task<IEnumerable<Show>> GetShowsAsync(int page)
        {
            var shows = await RetryingGetAsync<Show>($"shows?page={page}");
            return shows;
        }

        public async Task<IEnumerable<Cast>> GetCastAsync(int showId)
        {
            var casts = await RetryingGetAsync<Cast>($"shows/{showId}/cast");
            return casts;
        }

        private async Task<IEnumerable<T>> RetryingGetAsync<T>(string requestUri)
        {
            var retryCounter = 0;
            do
            {
                var httpReponse = await _httpClient.GetAsync(requestUri);

                if (httpReponse.IsSuccessStatusCode)
                {
                    var result = await httpReponse.Content.ReadAsAsync<List<T>>();
                    return result;
                }

                if (httpReponse.StatusCode == HttpStatusCode.NotFound)
                    return Enumerable.Empty<T>();

                if (httpReponse.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    Logger.Warning($"Failed to get data: {requestUri}. {retryCounter} try");
                    await Task.Delay(DelayAfterTooManyRequestsException);
                }

                retryCounter++;
            } while (retryCounter < MaxRequestRetries);

            throw new InvalidOperationException($"Failed to get data: {requestUri}");
        }
    }
}