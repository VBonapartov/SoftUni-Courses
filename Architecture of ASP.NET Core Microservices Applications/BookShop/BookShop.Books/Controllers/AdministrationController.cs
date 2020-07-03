namespace BookShop.Books.Controllers
{
    using BookShop.Infrastructure;

    [AuthorizeAdministrator]
    public abstract class AdministrationController : BaseController
    {
    }
}