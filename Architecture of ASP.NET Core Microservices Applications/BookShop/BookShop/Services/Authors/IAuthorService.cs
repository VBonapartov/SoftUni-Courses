namespace BookShop.Services.Authors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Services.Models.Authors;

    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDetailsServiceModel>> All();

        Task<AuthorDetailsServiceModel> Details(int id);

        Task<IEnumerable<AuthorListingServiceModel>> List();

        Task<int> Create(string firstName, string lastName);        

        Task<bool> Exists(int id);
    }
}