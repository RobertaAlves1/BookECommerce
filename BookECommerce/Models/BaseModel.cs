using System.Runtime.Serialization;

namespace BookECommerce.Models
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember]
        public int ID { get; protected set; }
    }
}
