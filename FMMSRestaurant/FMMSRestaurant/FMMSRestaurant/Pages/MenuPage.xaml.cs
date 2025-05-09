using FMMSRestaurant.ViewModels;
using FMMSRestaurant.Models;
using Microsoft.Maui.Controls;

namespace FMMSRestaurant.Pages;

public partial class MenuPage : ContentPage
{
    private readonly MenuPageViewModel _viewModel;

    public MenuPage() : this(App.Current.Handler.MauiContext.Services.GetService<MenuPageViewModel>())
    {
    }

    public MenuPage(MenuPageViewModel viewModel)
    {
        try
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            BindingContext = _viewModel;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Exception in MenuPage constructor: {ex.Message}");
            throw;
        }
    }

    private void OnQuantityChanged(object sender, ValueChangedEventArgs e)
    {
        if (sender is Stepper stepper && stepper.BindingContext is FoodItem item)
        {
            int newQuantity = (int)e.NewValue;
            _viewModel.UpdateQuantity(item, newQuantity);
        }
    }
}