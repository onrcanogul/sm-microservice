//
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using System.Net;
//
// namespace Shared.Base.Middlewares;
//
// public class ExceptionHandler : IExceptionHandler
// {
//     public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
//     {
//         var exception = context.Exception;
//
//         var problemDetails = new ProblemDetails
//         {
//             Status = GetStatusCode(exception), // Hata türüne göre HTTP kodunu alıyoruz
//             Title = "An error occurred while processing your request.",
//             Detail = exception.Message,
//         };
//
//         context.Result = new ObjectResult(problemDetails)
//         {
//             StatusCode = problemDetails.Status
//         };
//
//         return Task.CompletedTask;
//     }
//     
//     private int GetStatusCode(Exception exception)
//     {
//         return exception switch
//         {
//             ArgumentNullException => StatusCodes.Status400BadRequest, // 400 Bad Request
//             UnauthorizedAccessException => StatusCodes.Status401Unauthorized, // 401 Unauthorized
//             KeyNotFoundException => StatusCodes.Status404NotFound, // 404 Not Found
//             InvalidOperationException => StatusCodes.Status409Conflict, // 409 Conflict
//             _ => StatusCodes.Status500InternalServerError, // 500 Internal Server Error for other exceptions
//         };
//     }
// }