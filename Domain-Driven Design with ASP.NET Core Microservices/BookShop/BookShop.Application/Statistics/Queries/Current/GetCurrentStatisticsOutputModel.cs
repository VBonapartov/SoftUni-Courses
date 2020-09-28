namespace BookShop.Application.Statistics.Queries.Current
{
    using AutoMapper;
    using Common.Mapping;
    using Domain.Statistics.Models;

    public class GetCurrentStatisticsOutputModel : IMapFrom<Statistics>
    {
        public int TotalBooks { get; private set; }

        public int TotalBookViews { get; private set; }

        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<Statistics, GetCurrentStatisticsOutputModel>()
                .ForMember(cs => cs.TotalBookViews, cfg => cfg
                    .MapFrom(s => s.BookViews.Count));
    }
}
