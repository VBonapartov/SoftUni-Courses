﻿namespace BookShop.Application.Reviews.Commands.Create
{
    using Common;
    using FluentValidation;

    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator(IReviewQueryRepository reviewRepository)
            => this.Include(new ReviewCommandValidator<CreateReviewCommand>(reviewRepository));
    }
}