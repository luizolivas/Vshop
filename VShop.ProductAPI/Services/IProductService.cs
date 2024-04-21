using VShop.ProductAPI.DTOs;

namespace VShop.ProductAPI.Services
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDTO>> GetProducts();

        Task<IEnumerable<ProductDTO>> GetCategoriesProducts();

        Task<ProductDTO> GetProductById(int id);

        Task AddProduct(ProductDTO ProductDTO);

        Task DeleteProduct(int id);

        Task UpdateProduct(ProductDTO ProductDTO);
    }
}
