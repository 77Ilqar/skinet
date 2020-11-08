using Core.Interfaces;
using System.Threading.Tasks;
using Core.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Specifications;
namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;

        }
        

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);

        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams)
        {
            if (productParams.Sort == "priceAsc")
            {
                return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .OrderBy(p => p.Price)
                .Skip(productParams.PageSize * (productParams.PageIndex - 1))
                .Take(productParams.PageSize)
                .ToListAsync();
            }
            else if (productParams.Sort == "priceDesc")
            {
                return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .OrderByDescending(p => p.Price)
                .Skip(productParams.PageSize * (productParams.PageIndex - 1))
                .Take(productParams.PageSize)
                .ToListAsync();
            }
            else if(productParams.TypeId.HasValue)
            {
                return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .Where(p => p.ProductTypeId == productParams.TypeId)
                .OrderBy(p => p.Name)
                .Skip(productParams.PageSize * (productParams.PageIndex - 1))
                .Take(productParams.PageSize)
                .ToListAsync();
            }
            else if(productParams.BrandId.HasValue)
            {
                return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .Where(p => p.ProductBrandId == productParams.BrandId)
                .OrderBy(p => p.Name)
                .Skip(productParams.PageSize * (productParams.PageIndex - 1))
                .Take(productParams.PageSize)
                .ToListAsync();
            }
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .OrderBy(p => p.Name)
                .Skip(productParams.PageSize * (productParams.PageIndex - 1))
                .Take(productParams.PageSize)
                .ToListAsync();

        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes
            .OrderBy(t => t.Id)
            .ToListAsync();

        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands
            .OrderBy(b => b.Id)
            .ToListAsync();

        }
        
        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }
    }
}