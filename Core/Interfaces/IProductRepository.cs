using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        
        //Ireadonly is more specific about a list 
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductType>> GetProductsTypesAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync();
    }
}
