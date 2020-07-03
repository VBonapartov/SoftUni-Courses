namespace BookShop.Services.Identity
{
    using System;
    using System.Security.Claims;
    using Infrastructure;
    using Microsoft.AspNetCore.Http;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.user = httpContextAccessor.HttpContext?.User;

            if (this.user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }
            
            this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            this.Email = user.FindFirstValue(ClaimTypes.Name);
        }

        public string UserId { get; }

        public string Email { get; }

        public bool IsAdministrator => this.user.IsAdministrator();
    }
}