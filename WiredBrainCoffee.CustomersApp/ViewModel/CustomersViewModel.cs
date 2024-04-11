using System.Collections.ObjectModel;
using System.Windows.Controls;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Data.Interfaces;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel;

public class CustomersViewModel : ViewModelBase
{
    private readonly ICustomerDataProvider _customerDataProvider;
    private CustomerItemViewModel? _selectedCustomer;

    private NavigationSide _NavigationSide;
    public NavigationSide NavigationSide
    {
        get => _NavigationSide;
        private set
        {
            if (value == _NavigationSide) return;
            _NavigationSide = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<CustomerItemViewModel> Customers { get; set; } = new();

    public CustomerItemViewModel? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            if (Equals(value, _selectedCustomer)) return;
            _selectedCustomer = value;
            OnPropertyChanged();
        }
    }

    public CustomersViewModel(ICustomerDataProvider customerDataProvider)
    {
        _customerDataProvider = customerDataProvider;
    }

    public async Task LoadAsync()
    {
        if (Customers.Any())
        {
            return;
        }

        var customers = await _customerDataProvider.GetAllAsync();
        if (customers is not null)
        {
            foreach (var customer in customers)
            {
                Customers.Add(new CustomerItemViewModel(customer));
            }
        }
    }

    public void Add()
    {
        var customer = new CustomerItemViewModel(new Customer() { FirstName = "New" });
        Customers.Add(customer);
        SelectedCustomer = customer;
    }

    internal void MoveNavigation()
    {
        NavigationSide = NavigationSide == NavigationSide.Left ? NavigationSide.Right : NavigationSide.Left;
    }


}

public enum NavigationSide
{
    Left,
    Right
}