using Inventory.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net.Http.Headers;

namespace Inventory.WebAPI.Utils
{
    public static class ExceptionHandling
    {

        private const string TYPE_HEADER = "exception-type";

        public static IApplicationBuilder HandleExceptions(this IApplicationBuilder app)
            => app.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;
                    var response = context.Response;

                    if (exception is not null)
                    {

                        switch (exception)
                        {
                            case SingleErrorException singleEx:
                                response.StatusCode = StatusCodes.Status400BadRequest;
                                response.Headers[TYPE_HEADER] = nameof(SingleErrorException);
                                await response.WriteAsync(singleEx.Message);
                                break;

                            case MultiErrorsException multiException:
                                response.StatusCode = StatusCodes.Status400BadRequest;
                                response.Headers[TYPE_HEADER] = nameof(MultiErrorsException);
                                await response.WriteAsJsonAsync(multiException.Messages);
                                break;
                        }

                });
            });

    }
}
