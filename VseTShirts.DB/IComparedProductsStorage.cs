using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface IComparedProductsStorage
    {
        bool Add(string userId, Product product);
        bool Delete(string userId);
        List<Product> GetByUserId(string userId);
    }
}