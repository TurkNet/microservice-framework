using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Noctools.Domain;
using Noctools.Domain.Contracts;
using Noctools.Application.Dtos;
using Newtonsoft.Json;

namespace Noctools.Application.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private const string ErrorMessage = "Üzgünüz! İşleminiz sırasında beklenmedik bir hata olustu.";

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
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
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            string errorCode;
            HttpStatusCode statusCode;
            string message = exception.Message;

            switch (exception)
            {
                case ValidationException validationException:
                {
                    errorCode = validationException.Code;
                    switch (errorCode)
                    {
                        default:
                            statusCode = HttpStatusCode.BadRequest;
                            message = string.IsNullOrEmpty(validationException.UserFriendlyMessage)
                                ? ErrorMessage
                                : validationException.UserFriendlyMessage;
                            break;
                    }

                    break;
                }
                case BusinessException businessException:
                {
                    errorCode = businessException.Code;
                    switch (errorCode)
                    {
                        default:
                            statusCode = HttpStatusCode.BadRequest;
                            message = businessException.Message;
                            break;
                    }

                    break;
                }
                case DomainException domainException:
                {
                    errorCode = domainException.Code;
                    switch (errorCode)
                    {
                        default:
                            statusCode = HttpStatusCode.BadRequest;
                            message = domainException.Message;
                            break;
                    }

                    break;
                }
                case AuthenticationException _:
                {
                    errorCode = "EUNAUTH1001";
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                }
                default:
                    errorCode = "EUN1001";
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                message = exception.ToString();
            }


            _logger.LogError(
                "[ERROR] Code(Business): {errorCode} StatusCode: {statusCode} Message: {message}", errorCode,
                statusCode, message);

            ErrorResponseContract responseModel = new ErrorResponseContract
            {
                Instance = $"urn:noc-tools:{statusCode}:{context.TraceIdentifier}",
                Messages = new List<MessageContractDto>
                {
                    new MessageContractDto
                    {
                        Code = errorCode,
                        Content = exception is BusinessException || exception is ValidationException
                            ? message
                            : string.Empty,
                        Title = ErrorMessage,
                        Type = MessageType.Error
                    }
                },
                ReturnPath = context.Items.ContainsKey("returnPath")
                    ? context.Items["returnPath"].ToString()
                    : string.Empty
            };

            var content = JsonConvert.SerializeObject(responseModel);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            await context.Response.WriteAsync(content);
        }
    }
}