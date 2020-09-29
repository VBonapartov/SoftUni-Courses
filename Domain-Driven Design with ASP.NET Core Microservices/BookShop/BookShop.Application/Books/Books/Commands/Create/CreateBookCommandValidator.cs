namespace BookShop.Application.Books.Books.Commands.Create
{
    using Common;
    using FluentValidation;

    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator(IBookQueryRepository bookRepository)
            => this.Include(new BookCommandValidator<CreateBookCommand>(bookRepository));
    }
}