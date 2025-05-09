using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FMMSRestaurant.Models;
using Microsoft.Maui.Controls;

namespace FMMSRestaurant.ViewModels;

public class MenuPageViewModel : INotifyPropertyChanged
{
    private ObservableCollection<FoodItem> _foodItems;
    private ObservableCollection<FoodItem> _filteredFoodItems;
    private ObservableCollection<FoodItem> _orderItems;
    private string _selectedCategory = "All";
    private string _orderNotes;

    public ObservableCollection<FoodItem> FoodItems
    {
        get => _foodItems;
        set
        {
            _foodItems = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<FoodItem> FilteredFoodItems
    {
        get => _filteredFoodItems;
        set
        {
            _filteredFoodItems = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<FoodItem> OrderItems
    {
        get => _orderItems;
        set
        {
            _orderItems = value;
            OnPropertyChanged();
        }
    }

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            _selectedCategory = value;
            OnPropertyChanged();
            FilterFoodItems();
        }
    }

    public string OrderNotes
    {
        get => _orderNotes;
        set
        {
            _orderNotes = value;
            OnPropertyChanged();
        }
    }

    public Command<string> SelectCategoryCommand { get; }
    public Command<FoodItem> AddToOrderCommand { get; }
    public Command<FoodItem> UpdateQuantityCommand { get; }
    public Command PlaceOrderCommand { get; }
    public Command<string> NavigateCommand { get; } // Added NavigateCommand

    public event PropertyChangedEventHandler? PropertyChanged;

    public MenuPageViewModel()
    {
        _foodItems = new ObservableCollection<FoodItem>();
        _filteredFoodItems = new ObservableCollection<FoodItem>();
        _orderItems = new ObservableCollection<FoodItem>();
        _orderNotes = ""; // Initialize to avoid CS8618 warning
        SelectCategoryCommand = new Command<string>(category => SelectedCategory = category);
        AddToOrderCommand = new Command<FoodItem>(AddToOrder);
        UpdateQuantityCommand = new Command<FoodItem>(item => UpdateQuantity(item, 0)); // For remove button
        PlaceOrderCommand = new Command(PlaceOrder);
        NavigateCommand = new Command<string>(async (route) => await Shell.Current.GoToAsync(route)); // Implement navigation
        LoadFoodItems();
        FilterFoodItems();
    }

    private void LoadFoodItems()
    {
        _foodItems.Clear();
        _foodItems.Add(new FoodItem { Name = "Samosa", Description = "Crispy pastry with spiced filling", Price = 2.50, Category = "Appetizer" });
        _foodItems.Add(new FoodItem { Name = "Spring Rolls", Description = "Fried rolls with veggies", Price = 3.00, Category = "Appetizer" });
        _foodItems.Add(new FoodItem { Name = "Grilled Chicken", Description = "Juicy grilled chicken breast", Price = 8.50, Category = "Main Course" });
        _foodItems.Add(new FoodItem { Name = "Pasta Alfredo", Description = "Creamy pasta with cheese", Price = 7.00, Category = "Main Course" });
        _foodItems.Add(new FoodItem { Name = "Burger", Description = "Beef patty with cheese", Price = 5.00, Category = "Fast Food" });
        _foodItems.Add(new FoodItem { Name = "French Fries", Description = "Crispy golden fries", Price = 2.00, Category = "Fast Food" });
        _foodItems.Add(new FoodItem { Name = "Nachos", Description = "Tortilla chips with cheese", Price = 3.50, Category = "Snacks" });
        _foodItems.Add(new FoodItem { Name = "Popcorn", Description = "Salted popcorn", Price = 2.00, Category = "Snacks" });
        _foodItems.Add(new FoodItem { Name = "Chocolate Cake", Description = "Rich chocolate dessert", Price = 4.00, Category = "Dessert" });
        _foodItems.Add(new FoodItem { Name = "Ice Cream", Description = "Vanilla ice cream scoop", Price = 3.00, Category = "Dessert" });
        _foodItems.Add(new FoodItem { Name = "Cola", Description = "Chilled cola drink", Price = 1.50, Category = "Beverages" });
        _foodItems.Add(new FoodItem { Name = "Orange Juice", Description = "Freshly squeezed juice", Price = 2.00, Category = "Beverages" });
    }

    private void FilterFoodItems()
    {
        FilteredFoodItems.Clear();
        var items = _selectedCategory == "All"
            ? _foodItems
            : _foodItems.Where(item => item.Category == _selectedCategory);
        foreach (var item in items)
        {
            FilteredFoodItems.Add(item);
        }
    }

    public void AddToOrder(FoodItem foodItem)
    {
        var existingItem = OrderItems.FirstOrDefault(item => item.Name == foodItem.Name);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            var newItem = new FoodItem
            {
                Name = foodItem.Name,
                Price = foodItem.Price,
                Quantity = 1
            };
            OrderItems.Add(newItem);
        }
        OnPropertyChanged(nameof(OrderItems));
    }

    public void UpdateQuantity(FoodItem item, int quantity)
    {
        if (quantity <= 0)
        {
            OrderItems.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }
        OnPropertyChanged(nameof(OrderItems));
    }

    public void PlaceOrder()
    {
        if (!OrderItems.Any())
        {
            return;
        }

        OrderItems.Clear();
        OrderNotes = string.Empty;
        OnPropertyChanged(nameof(OrderItems));
        OnPropertyChanged(nameof(OrderNotes));
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}