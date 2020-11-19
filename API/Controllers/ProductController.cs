using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using System;
using API.Helpers;
namespace API.Controllers
{
    public class ProductController: BaseApiController
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepo,
                                 IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {   
            // var products = await _productRepo.GetProductsAsync(productParams);
            //var productCount = products.Count();

            var products = await _productRepo.GetProductsAsync(productParams);
            var productsCount = products.Count();
            var totalItems = await _productRepo.GetProductCountAsync();
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // var product =await  _repo.GetByIdAsync(id);
            var product = await _productRepo.GetProductByIdAsync(id);
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            // var producttypes = await _typeRepo.ListAllAsync();
            var producttypes = await _productRepo.GetProductTypesAsync(); 
            return Ok(producttypes);
        }
        
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productbrands = await _productRepo.GetProductBrandsAsync();
            // var productbrands = await _brandRepo.ListAllAsync();
            return Ok(productbrands);
        }
    }
}