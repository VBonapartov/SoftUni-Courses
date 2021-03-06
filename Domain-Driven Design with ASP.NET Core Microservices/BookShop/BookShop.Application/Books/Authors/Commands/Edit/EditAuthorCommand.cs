﻿namespace BookShop.Application.Books.Authors.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks; 
    using Common;
    using Common.Contracts;
    using Domain.Books.Repositories;
    using MediatR;

    public class EditAuthorCommand : EntityCommand<int>, IRequest<Result>
    {
        public string Name { get; set; } = default!;

        public class EditAuthorCommandHandler : IRequestHandler<EditAuthorCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IAuthorDomainRepository authorRepository;

            public EditAuthorCommandHandler(
                ICurrentUser currentUser,
                IAuthorDomainRepository authorRepository)
            {
                this.currentUser = currentUser;
                this.authorRepository = authorRepository;
            }

            public async Task<Result> Handle(
                EditAuthorCommand request,
                CancellationToken cancellationToken)
            {
                var author = await this.authorRepository.FindByUser(
                    this.currentUser.UserId,
                    cancellationToken);

                if (request.Id != author.Id)
                {
                    return "You cannot edit this author.";
                }

                author
                    .UpdateName(request.Name);

                await this.authorRepository.Save(author, cancellationToken);

                return Result.Success;
            }
        }
    }
}