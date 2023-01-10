namespace ApiWithAuth.Core.Utilities
{
    public class Response<T> where T : class
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Succeded { get; set; }
        public int StatusCode { get; set; }
        public Response(string message, T data, bool success, int statusCode)
        {
            Message = message;
            Data = data;
            Succeded = success;
            StatusCode = statusCode;
        }

        public Response()
        {

        }

        public static Response<T> Fail(string message, int statusCode = 404)
        {
            return new Response<T> { Message = message, StatusCode = statusCode };
        }
        public static Response<T> Fail(string message, int statusCode = 404, bool success = false)
        {
            return new Response<T> { Message = message, StatusCode = statusCode, Succeded = success };
        }
        public static Response<T> Success(string message, T data, bool success = true, int statusCode = 200)
        {
            return new Response<T> { Message = message, Data = data, Succeded = success, StatusCode = statusCode };
        }
        public static Response<T> Success(string message, IEnumerable<T> entityList, bool success = true, int statusCode = 200)
        {
            return new Response<T> { Message = message, Succeded = success, StatusCode = statusCode };
        }

    }
}
