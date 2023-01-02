using MyShop1.Models;

namespace MyShop1.Interfaces
{
    public interface ICatalogItemViewModelService
    {
        void UpdateCatalogItem(CatalogItemViewModel viewModel);

        Task<IEnumerable<CatalogItemViewModel>> GetCatalogItem();
    }
}
