using MyShop.ApplicationCore.Entities;
using MyShop1.Pages.Basket;

namespace MyShop1.Interfaces
{
    public interface IBasketViewModelServicecs
    {
        Task<BasketViewModel> GetOnCreateBasketForUser(string userName);

        Task<int> CourtTotalBasketItems(string userName);

        Task<BasketViewModel> Map(Basket basket);
    }
}
