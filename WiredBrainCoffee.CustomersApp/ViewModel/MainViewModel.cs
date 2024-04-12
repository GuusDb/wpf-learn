using WiredBrainCoffee.CustomersApp.Command;

namespace WiredBrainCoffee.CustomersApp.ViewModel;

public class MainViewModel : ViewModelBase
{

    private ViewModelBase? _selectedViewModel;
    public CustomersViewModel CustomersViewModel { get; }
    public ProductsViewModel ProductsViewModel { get; }
    public ViewModelBase? SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand SelectViewModelCommand { get; }

    public MainViewModel(CustomersViewModel customersViewModel, ProductsViewModel productsViewModel)
    {
        CustomersViewModel = customersViewModel;
        ProductsViewModel = productsViewModel;
        SelectedViewModel = customersViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel);
    }

    public override async Task LoadAsync()
    {
        if (SelectedViewModel is not null)
            await SelectedViewModel.LoadAsync();

    }

    private async void SelectViewModel(object? paramter)
    {
        SelectedViewModel = paramter as ViewModelBase;
        await LoadAsync();
    }
}