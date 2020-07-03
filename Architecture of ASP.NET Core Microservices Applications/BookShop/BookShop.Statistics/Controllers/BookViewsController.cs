namespace BookShop.Statistics.Controllers
{
    using System.Threading.Tasks;
    using BookShop.Controllers;
    using BookShop.Statistics.Services.BookViews;
    using Microsoft.AspNetCore.Mvc;    

    public class BookViewsController : ApiController
    {
        private readonly IBookViewService bookViews;

        public BookViewsController(IBookViewService bookViews)
        {
            this.bookViews = bookViews;
        }

        [HttpGet]
        [Route(nameof(TotalViews) + "/" + Id)]
        public async Task<ActionResult<int>> TotalViews(int id)
            => await this.bookViews.GetTotalViews(id);
    }
}