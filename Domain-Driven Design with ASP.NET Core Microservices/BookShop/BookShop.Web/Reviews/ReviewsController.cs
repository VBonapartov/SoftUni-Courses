namespace BookShop.Web.Books
{
    using System.Collections.Generic;
    using System.Threading.Tasks;    
    using Application.Common;
    using Application.Reviews.Commands.Create;
    using Application.Reviews.Commands.Delete;
    using Application.Reviews.Commands.Edit;
    using Application.Reviews.Queries;
    using Application.Reviews.Queries.Authors;
    using Application.Reviews.Queries.Search;
    using Domain.Books.Models.Authors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;    
    using Web.Common;

    public class ReviewsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SearchReviewsOutputModel>> Search(
            [FromQuery] SearchReviewsQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateReviewOutputModel>> Create(
            CreateReviewCommand command)
            => await this.Send(command);

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Edit(
            int id, EditReviewCommand command)
            => await this.Send(command.SetId(id));

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteReviewCommand command)
            => await this.Send(command);

        [HttpGet]
        [Route(nameof(Author))]
        public async Task<ActionResult<IEnumerable<GetReviewAuthorOutputModel>>> Authors(
            [FromQuery] GetReviewAuthorsQuery query)
            => await this.Send(query);
    }
}
