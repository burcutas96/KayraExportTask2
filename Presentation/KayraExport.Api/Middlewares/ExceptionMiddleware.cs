using FluentValidation;
using KayraExport.Api.ResponseModels;
using KayraExport.Domain.Exceptions.General;
using System.Security;
using System.Text.Json;

namespace KayraExport.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }



        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = GetResponseStatusCode(ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;


            string? response = null;

            if (statusCode == StatusCodes.Status500InternalServerError)
                response = JsonSerializer.Serialize(new ServerErrorResponse(ex.GetType(), ex.Message, ex.InnerException?.InnerException?.Message));

            else
                response = JsonSerializer.Serialize(GetExceptionModel(ex, statusCode));


            // errorResponse nesnesini JSON formatına dönüştürüp, HTTP yanıtını o şekilde gönderiyoruz.
            return context.Response.WriteAsync(response);
        }


        private static ErrorResponse GetExceptionModel(Exception ex, int statusCode)
        {
            string errorMessage = ex switch
            {
                ValidationException => ((ValidationException)ex).Errors.First().ErrorMessage,
                _ => ex.Message
            };

            return new ErrorResponse(errorMessage, statusCode);
        }


        private static int GetResponseStatusCode(Exception ex) =>
             ex switch
             {
                 ValidationException => StatusCodes.Status422UnprocessableEntity,
                 NotFoundBaseException => StatusCodes.Status404NotFound,
                 BadRequestBaseException => StatusCodes.Status400BadRequest,
                 _ => StatusCodes.Status500InternalServerError
             };



    }
}
