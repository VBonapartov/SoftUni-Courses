namespace BookShop.Web.Features
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Books.Books.Commands.ChangeAvailability;
    using Application.Books.Books.Commands.Create;
    using Application.Books.Books.Commands.Delete;
    using Application.Books.Books.Commands.Edit;
    using Application.Books.Books.Queries.Details;
    using Application.Books.Books.Queries.Publishers;
    using Application.Books.Books.Queries.Search;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using BookShop.Domain.Books.Models.Books;

    public class BooksController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SearchBooksOutputModel>> Search(
            [FromQuery] SearchBooksQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<BookDetailsOutputModel>> Details(
            [FromRoute] BookDetailsQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateBookOutputModel>> Create(
            CreateBookCommand command)
            => await this.Send(command);

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Edit(
            int id, EditBookCommand command)
            => await this.Send(command.SetId(id));

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteBookCommand command)
            => await this.Send(command);

        [HttpGet]
        [Route(nameof(Publisher))]
        public async Task<ActionResult<IEnumerable<GetBookPublisherOutputModel>>> Categories(
            [FromQuery] GetBookPublishersQuery query)
            => await this.Send(query);

        [HttpPut]
        [Authorize]
        [Route(Id + PathSeparator + nameof(ChangeAvailability))]
        public async Task<ActionResult> ChangeAvailability(
            [FromRoute] ChangeAvailabilityCommand query)
            => await this.Send(query);
    }
}
