namespace BookShop.Books.Services.Models.Users
{
    using BookShop.Books.Models.Users;
    using BookShop.Models;

    public class UserServiceModel : IMapFrom<UserInputModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}