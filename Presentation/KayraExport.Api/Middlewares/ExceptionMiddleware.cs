using FluentValidation;
using KayraExport.Api.ResponseModels;
using KayraExport.Domain.Exceptions.General;
using System.Security;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace KayraExport.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        readonly RequestDelegate _next;
        readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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



        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = GetResponseStatusCode(ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;


            // Türkçe ve diğer Unicode karakterlerin JSON'da düzgün görünmesini sağlamak için Encoder ayarlanıyor.
            JsonSerializerOptions options = new()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), 
                WriteIndented = true
            };

            string? response = null;

            if (statusCode == StatusCodes.Status500InternalServerError)
                response = JsonSerializer.Serialize(new ServerErrorResponse(ex.GetType(), ex.Message, ex.InnerException?.InnerException?.Message), options);

            else
                response = JsonSerializer.Serialize(GetExceptionModel(ex, statusCode), options);

            _logger.LogError(response);

            // errorResponse nesnesini JSON formatına dönüştürüp, HTTP yanıtını o şekilde gönderiyoruz.
            return context.Response.WriteAsync(response);
        }


        private ErrorResponse GetExceptionModel(Exception ex, int statusCode)
        {
            string errorMessage = ex switch
            {
                ValidationException => ((ValidationException)ex).Errors.First().ErrorMessage,
                _ => ex.Message
            };

            return new ErrorResponse(errorMessage, statusCode);
        }


        private int GetResponseStatusCode(Exception ex) =>
             ex switch
             {
                 ValidationException => StatusCodes.Status422UnprocessableEntity,
                 NotFoundBaseException => StatusCodes.Status404NotFound,
                 BadRequestBaseException => StatusCodes.Status400BadRequest,
                 _ => StatusCodes.Status500InternalServerError
             };



    }
}
