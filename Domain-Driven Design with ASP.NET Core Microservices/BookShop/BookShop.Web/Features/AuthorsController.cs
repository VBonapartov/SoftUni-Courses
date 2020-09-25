namespace BookShop.Web.Features
{
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Books.Authors.Commands.Edit;
    using Application.Books.Authors.Queries.Details;
    using Microsoft.AspNetCore.Mvc;    

    public class AuthorsController : ApiController
    {
        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<AuthorDetailsOutputModel>> Details(
            [FromRoute] AuthorDetailsQuery query)
            => await this.Send(query);

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Edit(
            int id, EditAuthorCommand command)
            => await this.Send(command.SetId(id));
    }
}
