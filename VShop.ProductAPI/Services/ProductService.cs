using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using VShop.ProductAPI.DTOs;
using VShop.ProductAPI.Models;
using VShop.ProductAPI.Repositories;

namespace VShop.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public ProductService(IProductRepository productRepository, IMapper mapper, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cache = cache; 
        }

        public async Task AddProduct(ProductDTO ProductDTO)
        {
            var productsEntity = _mapper.Map<Product>(ProductDTO);
            await _productRepository.Create(productsEntity);
            ProductDTO.Id = productsEntity.Id;
        }

        public async Task DeleteProduct(int id)
        {
            var ProductEntity = _productRepository.GetById(id).Result;
            await _productRepository.Delete(ProductEntity.Id);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            try
            {
                var productsEntity = await _productRepository.GetAll();
                return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<ProductDTO>> GetCategoriesProducts()
        {
            var productsEntity = await _productRepository.GetCategoriesProducts();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var productEntity = await _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task UpdateProduct(ProductDTO ProductDTO)
        {
            var ProductEntity = _mapper.Map<Product>(ProductDTO);
            await _productRepository.Update(ProductEntity);
        }
    }
}
