using FoundersPC.SharedKernel.ApplicationConstants;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.Web.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.Web.Endpoints.Base;

[Route("api/Client")]
[Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
[ServiceFilter(typeof(ApiTokenCheckFilter), IsReusable = true)]
public abstract class
    BaseRequestResponseAccessTokenEndpoint<TRequest, TResponse, TEndpoint> : BaseRequestResponseAnonymousEndpoint<TRequest, TResponse, TEndpoint>
    where TRequest : class, IRequest<TResponse> { }