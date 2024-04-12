﻿using System.Collections.ObjectModel;
using WiredBrainCoffee.CustomersApp.Data.Interfaces;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel;

public class ProductsViewModel : ViewModelBase
{
    private readonly IProductDataProvider _productDataProvider;

    public ObservableCollection<Product> Products { get; } = new();

    public ProductsViewModel(IProductDataProvider productDataProvider)
    {
        _productDataProvider = productDataProvider;
    }

    public override async Task LoadAsync()
    {
        if (Products.Any())
            return;

        var products = await _productDataProvider.GetAllAsync();

        if (products is null)
            return;

        foreach (var product in products)
        {
            Products.Add(product);
        }

    }
}