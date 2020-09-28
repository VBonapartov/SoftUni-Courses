namespace BookShop.Application.Reviews.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;
    using Common;
    using Domain.Reviews.Factories;    
    using MediatR;

    public class CreateReviewCommand : ReviewCommand<CreateReviewCommand>, IRequest<CreateReviewOutputModel>
    {
        public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreateReviewOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IReviewRepository reviewRepository;
            private readonly IReviewFactory reviewFactory;

            public CreateReviewCommandHandler(
                ICurrentUser currentUser,
                IReviewRepository reviewRepository,
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
                    .WithAuthor(this.currentUser.UserId)
                    .WithBook(request.BookId)
                    .WithTitle(request.Title)
                    .WithDescription(request.Description)
                    .Build();

                await this.reviewRepository.Save(review, cancellationToken);

                return new CreateReviewOutputModel(review.Id);
            }
        }
    }
}