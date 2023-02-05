using ApiWithAuth.Core.Domain;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace ApiWithAuth.Extensions
{
    public static class GlobalExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(erro =>
            {
                erro.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Somethin went wrong in the {contextFeature.Error}");
                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "An error occur, Please try again later"
                        }.ToString());
                    }
                });
            });
        }
    }
}
