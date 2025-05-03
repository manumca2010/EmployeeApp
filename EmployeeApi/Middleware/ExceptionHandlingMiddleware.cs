using System.Net;
using System.Text.Json;

namespace EmployeeApi.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware

    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";

                var response = new { error = ex.Message };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));

            }

        }

    }

}
