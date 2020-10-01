namespace BookShop.Application.Books.Authors.Commands.Create
{
    public class CreateAuthorOutputModel
    {
        public CreateAuthorOutputModel(int authorId)
            => this.AuthorId = authorId;

        public int AuthorId { get; }
    }
}