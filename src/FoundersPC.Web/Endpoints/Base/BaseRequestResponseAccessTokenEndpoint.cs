using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.Web.Middleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.Web.Endpoints.Base;

[Route("api/Client")]
[ServiceFilter(typeof(ApiTokenCheckFilter), IsReusable = true)]
public abstract class
    BaseRequestResponseAccessTokenEndpoint<TRequest, TResponse, TEndpoint> : BaseRequestResponseAnonymousEndpoint<TRequest, TResponse, TEndpoint>
    where TRequest : class, IRequest<TResponse> { }