namespace Api.Middleware
{
    using Api.Common;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Middleware personalizado
    /// </summary>
    public class CustomMiddleware
    {
        /// <summary>
        /// Objeto con la información del request
        /// </summary>
        private readonly RequestDelegate _request;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request">Request</param>        
        public CustomMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        /// <summary>
        /// Método para todos los request
        /// </summary>
        /// <param name="httpContext"></param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _request(httpContext);
            }
            catch (Exception ex)
            {
                ExceptionResultApi(httpContext, ex);
            }
        }

        /// <summary>
        /// Método para controlar las excepciones
        /// </summary>
        /// <param name="context"></param>
        /// <param name="excepcion"></param>
        protected void ExceptionResultApi(HttpContext context, Exception excepcion)
        {
            string jsonResponse = GetResponseException(excepcion);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.WriteAsync(jsonResponse);
        }

        private static string GetResponseException(Exception excepcion)
        {
            string detalle = $"Error: | {excepcion?.Message} | Detalle: | {excepcion?.InnerException?.Message} | Traza: | {excepcion?.StackTrace}";

            var objectResponse = new ResponseApi(StatusCodes.Status500InternalServerError, excepcion?.Message ?? string.Empty, detalle);

            var jsonResponse = JsonSerializer.Serialize(objectResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return jsonResponse;
        }
    }
}
