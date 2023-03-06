using Coins.Core.Constants;
using Coins.Core.Models.DtoAPI.General;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coins.Core
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                switch (error)
                {
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var baseResponse = new APIResponse()
                {
                    Status = false,
                    Message = response.StatusCode == (int)HttpStatusCode.InternalServerError ?
                        "Sorry! an error occurred, please call technical support." :
                        error.Message,
                    Data = null
                };
                var result = JsonSerializer.Serialize(baseResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
