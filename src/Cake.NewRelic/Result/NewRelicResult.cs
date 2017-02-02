namespace Cake.NewRelic.Result
{
    public class NewRelicResult
    {
        public readonly bool Successful;
        public readonly ErrorResult Error;
        public NewRelicResult(bool wasSuccess, ErrorResult error)
        {
            Successful = wasSuccess;
            Error = error;
        }
    }
    public class ErrorResult
    {
        public int StatusCode { get; set; }
        public string ErrorContent { get; set; }
    }
}
