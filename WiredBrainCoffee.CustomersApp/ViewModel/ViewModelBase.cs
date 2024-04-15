using CommunityToolkit.Mvvm.ComponentModel;

namespace WiredBrainCoffee.CustomersApp.ViewModel;

public abstract class ViewModelBase : ObservableObject
{
    public virtual Task LoadAsync() => Task.CompletedTask;


}