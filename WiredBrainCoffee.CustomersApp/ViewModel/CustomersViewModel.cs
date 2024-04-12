using System.Collections.ObjectModel;
using WiredBrainCoffee.CustomersApp.Command;
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
            OnPropertyChanged(nameof(IsCustomerSelected));
            DeleteCommand.RaiseCanExecuteChanged();
        }
    }

    public bool IsCustomerSelected => SelectedCustomer is not null;

    public DelegateCommand AddCommand { get; set; }
    public DelegateCommand MoveNavigationCommand { get; set; }
    public DelegateCommand DeleteCommand { get; set; }

    public CustomersViewModel(ICustomerDataProvider customerDataProvider)
    {
        _customerDataProvider = customerDataProvider;
        AddCommand = new DelegateCommand(Add);
        MoveNavigationCommand = new DelegateCommand(MoveNavigation);
        DeleteCommand = new DelegateCommand(Delete, CanDelete);
    }


    public override async Task LoadAsync()
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

    private void Add(Object? parameter)
    {
        var customer = new CustomerItemViewModel(new Customer() { FirstName = "New" });
        Customers.Add(customer);
        SelectedCustomer = customer;
    }

    private void MoveNavigation(Object? parameter)
    {
        NavigationSide = NavigationSide == NavigationSide.Left ? NavigationSide.Right : NavigationSide.Left;
    }

    private void Delete(object? parameter)
    {
        if (SelectedCustomer is not null)
        {
            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
        }
    }

    private bool CanDelete(object? parameter) => SelectedCustomer is not null;


}

public enum NavigationSide
{
    Left,
    Right
}