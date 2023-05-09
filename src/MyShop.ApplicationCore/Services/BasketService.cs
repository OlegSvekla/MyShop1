using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ApplicationCore.Services
{
    public sealed class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;

        public BasketService(IRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> AddItem2Basket(string username)
        {
            //TODO check if basket is already exist for this user
            Basket basket = default;
            if (basket == null) 
            {
                basket = new Basket(username);
                basket = await _basketRepository.AddSync(basket);
            }

            return basket;
        }
    }
}
