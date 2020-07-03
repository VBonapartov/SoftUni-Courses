namespace BookShop.Books.Services.Books
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Books.Services.Models.Books;

    public interface IBookService
    {
        Task<IEnumerable<BookListingServiceModel>> All();

        Task<BookDetailsServiceModel> Details(int id);

        Task<BookListingServiceModel> LessDetails(int id);

        Task<bool> Exists(int id);

        Task<int> Create(
                        string title,
                        string description,
                        decimal price,
                        int copies,
                        int? edition,
                        int? ageRestriction,
                        DateTime? releaseDate,
                        int authorId,
                        IEnumerable<int> categoriesId);

        Task Update(
                int id,
                string title,
                string description,
                decimal price,
                int copies,
                int? edition,
                int? ageRestriction,
                DateTime? releaseDate,
                int authorId,
                IEnumerable<int> categoriesId);

        Task Delete(int id);
    }
}