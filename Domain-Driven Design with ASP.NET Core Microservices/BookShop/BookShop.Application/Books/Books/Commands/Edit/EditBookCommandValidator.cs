namespace BookShop.Application.Books.Books.Commands.Edit
{
    using Common;
    using FluentValidation;

    public class EditBookCommandValidator : AbstractValidator<EditBookCommand>
    {
        public EditBookCommandValidator(IBookQueryRepository bookRepository)
            => this.Include(new BookCommandValidator<EditBookCommand>(bookRepository));
    }
}