namespace BookShop.Reviews.Gateway.Models.Reviews
{
    using BookShop.Models;

    public class MineReviewOutputModel : ReviewOutputModel, IMapFrom<ReviewOutputModel>
    {
        public int TotalViews { get; set; }
    }
}