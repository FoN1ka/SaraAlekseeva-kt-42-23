using System.Net;

namespace WebApplication1.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Произошла ошибка");

                var httpResponse = context.Response;

                // Защита от падения, если ответ уже начал отправляться
                if (!httpResponse.HasStarted)
                {
                    httpResponse.ContentType = "application/json";
                    httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var responseModel = new ResponseModel<object>
                    {
                        Succeeded = false,
                        Message = exception.Message,
                        // Безопасно собираем ошибки
                        Errors = new List<string>
                {
                    exception.Message,
                    exception.InnerException?.Message
                }.Where(e => !string.IsNullOrWhiteSpace(e)).ToList()
                    };

                    await httpResponse.WriteAsJsonAsync(responseModel);
                }
            }
        }
    }
    public class ResponseModel<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public ResponseModel()
        {
        }
        public ResponseModel(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public ResponseModel(string message)
        {
            Succeeded = true;
            Message = message;
        }
    }
}
//Запустить без отладчика (Самый быстрый)
//В Visual Studio нажмите Ctrl + F5 (Запуск без отладки) вместо обычного F5.
//В этом режиме Visual Studio не будет вмешиваться в ошибки.
