using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using MyShop1.Interfaces;
using MyShop1.Models;

namespace MyShop1.Services
{
    public sealed class CatalogItemViewModelService : ICatalogItemViewModelService
    {

        private readonly IRepository<CatalogItem> _catalogItemRepository;
        private readonly IRepository<CatalogBrand> _brandRepository;
        private readonly IRepository<CatalogType> _typeRepository;
        private readonly IAppLogger<CatalogItemViewModelService> _logger;
        private readonly IUriComposer _uriComposer;

        public CatalogItemViewModelService(IRepository<CatalogItem> catalogItemRepository, 
            IAppLogger<CatalogItemViewModelService> logger,
            IRepository<CatalogBrand> brandRepository,
            IRepository<CatalogType> typeRepository, IUriComposer uriComposer)
        {

            _catalogItemRepository = catalogItemRepository;
            _logger = logger;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _uriComposer = uriComposer;
        }

       

        public async Task<CatalogIndexViewModel> GetCatalogItems(int? brandId, int? typeId)
        {
            var entities = await _catalogItemRepository.GetAllAsync();

            var catalogItems = entities
                .Where(item => (!brandId.HasValue || item.CatalogBrandId == brandId 
                                && (!typeId.HasValue || item.CatalogTypeId == typeId)))
                .Select(item => new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = _uriComposer.ComposeImageUri(item.PictureUrl),
                Price = item.Price,
            }).ToList();

            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = catalogItems,
                Brands = (await GetBrands()).ToList(),
                Types = (await GetTypes()).ToList(),
            };

            return vm;
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typrId)
        {
            return await GetCatalogItems(brandId, typrId);
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            _logger.LogInformation("Get Brands called");
            var brands = await _brandRepository.GetAllAsync();

            var items = brands
                .Select(brand => new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand })
                .OrderBy(brand=>brand.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };

            items.Insert(0, allItem);

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            _logger.LogInformation("Get Types called");
            var types = await _typeRepository.GetAllAsync();

            var items = types
                .Select(_ => new SelectListItem() { Value = _.Id.ToString(), Text = _.Type })
                .OrderBy(_ => _.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };

            items.Insert(0, allItem);

            return items;
        }

        

        public void UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            var existingCatalogItem = _catalogItemRepository.GetById(viewModel.Id);
            if (existingCatalogItem is null)
            {

                var exception = new Exception($"Catalog item{viewModel.Id} was not found");
                _logger.LogError(exception, exception.Message);
                throw exception;

            }
                CatalogItem.CatalogItemDetails details = new(viewModel.Name, viewModel.Price);
            existingCatalogItem.UpdateDetails(details);
            _logger.LogInformation($"Updating catalog item {existingCatalogItem.Id}" +
                $"Name {existingCatalogItem.Name}" +
                $"Price {existingCatalogItem.Price}");
            _catalogItemRepository.Update(existingCatalogItem);
        }

       
    }
}