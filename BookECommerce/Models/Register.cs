using System.ComponentModel.DataAnnotations;

namespace BookECommerce.Models
{
    public class Register : BaseModel
    {
        #region Constructor

        public Register() { }
        
        #endregion

        #region Properties

        public virtual Request Request { get; set; }

        [MinLength(5, ErrorMessage = "Nome deve ter no mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "Nome deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Phone { get; set; } = "";

        [Required(ErrorMessage = "Endereco é obrigatório")]
        public string Address { get; set; } = "";

        [Required(ErrorMessage = "Complemento é obrigatório")]
        public string Supplement { get; set; } = "";

        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string Neighborhood { get; set; } = "";

        [Required(ErrorMessage = "Município é obrigatório")]
        public string City { get; set; } = "";

        [Required(ErrorMessage = "UF é Obrigatório")]
        public string FederatedUnit { get; set; } = "";

        [Required(ErrorMessage = "CEP é obrigatório")]
        public string ZipCode { get; set; } = "";

        #endregion

        #region Methods

        internal void Update(Register newRegister)
        {
            this.Name = newRegister.Name;
            this.Email = newRegister.Email;
            this.Phone = newRegister.Phone;
            this.Address = newRegister.Address;
            this.Supplement = newRegister.Supplement;
            this.Neighborhood = newRegister.Neighborhood;
            this.City = newRegister.City;
            this.FederatedUnit = newRegister.FederatedUnit;
            this.ZipCode = newRegister.ZipCode;
        }

        #endregion
    }
}
