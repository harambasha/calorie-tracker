namespace CT.WebAPI.HttpPipeline
{
    public class ErrorException
    {
        public string ClassName { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public ErrorException InnerException { get; set; }
    }
}