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
        void DeleteAll();
        List<Product> Filtr(List<Product> products ,FiltersModel filters);
        List<Product> GetByCollection(string name);
        void RemoveCollectionFromProducts(string name);
        void RemoveCollectionFromProduct(string name, Guid productId);
        void DeleteProductFromCollection(Guid id, string collectionName);
        void AddProductToCollection(Guid id, string collectionName);
    }
}