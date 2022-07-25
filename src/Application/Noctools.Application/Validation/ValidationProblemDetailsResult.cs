using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Noctools.Application.Extensions;
using static Noctools.Utils.Helpers.IdHelper;

namespace Noctools.Application.Validation
{
    /// <inheritdoc />
    /// <summary>
    ///     Reference at
    ///     https://www.strathweb.com/2018/07/centralized-exception-handling-and-request-validation-in-asp-net-core
    /// </summary>
    public class ValidationProblemDetailsResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();
            var errors = new List<ValidationError>();

            var details = "See ValidationErrors for details";

            if (modelStateEntries.Any())
            {
                if (modelStateEntries.Length == 1
                    && modelStateEntries[0].Value.Errors.Count == 1
                    && modelStateEntries[0].Key == string.Empty)
                    details = modelStateEntries[0].Value.Errors[0].ErrorMessage;
                else
                    foreach (var modelStateEntry in modelStateEntries)
                    foreach (var modelStateError in modelStateEntry.Value.Errors)
                    {
                        var error = new ValidationError
                        {
                            Name = modelStateEntry.Key, Description = modelStateError.ErrorMessage
                        };

                        errors.Add(error);
                    }
            }

            var problemDetails = new ValidationProblemDetails
            {
                Status = 400,
                Title = "Request Validation Error",
                Instance = $"urn:noc-tools:badrequest:{GenerateId()}",
                Detail = details,
                ValidationErrors = errors
            };

            context.HttpContext.Response.WriteJson(problemDetails);
            return Task.CompletedTask;
        }
    }
}