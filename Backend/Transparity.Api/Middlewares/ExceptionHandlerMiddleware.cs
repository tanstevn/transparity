using FluentValidation;
using System.Net;
using Transparity.Shared.Constants;
using Transparity.Shared.Exceptions;
using Transparity.Shared.Models;

namespace Transparity.Api.Middlewares {
    public class ExceptionHandlerMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            }
            catch (Exception ex) {
                var errObject = Result<object>
                    .Error(ex.Message ?? ErrorMessageConstants.ServerErrMessage);

                _ = ex switch {
                    ArgumentNullException or ValidationException or DataException
                        => WriteErrorResponse(context, 
                            HttpStatusCode.BadRequest, errObject),
                    InvalidOperationException
                        => WriteErrorResponse(context,
                            HttpStatusCode.Gone, errObject),
                    AppException
                        => WriteErrorResponse(context,
                            HttpStatusCode.InternalServerError, errObject),
                    _ => WriteErrorResponse(context,
                        HttpStatusCode.InternalServerError, errObject)
                };
            }
        }

        private static async Task WriteErrorResponse(HttpContext context,
            HttpStatusCode statusCode, object errorObject) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(errorObject);
        }
    }
}
