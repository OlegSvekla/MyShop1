using MyShop.ApplicationCore.Entities;
using MyShop1.Interfaces;
using MyShop1.Pages.Basket;

namespace MyShop1.Services
{
    public class BasketViewModelServicecs : IBasketViewModelServicecs
    {
        public Task<int> CourtTotalBasketItems(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<BasketViewModel> GetOnCreateBasketForUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<BasketViewModel> Map(Basket basket)
        {
            throw new NotImplementedException();
        }
    }
}
