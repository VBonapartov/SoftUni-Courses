namespace BookShop.Application.Identity.Commands.LoginUser
{
    public class LoginOutputModel
    {
        public LoginOutputModel(string token, int authorId)
        {
            this.Token = token;
            this.AuthorId = authorId;
        }

        public int AuthorId { get; }

        public string Token { get; }
    }
}