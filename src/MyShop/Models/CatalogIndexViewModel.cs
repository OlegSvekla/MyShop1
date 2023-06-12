using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyShop1.Models
{
    public sealed class CatalogIndexViewModel
    {
        public List<CatalogItemViewModel>? CatalogItems { get; set; }

        public List<SelectListItem>? Brands{ get; set; }

        public List<SelectListItem>? Types { get; set; }

        public int? BrandFilterAppled { get; set; }

        public int? TypesFilterAppled { get; set; }
    }
}