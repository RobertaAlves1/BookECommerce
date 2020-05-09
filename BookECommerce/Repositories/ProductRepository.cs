using System.Linq;
using BookECommerce.Models;
using System.Collections.Generic;

namespace BookECommerce.Repositories
{
    #region Interface
    
    public interface IProductRepository
    {
        void SaveProducts(List<Book> books);
        IList<Product> GetProducts();
    }

    #endregion

    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        #region Constructor

        public ProductRepository(ApplicationContext context) : base(context) { }

        #endregion

        #region Methods

        public IList<Product> GetProducts()
        {
            return dbSet.ToList();
        }

        public void SaveProducts(List<Book> books)
        {
            foreach (var book in books)
            {
                //any é um bool para retornar caso a condição seja atentida
                if (!dbSet.Where(p => p.Code == book.Code).Any())
                {
                    dbSet.Add(new Product(book.Code, book.Name, book.Price));
                }
            }
            context.SaveChanges();
        }

        #endregion
    }
}
