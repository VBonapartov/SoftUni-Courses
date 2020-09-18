namespace BookShop.Application.Reviews.Commands.Edit
{
    using Common;
    using FluentValidation;

    public class EditReviewCommandValidator : AbstractValidator<EditReviewCommand>
    {
        public EditReviewCommandValidator(IReviewRepository reviewRepository)
            => this.Include(new ReviewCommandValidator<EditReviewCommand>(reviewRepository));
    }
}