namespace BookShop.Application.Features.Authors.Commands.Edit
{
    using FluentValidation;

    using static Domain.Models.ModelConstants.Common;

    public class EditAuthorCommandValidator : AbstractValidator<EditAuthorCommand>
    {
        public EditAuthorCommandValidator()
        {
            this.RuleFor(u => u.Name)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();
        }
    }
}