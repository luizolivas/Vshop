using VShop.ProductAPI.DTOs;

namespace VShop.ProductAPI.Services
{
    public interface ICategoryService
    {

        Task<IEnumerable<CategoryDTO>> GetCategories();

        Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();

        Task<IEnumerable<CategoryDTO>> GetCategoryById(int id);

        Task AddCategory(CategoryDTO categoryDTO);

        Task DeleteCategory(int id);

        Task UpdateCategory(CategoryDTO categoryDTO);


    }
}
