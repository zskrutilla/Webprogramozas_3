using Task02.Models;

namespace Task02.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products = new();

        public ProductRepository()
        {
            products.Add(new Product { Name = "Laptop", Category = "Electronics", Price = 350000 });
            products.Add(new Product { Name = "Bread", Category = "Food", Price = 700 });
            products.Add(new Product { Name = "Notebook", Category = "Office", Price = 1200 });
            products.Add(new Product { Name = "C# Basics", Category = "Books", Price = 8500 });
        }

        public void Add(Product product)
        {
            if (product != null &&
                !string.IsNullOrWhiteSpace(product.Name) &&
                !string.IsNullOrWhiteSpace(product.Category))
            {
                products.Add(product);
            }
        }

        public List<Product> GetAll()
        {
            return products;
        }

        public List<Product> GetByCategory(string category)
        {
            return products
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
