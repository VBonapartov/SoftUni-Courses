namespace BookShop.Application.Features.Books.Commands.Common
{
    using Domain.Common;
    using Domain.Models.Books;
    using FluentValidation;

    using static Domain.Models.ModelConstants.Book;
    using static Domain.Models.ModelConstants.Common;    
    using static Domain.Models.ModelConstants.Options;

    public class BookCommandValidator<TCommand> : AbstractValidator<BookCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public BookCommandValidator(IBookRepository bookRepository)
        {
            this.RuleFor(c => c.Title)
                .MinimumLength(MinTitleLength)
                .MaximumLength(MaxTitleLength)
                .NotEmpty();

            this.RuleFor(c => c.Publisher)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();

            this.RuleFor(c => c.Price)
                .InclusiveBetween(Zero, decimal.MaxValue);

            this.RuleFor(c => c.NumberOfPages)
                .InclusiveBetween(MinNumberOfPages, MaxNumberOfPages);

            this.RuleFor(c => c.CoverType)
                .Must(Enumeration.HasValue<CoverType>)
                .WithMessage("'Cover Type' is not valid.");

            this.RuleFor(c => c.CategoryType)
                .Must(Enumeration.HasValue<CategoryType>)
                .WithMessage("'Category Type' is not valid.");
        }
    }
}