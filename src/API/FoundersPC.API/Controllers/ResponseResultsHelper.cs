#region Using namespaces

using System.Net;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers
{
    internal static class ResponseResultsHelper
    {
        public static ActionResult NotFoundByIdResult(int id) =>
            new ContentResult
            {
                Content = $"Entity with id = {id} not found",
                StatusCode = (int)HttpStatusCode.NotFound
            };

        public static ActionResult BadRequestWithIdResult(object obj = null) =>
            new ContentResult
            {
                Content = $"Monkey did a bad request.. {obj} - is not right!",
                StatusCode = (int)HttpStatusCode.BadRequest
            };

        public static ActionResult UpdateError(string description = "Error happened when server tried to update the model") =>
            new ObjectResult(new ProblemDetails
                             {
                                 Detail = description,
                                 Title = "Update error",
                                 Status = (int)HttpStatusCode.InternalServerError
                             });

        public static ActionResult InsertError(string description = "Error happened when server tried to insert the model") =>
            new ObjectResult(new ProblemDetails
                             {
                                 Detail = description,
                                 Title = "Insert error",
                                 Status = (int)HttpStatusCode.InternalServerError
                             });

        public static ActionResult DeleteError(string description = "Error happened when server tried to delete the model") =>
            new ObjectResult(new ProblemDetails
                             {
                                 Detail = description,
                                 Title = "Delete error",
                                 Status = (int)HttpStatusCode.InternalServerError
                             });
    }
}