#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage>
            DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T data) =>
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
                                       {
                                           Content = JsonContent.Create(data,
                                                                        typeof(T))
                                       });

        public static async Task<HttpResponseMessage>
            DeleteAsJsonAsync<T>(this HttpClient httpClient,
                                 string requestUri,
                                 T data,
                                 CancellationToken cancellationToken) =>
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
                                       {
                                           Content = JsonContent.Create(data,
                                                                        typeof(T))
                                       },
                                       cancellationToken);

        public static async Task<HttpResponseMessage>
            DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data) =>
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
                                       {
                                           Content = JsonContent.Create(data,
                                                                        typeof(T))
                                       });

        public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient,
                                                                           Uri requestUri,
                                                                           T data,
                                                                           CancellationToken cancellationToken) =>
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
                                       {
                                           Content = JsonContent.Create(data,
                                                                        typeof(T))
                                       },
                                       cancellationToken);
    }
}