namespace BookShop.Application.Features.Reviews.Commands.Common
{
    public abstract class ReviewCommand<TCommand> : EntityCommand<int>
            where TCommand : EntityCommand<int>
    {
        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;
    }
}