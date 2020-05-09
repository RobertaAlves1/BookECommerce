using System.IO;
using Newtonsoft.Json;
using BookECommerce.Models;
using BookECommerce.Repositories;
using System.Collections.Generic;

namespace BookECommerce
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext context;
        private readonly IProductRepository productRepository;

        #region Constructor

        public DataService(ApplicationContext context, IProductRepository productRepository)
        {
            this.context = context;
            this.productRepository = productRepository;
        }

        #endregion

        #region Methods

        public void InicializaDB()
        {
            context.Database.EnsureCreated();

            List<Book> books = GetBooks();

            productRepository.SaveProducts(books);
        }

        private List<Book> GetBooks()
        {
            var json = File.ReadAllText("books.json");

            var books = JsonConvert.DeserializeObject<List<Book>>(json);

            return books;
        }

        #endregion

    }
}
