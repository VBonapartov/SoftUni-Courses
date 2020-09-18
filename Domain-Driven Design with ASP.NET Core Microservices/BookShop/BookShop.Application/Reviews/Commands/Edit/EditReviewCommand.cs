namespace BookShop.Application.Reviews.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Contracts;
    using Commands.Common;
    using MediatR;

    public class EditReviewCommand : ReviewCommand<EditReviewCommand>, IRequest<Result>
    {
        public class EditReviewCommandHandler : IRequestHandler<EditReviewCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IReviewRepository reviewRepository;

            public EditReviewCommandHandler(
                ICurrentUser currentUser,
                IReviewRepository reviewRepository)
            {
                this.currentUser = currentUser;
                this.reviewRepository = reviewRepository;
            }

            public async Task<Result> Handle(
                EditReviewCommand request,
                CancellationToken cancellationToken)
            {
                //var dealerHasCar = await this.currentUser.DealerHasCarAd(
                //    this.dealerRepository,
                //    request.Id,
                //    cancellationToken);

                //if (!dealerHasCar)
                //{
                //    return dealerHasCar;
                //}

                var review = await this.reviewRepository
                    .Find(request.Id, cancellationToken);

                if (this.currentUser.UserId != review.UserId)
                {
                    return false;
                }

                review
                    .UpdateTitle(request.Title)
                    .UpdateDescription(request.Description);

                await this.reviewRepository.Save(review, cancellationToken);

                return Result.Success;
            }
        }
    }
}