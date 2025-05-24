using System;
using System.Collections.Generic;
using System.Linq;
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
        const int TotalSteps = 9;

        int _step = 1;
        int _bedrooms = 2;
        int _bathroomCount = 1;
        string? _propertyName;
        private HashSet<string> _selectedAmenities = new HashSet<string>();
        private CategoryItem? _selectedCategory;
        int _singleUnits = 1;
        const int MaxSingle = 20;
        int _masterUnits = 1;
        const int MaxMaster = 10;
        const int MaxBedrooms = 10;

        public AddPropertyPage()
        {
            InitializeComponent();
            LoadStep1Data();
            UpdateUI();
            InitializeAmenities();
            CategoryGrid.SelectionChanged += CategoryGrid_SelectionChanged;
        }

        void LoadStep1Data()
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

        private void InitializeAmenities()
        {
            _selectedAmenities.Clear();
            _selectedAmenities.Add("مسبح");
            _selectedAmenities.Add("مجلس");
            _selectedAmenities.Add("غرفة نوم");
            _selectedAmenities.Add("دورة مياه");
            _selectedAmenities.Add("مطبخ");
            _selectedAmenities.Add("بانيو / حوض استحمام");
            _selectedAmenities.Add("جاكوزي");
            _selectedAmenities.Add("سلبير");
            _selectedAmenities.Add("إطلالة على الجبل");
            _selectedAmenities.Add("اصنصير - مصاعد");
            _selectedAmenities.Add("انترنت");
            _selectedAmenities.Add("اطلاله على البحر");
            _selectedAmenities.Add("مدخل سيارة");
            _selectedAmenities.Add("شلال على الجبل");
            UpdateAmenityStates();
        }

        private void CategoryGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            _selectedCategory = e.CurrentSelection.FirstOrDefault() as CategoryItem;
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
                Shell.Current.GoToAsync("//main");
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
                if (_selectedCategory == null)
                {
                    DisplayAlert("خطأ", "يرجى اختيار تصنيف العقار", "موافق");
                    return;
                }
            }

            if (_step == 2)
            {
                if (string.IsNullOrEmpty(CityPicker.SelectedItem?.ToString()))
                {
                    DisplayAlert("خطأ", "يرجى اختيار المدينة", "موافق");
                    return;
                }
                if (string.IsNullOrEmpty(DirectionPicker.SelectedItem?.ToString()))
                {
                    DisplayAlert("خطأ", "يرجى اختيار الاتجاه", "موافق");
                    return;
                }
            }

            if (_step == 3)
            {
                if (string.IsNullOrWhiteSpace(PropertyAreaEntry.Text) || !int.TryParse(PropertyAreaEntry.Text, out int area) || area < 200)
                {
                    DisplayAlert("خطأ", "مساحة العقار يجب أن تكون 200 متر مربع على الأقل", "موافق");
                    return;
                }
                if (_selectedAmenities.Count == 0)
                {
                    DisplayAlert("خطأ", "يرجى اختيار على الأقل مرفق واحد", "موافق");
                    return;
                }
            }

            if (_step == 4)
            {
                if (_bedrooms < 1)
                {
                    DisplayAlert("خطأ", "يجب أن يكون عدد غرف النوم 1 على الأقل", "موافق");
                    return;
                }
                if (_singleUnits < 1)
                {
                    DisplayAlert("خطأ", "يجب أن يكون عدد الأسرة المفردة 1 على الأقل", "موافق");
                    return;
                }
                if (_masterUnits < 1)
                {
                    DisplayAlert("خطأ", "يجب أن يكون عدد الأسرة الرئيسية 1 على الأقل", "موافق");
                    return;
                }
            }

            if (_step == 5)
            {
                if (_bathroomCount < 1)
                {
                    DisplayAlert("خطأ", "يجب أن يكون عدد دورات المياه واحد على الأقل", "موافق");
                    return;
                }
                var bathroomAmenities = new[] { "بانيو / حوض استحمام", "جاكوزي", "دش", "شامبو", "صابون", "منايل", "رداء حمام", "ساونا", "سلبير" };
                if (!_selectedAmenities.Intersect(bathroomAmenities).Any())
                {
                    DisplayAlert("خطأ", "يرجى اختيار على الأقل مرفق واحد لدورات المياه", "موافق");
                    return;
                }
            }

            if (_step < TotalSteps)
            {
                _step++;
                UpdateUI();
            }
            else
            {
                Shell.Current.GoToAsync("//DashboardPage");
            }
        }

        private void OnIncrementBedroomsClicked(object sender, EventArgs e)
        {
            if (_bedrooms < MaxBedrooms)
            {
                _bedrooms++;
                BedroomsCountLabel.Text = _bedrooms.ToString();
            }
        }

        private void OnDecrementBedroomsClicked(object sender, EventArgs e)
        {
            if (_bedrooms > 0)
            {
                _bedrooms--;
                BedroomsCountLabel.Text = _bedrooms.ToString();
            }
        }

        private void OnIncrementSingleClicked(object sender, EventArgs e)
        {
            if (_singleUnits < MaxSingle)
            {
                _singleUnits++;
                SingleCountLabel.Text = _singleUnits.ToString();
            }
        }

        private void OnDecrementSingleClicked(object sender, EventArgs e)
        {
            if (_singleUnits > 0)
            {
                _singleUnits--;
                SingleCountLabel.Text = _singleUnits.ToString();
            }
        }

        private void OnIncrementMasterClicked(object sender, EventArgs e)
        {
            if (_masterUnits < MaxMaster)
            {
                _masterUnits++;
                MasterCountLabel.Text = _masterUnits.ToString();
            }
        }

        private void OnDecrementMasterClicked(object sender, EventArgs e)
        {
            if (_masterUnits > 0)
            {
                _masterUnits--;
                MasterCountLabel.Text = _masterUnits.ToString();
            }
        }

        private void OnIncrementBathroomClicked(object sender, EventArgs e)
        {
            if (_bathroomCount < 20)
            {
                _bathroomCount++;
                BathroomCountLabel.Text = _bathroomCount.ToString();
            }
        }

        private void OnDecrementBathroomClicked(object sender, EventArgs e)
        {
            if (_bathroomCount > 0)
            {
                _bathroomCount--;
                BathroomCountLabel.Text = _bathroomCount.ToString();
            }
        }

        private void OnAmenityTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {
                var gesture = frame.GestureRecognizers.OfType<TapGestureRecognizer>().FirstOrDefault();
                if (gesture != null)
                {
                    var parameter = gesture.CommandParameter?.ToString();
                    string? amenityName = null;

                    if (!string.IsNullOrEmpty(parameter))
                    {
                        amenityName = parameter switch
                        {
                            "Banio" => "بانيو / حوض استحمام",
                            "Jacuzzi" => "جاكوزي",
                            "Shower" => "دش",
                            "Shampoo" => "شامبو",
                            "Soap" => "صابون",
                            "Towel" => "منايل",
                            "Bathrobe" => "رداء حمام",
                            "Sauna" => "ساونا",
                            "Slippers" => "سلبير",
                            "ExtraLight" => "إضاءة إضافية",
                            "KidsPlay" => "ألعاب أطفال",
                            "Projector" => "بروجكتر",
                            "PrimelMandi" => "برميل مندي",
                            "SandPlay" => "العاب رمليه",
                            "Billiards" => "بلياردو",
                            "Balcony" => "بلكونة",
                            "Playstation" => "بلايستيشن",
                            "RoyalTent" => "بيت شعر ملكي",
                            "Tent" => "بيت شعر",
                            "Arish" => "جلسة عريش",
                            "TV" => "تلفزيون",
                            "Sand" => "تزلج على الرمل",
                            "Barbecue" => "زحليقه",
                            "Grill" => "ركن شواء",
                            "Riada" => "رذاذ",
                            "Dining" => "صالة طعام",
                            "Speakers" => "سماعات",
                            "Lighting" => "ستيج مضيئ",
                            "BrideRoom" => "غرفة تجهيز عروس",
                            "Tennis" => "طاولة تنس",
                            "TwoRooms" => "قسمين",
                            "CinemaRoom" => "غرفة سينما",
                            "DriverRoom" => "غرفة سائقين",
                            "SeparateRooms" => "قسمين منفصلة",
                            "ConnectedRooms" => "قسمين متصلة بينهم باب",
                            "MassageChair" => "كرسي مساج",
                            "WomenPool" => "مسبح عام للنساء",
                            "SharedPool" => "مسبح عام مشترك",
                            "GreenArea" => "مسطح اخضر",
                            "ExternalAnnex" => "ملحق خارجي",
                            "BasketballCourt" => "ملعب كرة سله",
                            "VolleyballCourt" => "ملعب كرة طائرة",
                            "FootballCourt" => "ملعب كرة قدم",
                            "WoodFireplace" => "موقد حطب",
                            "Trampoline" => "نطيطه",
                            "GardenView" => "إطلالة على الحديقة",
                            "MountainView" => "إطلالة على الجبل",
                            "Elevator" => "اصنصير - مصاعد",
                            "Internet" => "انترنت",
                            "SelfCheckIn" => "دخول ذاتي",
                            "PrivateBeach" => "شاطئ خاص",
                            "SeaView" => "اطلاله على البحر",
                            "CarEntrance" => "مدخل سيارة",
                            "MountainWaterfall" => "شلال على الجبل",
                            "SecurityOffice" => "مكتب امن",
                            _ => null
                        };
                    }

                    if (string.IsNullOrEmpty(amenityName))
                    {
                        if (frame.Content is VerticalStackLayout stackLayout)
                        {
                            var labels = stackLayout.Children.OfType<Label>().ToList();
                            if (labels.Count >= 2)
                            {
                                var nameLabel = labels[1];
                                amenityName = nameLabel.Text;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(amenityName))
                    {
                        if (_selectedAmenities.Contains(amenityName))
                        {
                            _selectedAmenities.Remove(amenityName);
                            frame.BackgroundColor = _step == 5 || _step == 6 || _step == 7 ? Colors.Transparent : Color.FromArgb("#F5F5F5");
                            frame.BorderColor = _step == 5 || _step == 6 || _step == 7 ? Color.FromArgb("#6200EE") : Colors.Transparent;
                            if (frame.Content is VerticalStackLayout vStack)
                            {
                                foreach (var label in vStack.Children.OfType<Label>())
                                {
                                    label.TextColor = _step == 5 || _step == 6 || _step == 7 ? Color.FromArgb("#6200EE") : Colors.Black;
                                }
                            }
                            else if (frame.Content is HorizontalStackLayout hStack)
                            {
                                foreach (var label in hStack.Children.OfType<Label>())
                                {
                                    label.TextColor = Color.FromArgb("#6200EE");
                                }
                            }
                            else if (frame.Content is Label label)
                            {
                                label.TextColor = Color.FromArgb("#6200EE");
                            }
                        }
                        else
                        {
                            _selectedAmenities.Add(amenityName);
                            frame.BackgroundColor = Color.FromArgb("#6200EE");
                            frame.BorderColor = Colors.Transparent;
                            if (frame.Content is VerticalStackLayout vStack)
                            {
                                foreach (var label in vStack.Children.OfType<Label>())
                                {
                                    label.TextColor = Colors.White;
                                }
                            }
                            else if (frame.Content is HorizontalStackLayout hStack)
                            {
                                foreach (var label in hStack.Children.OfType<Label>())
                                {
                                    label.TextColor = Colors.White;
                                }
                            }
                            else if (frame.Content is Label label)
                            {
                                label.TextColor = Colors.White;
                            }
                        }
                    }
                }
            }
        }

        void UpdateUI()
        {
            Step1.IsVisible = false;
            Step2.IsVisible = false;
            Step3.IsVisible = false;
            Step4.IsVisible = false;
            Step5.IsVisible = false;
            Step6.IsVisible = false;
            Step7.IsVisible = false;
            Step8.IsVisible = false;
            Step9.IsVisible = false;

            switch (_step)
            {
                case 1:
                    Step1.IsVisible = true;
                    TitleLabel.Text = "معلومات العقار";
                    break;
                case 2:
                    Step2.IsVisible = true;
                    TitleLabel.Text = "عنوان العقار";
                    break;
                case 3:
                    Step3.IsVisible = true;
                    TitleLabel.Text = "تفاصيل العقار";
                    break;
                case 4:
                    Step4.IsVisible = true;
                    TitleLabel.Text = "تفاصيل غرف النوم";
                    break;
                case 5:
                    Step5.IsVisible = true;
                    TitleLabel.Text = "دورات المياه";
                    break;
                case 6:
                    Step6.IsVisible = true;
                    TitleLabel.Text = "المرافق الإضافية";
                    break;
                case 7:
                    Step7.IsVisible = true;
                    TitleLabel.Text = "مميزات العقار";
                    break;
                case 8:
                    Step8.IsVisible = true;
                    TitleLabel.Text = "وصف العقار";
                    break;
                case 9:
                    Step9.IsVisible = true;
                    TitleLabel.Text = "رفع الصور";
                    break;
            }

            var fill = (_step * 460d) / TotalSteps;
            ProgressStrokeFill.WidthRequest = fill;

            NextButton.Text = _step < TotalSteps ? "التالي" : "حفظ";
        }

        private void UpdateAmenityStates()
        {
            UpdateAmenityState(AmenityPool, "مسبح");
            UpdateAmenityState(AmenityParking, "مجلس");
            UpdateAmenityState(AmenityBed, "غرفة نوم");
            UpdateAmenityState(AmenityBathroom, "دورة مياه");
            UpdateAmenityState(AmenityKitchen, "مطبخ");
            UpdateAmenityState(AmenityBanio, "بانيو / حوض استحمام");
            UpdateAmenityState(AmenityJacuzzi, "جاكوزي");
            UpdateAmenityState(AmenityShower, "دش");
            UpdateAmenityState(AmenityShampoo, "شامبو");
            UpdateAmenityState(AmenitySoap, "صابون");
            UpdateAmenityState(AmenityTowel, "منايل");
            UpdateAmenityState(AmenityBathrobe, "رداء حمام");
            UpdateAmenityState(AmenitySauna, "ساونا");
            UpdateAmenityState(AmenitySlippers, "سلبير");
        }

        private void UpdateAmenityState(Frame frame, string amenityName)
        {
            if (frame != null)
            {
                if (_selectedAmenities.Contains(amenityName))
                {
                    frame.BackgroundColor = Color.FromArgb("#6200EE");
                    frame.BorderColor = Colors.Transparent;
                    if (frame.Content is VerticalStackLayout vStack)
                    {
                        foreach (var label in vStack.Children.OfType<Label>())
                        {
                            label.TextColor = Colors.White;
                        }
                    }
                    else if (frame.Content is HorizontalStackLayout hStack)
                    {
                        foreach (var label in hStack.Children.OfType<Label>())
                        {
                            label.TextColor = Colors.White;
                        }
                    }
                    else if (frame.Content is Label label)
                    {
                        label.TextColor = Colors.White;
                    }
                }
                else
                {
                    frame.BackgroundColor = _step == 5 || _step == 6 || _step == 7 ? Colors.Transparent : Color.FromArgb("#F5F5F5");
                    frame.BorderColor = _step == 5 || _step == 6 || _step == 7 ? Color.FromArgb("#6200EE") : Colors.Transparent;
                    if (frame.Content is VerticalStackLayout vStack)
                    {
                        foreach (var label in vStack.Children.OfType<Label>())
                        {
                            label.TextColor = Colors.Black;
                        }
                    }
                    else if (frame.Content is HorizontalStackLayout hStack)
                    {
                        foreach (var label in hStack.Children.OfType<Label>())
                        {
                            label.TextColor = Color.FromArgb("#6200EE");
                        }
                    }
                    else if (frame.Content is Label label)
                    {
                        label.TextColor = Color.FromArgb("#6200EE");
                    }
                }
            }
        }
    }
}