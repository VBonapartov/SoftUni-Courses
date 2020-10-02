namespace BookShop.Application.Reviews.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;    
    using Common;
    using Domain.Reviews.Factories;
    using Domain.Reviews.Repositories;
    using MediatR;

    public class CreateReviewCommand : ReviewCommand<CreateReviewCommand>, IRequest<CreateReviewOutputModel>
    {
        public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreateReviewOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IReviewDomainRepository reviewRepository;
            private readonly IReviewFactory reviewFactory;

            public CreateReviewCommandHandler(
                ICurrentUser currentUser,
                IReviewDomainRepository reviewRepository,
                IReviewFactory reviewFactory)
            {
                this.currentUser = currentUser;
                this.reviewRepository = reviewRepository;
                this.reviewFactory = reviewFactory;
            }

            public async Task<CreateReviewOutputModel> Handle(
                CreateReviewCommand request,
                CancellationToken cancellationToken)
            {
                //var author = await this.Repository.GetAuthor(
                //    request.Author,
                //    cancellationToken);

                //var publisher = await this.bookRepository.GetPublisher(
                //    request.Publisher,
                //    cancellationToken);

                //var factory = author == null
                //    ? this.bookFactory.WithAuthor(request.Author)
                //    : this.bookFactory.WithAuthor(author);

                var review = this.reviewFactory
                    .ForBook(request.BookId)
                    .WithAuthor(this.currentUser.UserId)                    
                    .WithTitle(request.Title)
                    .WithDescription(request.Description)
                    .Build();

                await this.reviewRepository.Save(review, cancellationToken);

                return new CreateReviewOutputModel(review.Id);
            }
        }
    }
}