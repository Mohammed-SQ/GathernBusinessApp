using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    // Mark both properties as required so they can't be null after construction
    public class CategoryItem
    {
        public required string Image { get; set; }
        public required string Name { get; set; }
    }

    public partial class AddPropertyPage : ContentPage
    {
        public AddPropertyPage()
        {
            InitializeComponent();

            CategoryView.ItemsSource = new List<CategoryItem>
            {
                new CategoryItem { Image = "ga_resthouse", Name = "استراحة" },
                new CategoryItem { Image = "ga_chalet",    Name = "شاليه" },
                new CategoryItem { Image = "ga_apartment", Name = "شقة" },
                new CategoryItem { Image = "ga_villa",     Name = "فيلا" },
                new CategoryItem { Image = "ga_camp",      Name = "مخيم" },
                new CategoryItem { Image = "ga_studio",    Name = "استوديو" },
                new CategoryItem { Image = "ga_farm",      Name = "مزرعة" },
                new CategoryItem { Image = "ga_room",      Name = "غرفة" },
                new CategoryItem { Image = "ga_resort",    Name = "منتجع صحي / منتجع فندقي" }
            };
        }

        async void OnBackClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//MainPage");

        async void OnNextClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//BookingsPage");
    }
}
