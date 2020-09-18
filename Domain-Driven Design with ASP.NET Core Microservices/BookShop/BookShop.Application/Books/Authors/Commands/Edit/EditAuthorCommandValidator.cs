namespace BookShop.Application.Books.Authors.Commands.Edit
{
    using FluentValidation;

    using static Domain.Common.Models.ModelConstants.Common;

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