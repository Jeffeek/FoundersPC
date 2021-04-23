#region Using namespaces

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
        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="uriString"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException"/>, instead.
        ///     <paramref name="uriString"/> is empty.
        ///     -or-
        ///     The scheme specified in <paramref name="uriString"/> is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)"/>.
        ///     -or-
        ///     <paramref name="uriString"/> contains too many slashes.
        ///     -or-
        ///     The password specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The host name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The file name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The user name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The host or authority name specified in <paramref name="uriString"/> cannot be terminated by backslashes.
        ///     -or-
        ///     The port number specified in <paramref name="uriString"/> is not valid or cannot be parsed.
        ///     -or-
        ///     The length of <paramref name="uriString"/> exceeds 65519 characters.
        ///     -or-
        ///     The length of the scheme specified in <paramref name="uriString"/> exceeds 1023 characters.
        ///     -or-
        ///     There is an invalid character sequence in <paramref name="uriString"/>.
        ///     -or-
        ///     The MS-DOS path specified in <paramref name="uriString"/> must start with c:\\.
        /// </exception>

        #endregion

        public static void PrepareJsonRequest(this HttpClient client, string baseAddress = null)
        {
            if (baseAddress is not null)
                client.BaseAddress = new Uri(baseAddress);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }

        #region Docs

        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException"/>, instead.
        ///     <paramref name="uriString"/> is empty.
        ///     -or-
        ///     The scheme specified in <paramref name="uriString"/> is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)"/>.
        ///     -or-
        ///     <paramref name="uriString"/> contains too many slashes.
        ///     -or-
        ///     The password specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The host name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The file name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The user name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The host or authority name specified in <paramref name="uriString"/> cannot be terminated by backslashes.
        ///     -or-
        ///     The port number specified in <paramref name="uriString"/> is not valid or cannot be parsed.
        ///     -or-
        ///     The length of <paramref name="uriString"/> exceeds 65519 characters.
        ///     -or-
        ///     The length of the scheme specified in <paramref name="uriString"/> exceeds 1023 characters.
        ///     -or-
        ///     There is an invalid character sequence in <paramref name="uriString"/>.
        ///     -or-
        ///     The MS-DOS path specified in <paramref name="uriString"/> must start with c:\\.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>

        #endregion

        public static void PrepareJsonRequestWithAuthentication(this HttpClient client,
                                                                string authScheme,
                                                                string token,
                                                                string baseAddress = null)
        {
            client.PrepareJsonRequest(baseAddress);
            client.PrepareRequestWithAuthentication(authScheme, token);
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="uriString"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException"/>, instead.
        ///     <paramref name="uriString"/> is empty.
        ///     -or-
        ///     The scheme specified in <paramref name="uriString"/> is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)"/>.
        ///     -or-
        ///     <paramref name="uriString"/> contains too many slashes.
        ///     -or-
        ///     The password specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The host name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The file name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The user name specified in <paramref name="uriString"/> is not valid.
        ///     -or-
        ///     The host or authority name specified in <paramref name="uriString"/> cannot be terminated by backslashes.
        ///     -or-
        ///     The port number specified in <paramref name="uriString"/> is not valid or cannot be parsed.
        ///     -or-
        ///     The length of <paramref name="uriString"/> exceeds 65519 characters.
        ///     -or-
        ///     The length of the scheme specified in <paramref name="uriString"/> exceeds 1023 characters.
        ///     -or-
        ///     There is an invalid character sequence in <paramref name="uriString"/>.
        ///     -or-
        ///     The MS-DOS path specified in <paramref name="uriString"/> must start with c:\\.
        /// </exception>

        #endregion

        public static void PrepareRequestWithAuthentication(this HttpClient client,
                                                            string authScheme,
                                                            string token,
                                                            string baseAddress = null)
        {
            if (baseAddress is not null)
                client.BaseAddress = new Uri(baseAddress);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authScheme, token);
        }

        #region Docs

        /// <exception cref="T:System.ArgumentException">Token was null or length was not 64.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     Misused header name. Make sure request headers are used with
        ///     HttpRequestMessage, response headers with HttpResponseMessage, and content headers with HttpContent objects.
        /// </exception>
        /// <exception cref="T:System.FormatException">
        ///     The header name format is invalid.
        ///     -or-
        ///     New line characters in header values must be followed by a white-space character.
        /// </exception>

        #endregion

        public static void AddApiAccessTokenInHeader(this HttpClient client, string token)
        {
            if (token is null
                || token.Length != 64)
                throw new ArgumentException(nameof(token));

            client.DefaultRequestHeaders.Add("HARDWARE-ACCESS-TOKEN", token);
        }

        #region Delete As Json Async

        #region Docs

        /// <exception cref="T:System.Net.Http.HttpRequestException">
        ///     The request failed due to an underlying issue such as network
        ///     connectivity, DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
        ///     .NET Core and .NET 5.0 and later only: The request
        ///     failed due to timeout.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The request message was already sent by the
        ///     <see cref="T:System.Net.Http.HttpClient"/> instance.
        /// </exception>

        #endregion

        public static Task<HttpResponseMessage>
            DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T data) =>
            httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
                                 {
                                     Content = JsonContent.Create(data,
                                                                  typeof(T))
                                 });

        #region Docs

        /// <exception cref="T:System.Net.Http.HttpRequestException">
        ///     The request failed due to an underlying issue such as network
        ///     connectivity, DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
        ///     .NET Core and .NET 5.0 and later only: The request
        ///     failed due to timeout.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The request message was already sent by the
        ///     <see cref="T:System.Net.Http.HttpClient"/> instance.
        /// </exception>

        #endregion

        public static Task<HttpResponseMessage>
            DeleteAsJsonAsync<T>(this HttpClient httpClient,
                                 string requestUri,
                                 T data,
                                 CancellationToken cancellationToken) =>
            httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
                                 {
                                     Content = JsonContent.Create(data,
                                                                  typeof(T))
                                 },
                                 cancellationToken);

        #region Docs

        /// <exception cref="T:System.Net.Http.HttpRequestException">
        ///     The request failed due to an underlying issue such as network
        ///     connectivity, DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
        ///     .NET Core and .NET 5.0 and later only: The request
        ///     failed due to timeout.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The request message was already sent by the
        ///     <see cref="T:System.Net.Http.HttpClient"/> instance.
        /// </exception>

        #endregion

        public static Task<HttpResponseMessage>
            DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data) =>
            httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
                                 {
                                     Content = JsonContent.Create(data,
                                                                  typeof(T))
                                 });

        #region Docs

        /// <exception cref="T:System.Net.Http.HttpRequestException">
        ///     The request failed due to an underlying issue such as network
        ///     connectivity, DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
        ///     .NET Core and .NET 5.0 and later only: The request
        ///     failed due to timeout.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The request message was already sent by the
        ///     <see cref="T:System.Net.Http.HttpClient"/> instance.
        /// </exception>

        #endregion

        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient,
                                                                     Uri requestUri,
                                                                     T data,
                                                                     CancellationToken cancellationToken) =>
            httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
                                 {
                                     Content = JsonContent.Create(data,
                                                                  typeof(T))
                                 },
                                 cancellationToken);

        #endregion
    }
}