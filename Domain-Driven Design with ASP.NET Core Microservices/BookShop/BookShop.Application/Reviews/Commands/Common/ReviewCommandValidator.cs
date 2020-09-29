namespace BookShop.Application.Reviews.Commands.Common
{
    using Application.Common;
    using FluentValidation;

    using static Domain.Common.Models.ModelConstants.Review;

    public class ReviewCommandValidator<TCommand> : AbstractValidator<ReviewCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public ReviewCommandValidator(IReviewQueryRepository reviewRepository)
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