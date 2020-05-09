using System;
using System.Linq;
using BookECommerce.Models;

namespace BookECommerce.Repositories
{
    #region Interface

    public interface IRegisterRepository
    {
        Register Update(int registerId, Register newRegister);  
    }

    #endregion

    public class RegisterRepository : BaseRepository<Register>, IRegisterRepository
    {
        #region Constructor

        public RegisterRepository(ApplicationContext context) : base(context) { }

        #endregion

        #region Methods

        public Register Update(int registerId, Register newRegister)
        {
            var registerDB = dbSet.Where(c => c.ID == registerId).SingleOrDefault();
            if (registerDB == null)
            {
                throw new ArgumentNullException("cadastro");
            }
            registerDB.Update(newRegister);
            context.SaveChanges();

            return registerDB;
        }

        #endregion
    }
}
