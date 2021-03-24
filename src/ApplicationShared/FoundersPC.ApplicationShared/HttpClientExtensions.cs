﻿#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public static void PrepareJsonRequest(this HttpClient client, string baseAddress = null)
        {
            if (baseAddress is not null) client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
    }
}