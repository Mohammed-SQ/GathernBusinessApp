using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FMMSRestaurant.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace FMMSRestaurant.ViewModels;

public partial class StaffOrderViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Order> orders;

    public StaffOrderViewModel()
    {
        Orders = new ObservableCollection<Order>
        {
            new Order { Table = new Table { TableNumber = "Table 1" }, Items = "Burger, Pizza" }
        };
    }

    [RelayCommand]
    void MarkAsServed(Order order)
    {
        Orders.Remove(order);
    }
}