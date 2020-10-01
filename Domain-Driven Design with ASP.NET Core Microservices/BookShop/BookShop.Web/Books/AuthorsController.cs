namespace BookShop.Web.Books
{
    using System.Threading.Tasks;    
    using Application.Books.Authors.Commands.Create;
    using Application.Books.Authors.Commands.Edit;
    using Application.Books.Authors.Queries.Details;
    using Application.Common;    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Web.Common;

    public class AuthorsController : ApiController
    {
        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<AuthorDetailsOutputModel>> Details(
            [FromRoute] AuthorDetailsQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateAuthorOutputModel>> Create(
            CreateAuthorCommand command)
            => await this.Send(command);

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Edit(
            int id, EditAuthorCommand command)
            => await this.Send(command.SetId(id));
    }
}
