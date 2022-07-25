using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Noctools.Application.Validation
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public ICollection<ValidationError> ValidationErrors { get; set; }
    }
}