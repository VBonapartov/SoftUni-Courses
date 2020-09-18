namespace BookShop.Application.Reviews.Commands.Common
{
    using Application.Common;

    public abstract class ReviewCommand<TCommand> : EntityCommand<int>
            where TCommand : EntityCommand<int>
    {
        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;
    }
}