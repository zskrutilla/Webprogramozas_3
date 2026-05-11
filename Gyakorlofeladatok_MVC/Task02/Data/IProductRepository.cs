using Task02.Models;

namespace Task02.Data
{
    public interface IProductRepository
    {
        void Add(Product product);
        List<Product> GetAll();
        List<Product> GetByCategory(string category);
    }
}
