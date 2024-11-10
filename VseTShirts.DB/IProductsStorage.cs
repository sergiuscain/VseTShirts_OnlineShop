using VseTShirts.DB.Models;

namespace VseTShirts
{
    public interface IProductsStorage
    {
        Task DeleteAsync(Guid Id);
        Task<Product> GetByIdAsync(Guid Id);
        Task QuantitiReduceAsync(Guid Id);
        Task AddAsync(Product product);
        Task QuantityIncreaseAsync(Guid Id);
        Task EditProductAsync(Guid Id, Product newProduct);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByNameAsync(string name);
        Task DeleteAllAsync();
        Task<List<Product>> FiltrAsync(List<Product> products ,FiltersModel filters);
        Task<List<Product>> GetByCollectionAsync(string name);
        Task RemoveCollectionFromProductsAsync(string name);
        Task RemoveCollectionFromProductAsync(string name, Guid productId);
        Task DeleteProductFromCollectionAsync(Guid id, string collectionName);
        Task AddProductToCollectionAsync(Guid id, string collectionName);
    }
}