namespace BookShop.Identity.Services.Identity
{
    using Data.Models;
    using System.Collections.Generic;

    public interface ITokenGeneratorService
    {
        string GenerateToken(User user, IEnumerable<string> roles = null);
    }
}