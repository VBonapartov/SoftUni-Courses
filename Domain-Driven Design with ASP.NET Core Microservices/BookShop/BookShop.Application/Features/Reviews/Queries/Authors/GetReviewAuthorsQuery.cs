namespace BookShop.Application.Features.Reviews.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.Application.Features.Reviews.Queries.Authors;
    using MediatR;

    public class GetReviewAuthorsQuery : IRequest<IEnumerable<GetReviewAuthorOutputModel>>
    {
        public class GetReviewAuthorsQueryHandler : IRequestHandler<
            GetReviewAuthorsQuery,
            IEnumerable<GetReviewAuthorOutputModel>>
        {
            private readonly IReviewRepository reviewRepository;

            public GetReviewAuthorsQueryHandler(IReviewRepository reviewRepository)
                => this.reviewRepository = reviewRepository;

            public async Task<IEnumerable<GetReviewAuthorOutputModel>> Handle(
                GetReviewAuthorsQuery request,
                CancellationToken cancellationToken)
                => await this.reviewRepository.GetReviewAuthors(cancellationToken);
        }
    }
}