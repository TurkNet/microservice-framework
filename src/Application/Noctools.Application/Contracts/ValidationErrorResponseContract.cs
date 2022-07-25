using System.Collections.Generic;
using Noctools.Domain.Contracts;

namespace Noctools.Application.Dtos
{
    public sealed class ValidationErrorResponseContract : ApiResponseBaseContract<object>
    {
        public ValidationErrorResponseContract()
        {
            Messages = new List<MessageContractDto>();
        }
    }
}