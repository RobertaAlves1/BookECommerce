using System.ComponentModel.DataAnnotations;

namespace BookECommerce.Models
{
    public class Product : BaseModel
    {
        #region Constructor

        public Product() { }

        public Product(string code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        #endregion

        #region Properties

        [Required]
        public string Code { get; private set; }

        [Required]
        public string Name { get; private set; }

        [Required]
        public decimal Price { get; private set; }

        #endregion
    }
}
