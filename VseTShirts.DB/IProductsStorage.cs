using VseTShirts.DB.Models;

namespace VseTShirts
{
    public interface IProductsStorage
    {
        void Delete(Guid Id);
        Product GetById(Guid Id);
        void QuantitiReduce(Guid Id);
        void Add(Product product);
        void QuantityIncrease(Guid Id);
        void EditProduct(Guid Id, Product newProduct);
        List<Product> GetAll();
        Product GetByName(string name);
    }
}