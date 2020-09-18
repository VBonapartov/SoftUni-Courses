namespace BookShop.Application.Books.Books.Queries.Publishers
{
    using Application.Common.Mapping;
    using Domain.Books.Models.Books;

    public class GetBookPublisherOutputModel : IMapFrom<Publisher>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public int TotalBooks { get; set; }
    }
}