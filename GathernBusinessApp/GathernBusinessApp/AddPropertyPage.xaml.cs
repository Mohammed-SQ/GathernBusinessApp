using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public class CategoryItem
    {
        public string Image { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    public partial class AddPropertyPage : ContentPage
    {
        private int _step = 1;
        private readonly int _maxSteps = 8;
        private int _bedroomCount = 2;
        private int _bathroomCount = 2;
        private string _propertyName;

        public AddPropertyPage()
        {
            InitializeComponent();
            LoadStep1Data();
            UpdateUI();
        }

        private void LoadStep1Data()
        {
            CategoryGrid.ItemsSource = new List<CategoryItem>
            {
                new() { Image = "ga_resthouse.jpg", Name = "استراحة" },
                new() { Image = "ga_chalet.jpg", Name = "شاليه" },
                new() { Image = "ga_apartment.jpg", Name = "شقة" },
                new() { Image = "ga_villa.jpg", Name = "فيلا" },
                new() { Image = "ga_camp.jpg", Name = "مخيم" },
                new() { Image = "ga_studio.jpg", Name = "استوديو" },
                new() { Image = "ga_farm.jpg", Name = "مزرعة" },
                new() { Image = "ga_room.jpg", Name = "غرفة" },
                new() { Image = "ga_resort.jpg", Name = "منتجع فندقي" }
            };
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            if (_step > 1)
            {
                _step--;
                UpdateUI();
            }
            else
            {
                Shell.Current.GoToAsync("//MainPage");
            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (_step == 1)
            {
                _propertyName = PropertyNameEntry.Text;
                if (string.IsNullOrWhiteSpace(_propertyName))
                {
                    DisplayAlert("خطأ", "يرجى إدخال اسم العقار", "موافق");
                    return;
                }
            }

            if (_step < _maxSteps)
            {
                _step++;
                UpdateUI();
            }
            else
            {
                Shell.Current.GoToAsync("//BookingsPage");
            }
        }

        private void OnSkipClicked(object sender, EventArgs e)
        {
            _step++;
            if (_step > _maxSteps) _step = _maxSteps;
            UpdateUI();
        }

        private void OnIncrementBedroomClicked(object sender, EventArgs e)
        {
            _bedroomCount++;
            BedroomCountLabel.Text = _bedroomCount.ToString();
        }

        private void OnDecrementBedroomClicked(object sender, EventArgs e)
        {
            if (_bedroomCount > 0)
            {
                _bedroomCount--;
                BedroomCountLabel.Text = _bedroomCount.ToString();
            }
        }

        private void OnIncrementBathroomClicked(object sender, EventArgs e)
        {
            _bathroomCount++;
            BathroomCountLabel.Text = _bathroomCount.ToString();
        }

        private void OnDecrementBathroomClicked(object sender, EventArgs e)
        {
            if (_bathroomCount > 0)
            {
                _bathroomCount--;
                BathroomCountLabel.Text = _bathroomCount.ToString();
            }
        }

        private void UpdateUI()
        {
            // Hide all steps
            Step1.IsVisible = false;
            Step2.IsVisible = false;
            Step3.IsVisible = false;
            Step4.IsVisible = false;
            Step5.IsVisible = false;
            Step6.IsVisible = false;
            Step7.IsVisible = false;
            Step8.IsVisible = false;

            // Show current step
            switch (_step)
            {
                case 1: Step1.IsVisible = true; TitleLabel.Text = "معلومات العقار"; break;
                case 2: Step2.IsVisible = true; TitleLabel.Text = "عنوان العقار"; break;
                case 3: Step3.IsVisible = true; TitleLabel.Text = "تفاصيل العقار"; break;
                case 4: Step4.IsVisible = true; TitleLabel.Text = "تفاصيل غرف النوم"; break;
                case 5: Step5.IsVisible = true; TitleLabel.Text = "دورات المياه"; break;
                case 6: Step6.IsVisible = true; TitleLabel.Text = "المرافق الإضافية"; break;
                case 7: Step7.IsVisible = true; TitleLabel.Text = "مميزات العقار"; break;
                case 8: Step8.IsVisible = true; TitleLabel.Text = "وصف العقار"; break;
            }

            // Update progress line
            ProgressLine.WidthRequest = (_step * 500) / _maxSteps;

            // Toggle buttons
            BackButton.IsVisible = _step > 1;
            NextButton.Text = _step < _maxSteps ? "التالي" : "حفظ";
        }
    }
}