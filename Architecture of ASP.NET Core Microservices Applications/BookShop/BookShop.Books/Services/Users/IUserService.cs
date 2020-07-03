namespace BookShop.Books.Services.Users
{
    using System.Threading.Tasks;
    using BookShop.Books.Services.Models.Users;
    using Refit;

    public interface IUserService
    {
        [Post("/Identity/Login")]
        Task<UserOutputModel> Login([Body] UserServiceModel model);

        [Post("/Identity/Register")]
        Task<UserOutputModel> Register([Body] UserRegisterServiceModel model);
    }
}