using Core.Entities;
namespace Core.Specifications
{
    public class ProductsWithTypeAndBrandSpecifications: BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecifications()
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}