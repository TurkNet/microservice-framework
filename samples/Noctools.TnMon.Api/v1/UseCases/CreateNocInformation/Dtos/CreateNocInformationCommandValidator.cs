using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;
using Noctools.TnMon.Api.Contants;
using Noctools.TnMon.Api.Domain.Constants;

namespace Noctools.TnMon.Api.Controllers.UseCases.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CreateNocInformationCommandValidator : AbstractValidator<CreateNocInformationCommand>
    {
        public CreateNocInformationCommandValidator()
        {
            RuleFor(m => m).NotNull()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1001))
                .WithMessage(ApplicationErrorConstants.EAAnakin1001);

            RuleFor(m => m.Category).NotNull().NotEmpty()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1002))
                .WithMessage(ApplicationErrorConstants.EAAnakin1002);

            RuleFor(m => m.Message).NotNull().NotEmpty()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1003))
                .WithMessage(ApplicationErrorConstants.EAAnakin1003);

            RuleFor(m => m.DiscoveryDate).NotNull()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1004))
                .WithMessage(ApplicationErrorConstants.EAAnakin1004);

            RuleFor(m => m.HostName).NotNull().NotEmpty()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1005))
                .WithMessage(ApplicationErrorConstants.EAAnakin1005);

            RuleFor(m => m.LogDate).NotNull()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1006))
                .WithMessage(ApplicationErrorConstants.EAAnakin1006);

            RuleFor(m => m.PopName).NotNull().NotEmpty()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1007))
                .WithMessage(ApplicationErrorConstants.EAAnakin1007);

            RuleFor(m => m.LogIndexId).NotNull().NotEmpty()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1008))
                .WithMessage(ApplicationErrorConstants.EAAnakin1008);

            RuleFor(m => m.LogIndexName).NotNull().NotEmpty()
                .WithErrorCode(nameof(ApplicationErrorConstants.EAAnakin1009))
                .WithMessage(ApplicationErrorConstants.EAAnakin1009);

            RuleFor(m => m)
                .Custom((model, context) =>
                {
                    if (model.RecoveryTime.HasValue)
                    {
                        if (!(model.RecoveryTime > model.LogDate && model.RecoveryTime < model.DiscoveryDate))
                            context.AddFailure(new ValidationFailure("RecoveryTime",
                                ApplicationErrorConstants.EAAnakin10010,
                                nameof(ApplicationErrorConstants.EAAnakin10010)));
                    }
                });
        }
    }
}