namespace KayraExport.Api.ResponseModels
{
    public abstract record Response
    {
        public bool Success { get; init; }
        public int StatusCode { get; init; }
    }
}
