using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Data.Interfaces;

public interface IProductDataProvider
{
    Task<IEnumerable<Product>?> GetAllAsync();
}