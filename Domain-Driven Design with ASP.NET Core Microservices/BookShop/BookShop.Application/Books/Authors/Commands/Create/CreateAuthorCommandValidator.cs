namespace BookShop.Application.Books.Authors.Commands.Create
{
    using FluentValidation;

    using static Domain.Common.Models.ModelConstants.Common;

    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            this.RuleFor(u => u.Name)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();
        }
    }
}
