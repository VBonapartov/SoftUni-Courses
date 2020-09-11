namespace BookShop.Application.Features.Books.Commands.Edit
{
    using Common;
    using FluentValidation;

    public class EditBookCommandValidator : AbstractValidator<EditBookCommand>
    {
        public EditBookCommandValidator(IBookRepository bookRepository)
            => this.Include(new BookCommandValidator<EditBookCommand>(bookRepository));
    }
}