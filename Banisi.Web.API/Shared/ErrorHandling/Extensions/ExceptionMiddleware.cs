using Banisi.Web.API.Shared.ErrorHandling.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Banisi.Web.API.Shared.ErrorHandling.Extensions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger, IConfiguration configuration)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var customerName = configuration.GetValue<string>("CustomerName");
                    var microserviceName = "Banisi";

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        logger.LogError($"C:{customerName} M:{microserviceName} {Environment.NewLine} {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}