using Microsoft.AspNetCore.Http;
using PD.Domain.Constants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PD.Domain.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
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
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case BadRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case InvalidCredentialsException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var errorRespone = new
                {
                    message = error.Message,
                    statusCode = response.StatusCode
                };

                var result = JsonSerializer.Serialize(errorRespone);
                await response.WriteAsync(result);
            }
        }
    }
}
