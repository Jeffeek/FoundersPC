using System;

namespace FoundersPC.SharedKernel.Exceptions;

public enum AccessTokenExceptionType
{
    Expired = 1,
    TooManyRequests = 2,
    Blocked = 3,
    NotFoundKey = 4,
    InvalidAccessToken = 5,
    NotFoundAccessToken = 6,
}

public class AccessTokenException : Exception
{
    public AccessTokenException(AccessTokenExceptionType type)
        : base(type switch
               {
                   AccessTokenExceptionType.Blocked                => "Your access token is blocked. Contact us if it's a mistake",
                   AccessTokenExceptionType.TooManyRequests        => "Too many requests with your token in one time. Please wait",
                   AccessTokenExceptionType.Expired                => "Your access token has expired",
                   AccessTokenExceptionType.NotFoundKey            => "Not found key 'api-key' in your request header",
                   AccessTokenExceptionType.InvalidAccessToken     => "Key 'api-key' in your request header is invalid",
                   AccessTokenExceptionType.NotFoundAccessToken    => "Key 'api-key' in your request header not found",
                   _                                               => "Unknown access token exception"
               }) { }
}