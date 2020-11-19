using System.Threading.Tasks;
using Core.Entities;
using System.Collections.Generic;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams);
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<int> GetProductCountAsync();
    }
}