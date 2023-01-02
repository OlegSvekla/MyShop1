namespace MyShop.ApplicationCore.Entities
{
    public sealed class CatalogBrand
    {
        //TODO Replace to GUID
        public int Id { get; set; }

        public string Brand { get; set; }

        public CatalogBrand(string brand)
        {
            brand = Brand;
        }

    }
}
