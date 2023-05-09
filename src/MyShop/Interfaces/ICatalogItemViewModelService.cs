using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop1.Models;

namespace MyShop1.Interfaces
{
    public interface ICatalogItemViewModelService
    {
        void UpdateCatalogItem(CatalogItemViewModel viewModel);

        Task<CatalogIndexViewModel> GetCatalogItems(int? brandId, int? typrId);

        Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage ,int? brandId, int? typrId);

        Task<IEnumerable<SelectListItem>> GetBrands();

        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
