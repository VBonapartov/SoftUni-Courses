namespace BookShop.Application.Features.Reviews.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    public class DeleteReviewCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Result>
        {
            //private readonly ICurrentUser currentUser;
            private readonly IReviewRepository reviewRepository;

            public DeleteReviewCommandHandler(
                //ICurrentUser currentUser,
                IReviewRepository reviewRepository)
            //IDealerRepository dealerRepository)
            {
                //this.currentUser = currentUser;
                this.reviewRepository = reviewRepository;
            }

            public async Task<Result> Handle(
                DeleteReviewCommand request,
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

                return await this.reviewRepository.Delete(
                    request.Id,
                    cancellationToken);
            }
        }
    }
}