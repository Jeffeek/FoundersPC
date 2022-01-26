using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace FoundersPC.Web.Services;

public class AppSettings
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppSettings(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;

    public string GetAbsoluteUri(string relativeUri)
    {
        var redirectUri = relativeUri;

        var uri = new Uri(relativeUri, UriKind.RelativeOrAbsolute);

        if (uri.IsAbsoluteUri)
            return redirectUri;

        var baseUri = _httpContextAccessor.HttpContext?.Request.GetEncodedUrl();

        if (!String.IsNullOrWhiteSpace(baseUri) && !baseUri.EndsWith("/"))
            baseUri += "/";

        redirectUri = baseUri + redirectUri;

        return redirectUri;
    }
}