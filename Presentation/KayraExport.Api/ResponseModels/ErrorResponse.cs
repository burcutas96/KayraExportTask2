namespace KayraExport.Api.ResponseModels
{
    public record ErrorResponse : Response
    {
        public string Message { get; init; }


        public ErrorResponse(string message, int statusCode)
        {
            Message = message;
            Success = false;
            StatusCode = statusCode;
        }


    }
}
