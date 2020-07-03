namespace BookShop.Services.Identity
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        string Email { get; }

        bool IsAdministrator { get; }
    }
}