namespace BookShop.Application.Features.Books.Queries.Publishers
{
    using BookShop.Application.Mapping;
    using BookShop.Domain.Models.Books;

    public class GetBookPublisherOutputModel : IMapFrom<Publisher>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public int TotalBooks { get; set; }
    }
}