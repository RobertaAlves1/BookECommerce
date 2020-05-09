using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BookECommerce.Models
{
    [DataContract]
    public class RequestItem : BaseModel
    {
        #region Constructor

        public RequestItem() { }

        public RequestItem(Request request, Product product, int amount, decimal unitPrice)
        {
            Request = request;
            Product = product;
            Amount = amount;
            UnitPrice = unitPrice;
        }

        #endregion

        #region Properties

        [Required, DataMember]
        public Request Request { get; private set; }

        [Required, DataMember]
        public Product Product { get; private set; }

        [Required, DataMember]
        public int Amount { get; private set; }

        [Required, DataMember]
        public decimal UnitPrice { get; private set; }

        [DataMember]
        public decimal Subtotal
        {
            get
            {
                return Amount * UnitPrice;
            }
        }

        #endregion

        #region Methods

        internal void UpdateAmount(int amount)
        {
            Amount = amount;
        }

        #endregion
    }
}
