using System.Linq;
using BookECommerce.Models;

namespace BookECommerce.Repositories
{
    #region Interface

    public interface IRequestItemRepository
    {
        RequestItem GetRequestItem(int requestItemId);

        void RemoveRequestItem(int requestItemId);

    }

    #endregion

    public class RequestItemRepository : BaseRepository<RequestItem>, IRequestItemRepository
    {
        #region Constructor

        public RequestItemRepository(ApplicationContext context) : base(context) { }

        #endregion

        #region Methods

        public RequestItem GetRequestItem(int requestItemId)
        {
            return dbSet.Where(ri => ri.ID == requestItemId).SingleOrDefault();
        }

        public void RemoveRequestItem(int requestItemId)
        {
            dbSet.Remove(GetRequestItem(requestItemId));
        }

        #endregion
    }
}
