using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FMMSRestaurant.Models;
using System.Collections.ObjectModel;

namespace FMMSRestaurant.ViewModels;

public partial class MenuTableViewModel : ObservableObject
{
    [ObservableProperty]
    private string customerName = "Mohammed Nashiri";

    [ObservableProperty]
    private ObservableCollection<Models.MenuItem> menuItems = new();

    [ObservableProperty]
    private ObservableCollection<Table> tables = new();

    [ObservableProperty]
    private Table? selectedTable;

    public MenuTableViewModel()
    {
        menuItems.Add(new Models.MenuItem { Name = "Burger", Price = 10.99m });
        menuItems.Add(new Models.MenuItem { Name = "Pizza", Price = 12.99m });
        tables.Add(new Table { TableNumber = "Table 1" });
        tables.Add(new Table { TableNumber = "Table 2" });
    }

    [RelayCommand]
    void AddToOrder(Models.MenuItem item)
    {
        // Logic to add item to order
    }
}