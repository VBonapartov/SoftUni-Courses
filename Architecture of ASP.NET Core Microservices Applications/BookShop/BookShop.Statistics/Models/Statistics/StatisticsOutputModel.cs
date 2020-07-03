namespace BookShop.Statistics.Models.Statistics
{
    using BookShop.Models;
    using Data.Models;

    public class StatisticsOutputModel : IMapFrom<Statistics>
    {
        public int TotalBooks { get; set; }

        public int TotalReviews { get; set; }
    }
}