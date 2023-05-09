using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop1.Interfaces;
using MyShop1.Models;

namespace MyShop1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;

        public IndexModel(ICatalogItemViewModelService catalogItemViewModelService)
        {
            _catalogItemViewModelService = catalogItemViewModelService;
        }
        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();

        public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
        {
            CatalogModel = await _catalogItemViewModelService.GetCatalogItems(pageId ?? 0, Constants.ITEMS_PER_PAGE, catalogModel.BrandFilterAppled, catalogModel.TypesFilterAppled);
        }
    }
}
