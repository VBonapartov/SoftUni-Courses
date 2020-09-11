namespace BookShop.Application.Features.Books.Commands.Common
{
    public abstract class BookCommand<TCommand> : EntityCommand<int>
            where TCommand : EntityCommand<int>
    {
        public string Title { get; set; } = default!;

        public string Publisher { get; set; } = default!;

        public decimal Price { get; set; }

        public int NumberOfPages { get; set; }

        public int CoverType { get; set; }

        public int CategoryType { get; set; }
    }
}