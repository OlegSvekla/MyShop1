using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using MyShop1.Interfaces;
using MyShop1.Models;
using MyShop1.Services;

namespace MyShop1.Controllers
{
    public class CatalogController : Controller
    {


        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly IRepository<CatalogItem> _catalogRepository;
        private readonly IBasketService _basketService;
        public CatalogController(IRepository<CatalogItem> catalogRepository,
            ICatalogItemViewModelService catalogItemViewModelService,
            IBasketService basketService)
        {
            //TODO: replace to IoC approach
            _catalogItemViewModelService = catalogItemViewModelService;
            _catalogRepository = catalogRepository;
            _basketService = basketService;
        }


        [HttpGet]
        public async Task <IActionResult> Index(int? brandFilterAppled, int? typesFilterAppled)
        {
            var userName = GetOrSetBasketCookieAndUserName();

            var viewModel = await _catalogItemViewModelService.GetCatalogItems(brandFilterAppled, typesFilterAppled);
 
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id) 
        {
            var userName = GetOrSetBasketCookieAndUserName();
            var basket = await _basketService.AddItem2Basket(userName);
            
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var item = _catalogRepository.GetById(id);
            if (item == null) return RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price,
            };

            return View(result);
        }


        [HttpGet]
        //GET : CatalogControllerEdit/5 
        public IActionResult Edit(int id)
        {
            var item = _catalogRepository.GetById(id);
            if (item == null) return RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price,
            };

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(CatalogItemViewModel catalogItemViewModel) 
        {
            try
            {
                _catalogItemViewModelService.UpdateCatalogItem(catalogItemViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View();
            }
        }

        private string GetOrSetBasketCookieAndUserName() 
        {
            string? userName = default;

            if (this.HttpContext.User.Identity.IsAuthenticated) 
            {
                return Request.HttpContext.User.Identity.Name;
            }

            if (this.Request.Cookies.ContainsKey("eShop")) 
            {
                userName = Request.Cookies["eShop"];

                if (!Request.HttpContext.User.Identity.IsAuthenticated)
                {
                    userName = default;
                }

            }
            if (userName != null) return userName;
            
            userName = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("eShop", userName, cookieOptions);
            return userName;
        }
    }
}