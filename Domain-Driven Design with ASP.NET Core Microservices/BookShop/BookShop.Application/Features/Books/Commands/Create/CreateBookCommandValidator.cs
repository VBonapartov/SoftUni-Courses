namespace BookShop.Application.Features.Books.Commands.Create
{
    using Common;
    using FluentValidation;

    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator(IBookRepository bookRepository)
            => this.Include(new BookCommandValidator<CreateBookCommand>(bookRepository));
    }
}