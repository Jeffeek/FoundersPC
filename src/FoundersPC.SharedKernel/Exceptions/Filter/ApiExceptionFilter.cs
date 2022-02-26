#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net;
using FoundersPC.SharedKernel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

#endregion

namespace FoundersPC.SharedKernel.Exceptions.Filter;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilter() =>
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
                             {
                                 {
                                     typeof(ValidationException), HandleValidationException
                                 },
                                 {
                                     typeof(NotFoundException), HandleNotFoundException
                                 },
                                 {
                                     typeof(BadRequestException), HandleBadRequestException
                                 },
                                 {
                                     typeof(StatusCodeException), HandleExceptionBase
                                 },
                                 {
                                     typeof(AccessTokenException), HandleAccessTokenException
                                 }
                             };

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();

        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type](context);

            return;
        }

        HandleUnknownException(context);
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        var stacktrace = String.Empty;

        #if DEBUG
        stacktrace = context.Exception.StackTrace;
        #endif

        var details = new Error($"{StatusCodes.Status500InternalServerError}. Unknown server error",
                                $"Exception: {context.Exception.Message}. Stacktrace: {stacktrace}");

        context.Result = new ObjectResult(details)
                         {
                             StatusCode = StatusCodes.Status500InternalServerError
                         };

        context.ExceptionHandled = true;
    }

    private static void HandleExceptionBase(ExceptionContext context)
    {
        var exception = context.Exception as StatusCodeException;

        Error details;

        switch (exception?.StatusCode)
        {
            case HttpStatusCode.NotFound:
                details = new("The specified resource was not found.", exception.Message);

                context.Result = new NotFoundObjectResult(details);
                context.ExceptionHandled = true;

                break;

            case HttpStatusCode.BadRequest:
                details = new("Bad request", exception.Message);

                context.Result = new BadRequestObjectResult(details);
                context.ExceptionHandled = true;

                break;
        }
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidationException;

        var details = new Error("Validation error.", JsonConvert.SerializeObject(exception?.Errors));

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;

        var details = new Error("Not found", exception?.Message ?? "");

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleBadRequestException(ExceptionContext context)
    {
        if (context.Exception is BadRequestException exception && exception.IsError())
        {
            context.Result = new BadRequestObjectResult(exception.GetError());
        }
        else
        {
            var details = new Error("Bad request", "400");

            context.Result = new BadRequestObjectResult(details);
        }

        context.ExceptionHandled = true;
    }

    private static void HandleAccessTokenException(ExceptionContext context)
    {
        if (context.Exception is AccessTokenException exception)
            context.Result ??= new BadRequestObjectResult(new Error("Access token exception", exception.Message));

        context.ExceptionHandled = true;
    }
}