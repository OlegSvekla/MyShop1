﻿namespace MyShop1.Pages.Basket
{
    public sealed class BasketViewModel
    {
        public int Id { get; set; }

        public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();

        public string? BuyerId { get; set; }

        public decimal Total()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quanity), 2);
        }
    }
}
