namespace BookShop.Application.Features.Reviews.Commands.Common
{
    using FluentValidation;

    using static Domain.Models.ModelConstants.Review;

    public class ReviewCommandValidator<TCommand> : AbstractValidator<ReviewCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public ReviewCommandValidator(IReviewRepository reviewRepository)
        {
            this.RuleFor(c => c.Title)
                .MinimumLength(MinTitleLength)
                .MaximumLength(MaxTitleLength)
                .NotEmpty();

            this.RuleFor(c => c.Description)
                .MinimumLength(MinDescriptionLength)
                .MaximumLength(MaxDescriptionLength)
                .NotEmpty();
        }
    }
}