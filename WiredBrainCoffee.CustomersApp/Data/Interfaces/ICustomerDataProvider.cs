using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Data.Interfaces;

public interface ICustomerDataProvider
{
    Task<IEnumerable<Customer>?> GetAllAsync();
}