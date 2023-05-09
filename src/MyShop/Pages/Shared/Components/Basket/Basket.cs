using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Identity;
using MyShop1.Interfaces;
using MyShop1.Models;

namespace MyShop1.Pages.Shared.Components.BasketComponent
{
    public sealed class Basket : ViewComponent
    {
        private readonly IBasketViewModelServicecs _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Basket(IBasketViewModelServicecs basketService, SignInManager<ApplicationUser> signInManager)
        {
            _basketService = basketService;
            _signInManager = signInManager;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new BasketComponentViewModel { ItemsCount = 5 };

            return View(vm);
        }
    }
}
