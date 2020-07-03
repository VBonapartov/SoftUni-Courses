namespace BookShop.Services.Identity
{
    public interface ICurrentTokenService
    {
        string Get();

        void Set(string token);
    }
}