using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WiredBrainCoffee.CustomersApp.Command;
using WiredBrainCoffee.CustomersApp.Data.Interfaces;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel;

public partial class CustomersViewModel : ViewModelBase
{
    private readonly ICustomerDataProvider _customerDataProvider;

    [ObservableProperty]
    private NavigationSide navigationSide;
    public ObservableCollection<CustomerItemViewModel> Customers { get; set; } = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    [NotifyPropertyChangedFor(nameof(IsCustomerSelected))]
    private CustomerItemViewModel? _selectedCustomer;

    public bool IsCustomerSelected => SelectedCustomer is not null;

    public CustomersViewModel(ICustomerDataProvider customerDataProvider)
    {
        _customerDataProvider = customerDataProvider;
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

    [RelayCommand]
    private void Add(Object? parameter)
    {
        var customer = new CustomerItemViewModel(new Customer() { FirstName = "New" });
        Customers.Add(customer);
        SelectedCustomer = customer;
    }

    [RelayCommand]
    private void MoveNavigation(Object? parameter)
    {
        NavigationSide = NavigationSide == NavigationSide.Left ? NavigationSide.Right : NavigationSide.Left;
    }

    private bool CanDelete => SelectedCustomer is not null;


    [RelayCommand(CanExecute = nameof(CanDelete))]
    private void Delete(object? parameter)
    {
        if (SelectedCustomer is not null)
        {
            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
        }
    }
}

public enum NavigationSide
{
    Left,
    Right
}