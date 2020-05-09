using BookECommerce.Models.ViewModels;

namespace BookECommerce.Models
{
    public class UpdateAmoutResponse
    {
        #region Properties

        public RequestItem RequestItem { get; }

        public ShoppingViewModel ShoppingViewModel { get; }

        #endregion

        #region Constructor

        public UpdateAmoutResponse(RequestItem requestItem, ShoppingViewModel shoppingViewModel)
        {
            RequestItem = requestItem;
            ShoppingViewModel = ShoppingViewModel;
        }

        #endregion
    }
}
