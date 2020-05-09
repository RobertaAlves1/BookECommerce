using BookECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using BookECommerce.Repositories;
using System.Collections.Generic;
using BookECommerce.Models.ViewModels;

namespace BookECommerce.Controllers
{
    public class RequestController : Controller
    {
        #region Injections

        private readonly IProductRepository productRepository;
        private readonly IRequestRepository requestRepository;
        private readonly IRegisterRepository registerRepository;
        private readonly IRequestItemRepository requestItemRepository;

        #endregion

        #region Constructor

        public RequestController(IProductRepository productRepository,
                                    IRequestRepository requestRepository,
                                         IRequestItemRepository requestItemRepository,
                                             IRegisterRepository registerRepository)
        {
            this.productRepository = productRepository;
            this.requestRepository = requestRepository;
            this.registerRepository = registerRepository;
            this.requestItemRepository = requestItemRepository;
        }

        #endregion

        #region Actions

        public IActionResult Carrossel()
        {
            return View(productRepository.GetProducts());
        }

        public IActionResult Shopping(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                requestRepository.AddItem(code);
            }

            Request request = requestRepository.GetRequest();
            List<RequestItem> items = request.Items;
            ShoppingViewModel shoppingViewModel = new ShoppingViewModel(items);

            //injetar na view os itens do repositório
            return base.View(shoppingViewModel);
        }

        public IActionResult Register()
        {
            var request = requestRepository.GetRequest();
            if (request == null)
            {
                return RedirectToAction("Carrossel");
            }

            return View(request.Register);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Summary(Register register)
        {
            if (ModelState.IsValid)
            {
                return View(requestRepository.UpdateRegister(register));
            }

            return RedirectToAction("Register");
        }

        #endregion

        #region Methods

        [HttpPost, ValidateAntiForgeryToken]
        public UpdateAmoutResponse UpdateAmout([FromBody]RequestItem requestItem)
        {
            return requestRepository.UpdateAmout(requestItem);
        }
        
        #endregion
    }
}