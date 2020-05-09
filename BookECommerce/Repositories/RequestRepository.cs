using BookECommerce.Models;
using BookECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookECommerce.Repositories
{
    #region Interface

    public interface IRequestRepository
    {
        Request GetRequest();

        void AddItem(string code);

        Request UpdateRegister(Register register);

        UpdateAmoutResponse UpdateAmout(RequestItem requestItem);
    }

    #endregion

    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        //para pegar o objeto armazenado em cache
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IRegisterRepository registerRepository;
        private readonly IRequestItemRepository requestItemRepository;

        #region Constructor

        public RequestRepository(ApplicationContext context,
                                    IHttpContextAccessor contextAccessor,
                                        IRequestItemRepository requestItemRepository,
                                                IRegisterRepository registerRepository) : base(context)
        {
            this.contextAccessor = contextAccessor;
            this.requestItemRepository = requestItemRepository;
            this.registerRepository = registerRepository;

        }
        
        #endregion

        #region Methods

        public void AddItem(string code)
        {
            var product = context.Set<Product>()
                .Where(p => p.Code == code).SingleOrDefault();

            if (product == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var request = GetRequest();

            var requestItem = context.Set<RequestItem>().
                Where(ri => ri.Product.Code == code
                 && ri.Request.ID == request.ID).SingleOrDefault();

            if (requestItem == null)
            {
                requestItem = new RequestItem(request, product, 1, product.Price);

                context.Set<RequestItem>().Add(requestItem);

                context.SaveChanges();
            }
        }

        public Request GetRequest()
        {
            //obter o id do produto gravado em cache
            var requestId = GetRquestId();

            var request = dbSet.Include(r => r.Items)
                .ThenInclude(i => i.Product)
                    .Include(r => r.Register)
                        .Where(r => r.ID == requestId)
                            .SingleOrDefault();

            if (request == null)
            {
                request = new Request();

                dbSet.Add(request);

                context.SaveChanges();

                //Para gravar o id na session
                SetRequestId(request.ID);
            }

            return request;
        }

        private int? GetRquestId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("requestId");
        }

        private void SetRequestId(int requestId)
        {
            contextAccessor.HttpContext.Session.SetInt32("requestId", requestId);
        }
        
        public UpdateAmoutResponse UpdateAmout(RequestItem requestItem)
        {
            var requestItemDB = requestItemRepository.GetRequestItem(requestItem.ID);

            if (requestItemDB != null)
            {
                requestItemDB.UpdateAmount(requestItem.Amount);

                if (requestItem.Amount == 0)
                {
                    requestItemRepository.RemoveRequestItem(requestItem.ID);
                }

                context.SaveChanges();

                var shoppingViewModel = new ShoppingViewModel(GetRequest().Items);

                return new UpdateAmoutResponse(requestItemDB, shoppingViewModel);
            }

            throw new ArgumentException("Item não encontrado");
        }

        public Request UpdateRegister(Register register)
        {
            var request = GetRequest();

            registerRepository.Update(request.Register.ID, register);

            return request;
        }

        #endregion
    }
}
