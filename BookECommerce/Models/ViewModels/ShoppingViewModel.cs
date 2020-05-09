using System.Linq;
using System.Collections.Generic;

namespace BookECommerce.Models.ViewModels
{
    public class ShoppingViewModel
    {
        #region Properties

        public IList<RequestItem> Items { get; }

        #endregion

        #region Constructor

        public ShoppingViewModel(IList<RequestItem> items)
        {
            Items = items;  
        }

        #endregion

        #region Methods

        public decimal Total => Items.Sum(i => i.Amount * i.UnitPrice);

        #endregion
    }
}
