using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookECommerce.Models
{
    public class Request : BaseModel
    {
        #region Constructor

        public Request()
        {
            Register = new Register();
        }

        public Request(Register register)
        {
            Register = register;
        }

        #endregion

        #region Properties

        [Required]
        public virtual Register Register { get; set; }

        public List<RequestItem> Items { get; private set; } = new List<RequestItem>();

        #endregion
    }
}
