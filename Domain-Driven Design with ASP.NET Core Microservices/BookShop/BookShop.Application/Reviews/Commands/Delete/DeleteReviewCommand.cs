namespace BookShop.Application.Reviews.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Contracts;
    using Domain.Reviews.Repositories;
    using MediatR;

    public class DeleteReviewCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IReviewDomainRepository reviewRepository;

            public DeleteReviewCommandHandler(
                ICurrentUser currentUser,
                IReviewDomainRepository reviewRepository)
            {
                this.currentUser = currentUser;
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

                //if (this.currentUser.UserId != request.UserId)
                //{
                //    return false;
                //}

                return await this.reviewRepository.Delete(
                    request.Id,
                    cancellationToken);
            }
        }
    }
}