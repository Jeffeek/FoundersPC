﻿using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoundersPC.SharedKernel.Exceptions.Filter
{
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

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
                          {
                              Status = StatusCodes.Status500InternalServerError,
                              Title = "An error occurred while processing your request.",
                              Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1", 
                              Extensions =
                              {
                                  {
                                      "exception", context.Exception.Message
                                  },
                                  {
                                      "stacktrace", context.Exception.StackTrace
                                  }}
                          };

            context.Result = new ObjectResult(details)
                             {
                                 StatusCode = StatusCodes.Status500InternalServerError
                             };

            context.ExceptionHandled = true;
        }

        private void HandleExceptionBase(ExceptionContext context)
        {
            var exception = context.Exception as StatusCodeException;

            ProblemDetails details;

            switch (exception?.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    details = new ProblemDetails()
                              {
                                  Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                                  Title = "The specified resource was not found.",
                                  Detail = exception.Message
                              };

                    context.Result = new NotFoundObjectResult(details);
                    context.ExceptionHandled = true;

                    break;

                case HttpStatusCode.BadRequest:
                    details = new ProblemDetails()
                              {
                                  Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                                  Detail = exception.Message
                              };

                    context.Result = new BadRequestObjectResult(details);
                    context.ExceptionHandled = true;

                    break;
            }
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
                          {
                              Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                          };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
                          {
                              Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                              Title = "The specified resource was not found.",
                              Detail = exception.Message
                          };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleBadRequestException(ExceptionContext context)
        {
            var exception = context.Exception as BadRequestException;

            if (exception != null && exception.IsError())
            {
                context.Result = new BadRequestObjectResult(exception.GetError());
            }
            else
            {
                var details = new ProblemDetails()
                              {
                                  Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                                  Detail = exception.Message
                              };

                context.Result = new BadRequestObjectResult(details);
            }

            context.ExceptionHandled = true;
        }
    }
}