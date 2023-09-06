using Newtonsoft.Json.Linq;

namespace Global.Errorhandling
{
    public class HttpException: Exception
    {
        public int StatusCode { get; private set; }
        public string ContentType { get; private set; }
        public JObject Json { get; private set; }

        public HttpException(

            int statusCode,
            string message,
            Exception innerException,
            JObject json = null
        ) : base(message, innerException)
        {
            this.StatusCode = statusCode;
            this.ContentType = json != null ? "application/json" : "text/plain";
            this.Json = json;
        }
    }
}
