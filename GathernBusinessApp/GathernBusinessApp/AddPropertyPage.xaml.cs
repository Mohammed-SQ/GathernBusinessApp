using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace GathernBusinessApp
{
    public class CategoryItem
    {
        public string Image { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    [QueryProperty("UserID", "userID")]
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
        private string _city = "";
        private string _direction = "";
        private int _area = 0;
        private string _description = "";

        private int _userID;
        public int UserID
        {
            get => _userID;
            set
            {
                int prefUserId = Preferences.Get("UserID", 0);
                _userID = (value > 0 && value != 1) ? value : prefUserId; // Prioritize valid query param, fallback to Preferences
                if (_userID <= 0)
                {
                    Shell.Current.GoToAsync("//SignInPage");
                    DisplayAlert("خطأ", "لم يتم العثور على معرف المستخدم. يرجى تسجيل الدخول مرة أخرى.", "موافق");
                }
            }
        }

        private readonly string _connectionString = "Data Source=SQL6032.site4now.net,1433;Initial Catalog=db_ab93cb_gathernapp;User Id=db_ab93cb_gathernapp_admin;Password=m1234567;";

        public AddPropertyPage()
        {
            InitializeComponent();
            LoadStep1Data();
            UpdateUI();
            InitializeAmenities();
            CategoryGrid.SelectionChanged += CategoryGrid_SelectionChanged;

            // Ensure UserID is set even if not passed via navigation
            if (_userID <= 0)
            {
                _userID = Preferences.Get("UserID", 0);
                if (_userID <= 0)
                {
                    Shell.Current.GoToAsync("//SignInPage");
                    DisplayAlert("خطأ", "لم يتم العثور على معرف المستخدم. يرجى تسجيل الدخول مرة أخرى.", "موافق");
                }
            }
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

        private async void OnNextClicked(object sender, EventArgs e)
        {
            if (_step == 1)
            {
                _propertyName = PropertyNameEntry.Text;
                if (string.IsNullOrWhiteSpace(_propertyName))
                {
                    await DisplayAlert("خطأ", "يرجى إدخال اسم العقار", "موافق");
                    return;
                }
                if (_selectedCategory == null)
                {
                    await DisplayAlert("خطأ", "يرجى اختيار تصنيف العقار", "موافق");
                    return;
                }
            }

            if (_step == 2)
            {
                _city = CityPicker.SelectedItem?.ToString() ?? "";
                _direction = DirectionPicker.SelectedItem?.ToString() ?? "";
                if (string.IsNullOrEmpty(_city))
                {
                    await DisplayAlert("خطأ", "يرجى اختيار المدينة", "موافق");
                    return;
                }
                if (string.IsNullOrEmpty(_direction))
                {
                    await DisplayAlert("خطأ", "يرجى اختيار الاتجاه", "موافق");
                    return;
                }
            }

            if (_step == 3)
            {
                if (!int.TryParse(PropertyAreaEntry.Text, out _area) || _area < 200)
                {
                    await DisplayAlert("خطأ", "مساحة العقار يجب أن تكون 200 متر مربع على الأقل", "موافق");
                    return;
                }
                if (_selectedAmenities.Count == 0)
                {
                    await DisplayAlert("خطأ", "يرجى اختيار على الأقل مرفق واحد", "موافق");
                    return;
                }
            }

            if (_step == 4)
            {
                if (_bedrooms < 1)
                {
                    await DisplayAlert("خطأ", "يجب أن يكون عدد غرف النوم 1 على الأقل", "موافق");
                    return;
                }
                if (_singleUnits < 1)
                {
                    await DisplayAlert("خطأ", "يجب أن يكون عدد الأسرة المفردة 1 على الأقل", "موافق");
                    return;
                }
                if (_masterUnits < 1)
                {
                    await DisplayAlert("خطأ", "يجب أن يكون عدد الأسرة الرئيسية 1 على الأقل", "موافق");
                    return;
                }
            }

            if (_step == 5)
            {
                if (_bathroomCount < 1)
                {
                    await DisplayAlert("خطأ", "يجب أن يكون عدد دورات المياه واحد على الأقل", "موافق");
                    return;
                }
                var bathroomAmenities = new[] { "بانيو / حوض استحمام", "جاكوزي", "دش", "شامبو", "صابون", "منايل", "رداء حمام", "ساونا", "سلبير" };
                if (!_selectedAmenities.Intersect(bathroomAmenities).Any())
                {
                    await DisplayAlert("خطأ", "يرجى اختيار على الأقل مرفق واحد لدورات المياة", "موافق");
                    return;
                }
            }

            if (_step == 8)
            {
                _description = PropertyDescription.Text;
                if (string.IsNullOrWhiteSpace(_description))
                {
                    await DisplayAlert("خطأ", "يرجى إدخال وصف للعقار", "موافق");
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
                await SavePropertyToDatabase();
                await Shell.Current.GoToAsync("//DashboardPage");
            }
        }

        private async Task SavePropertyToDatabase()
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                var amenities = string.Join(",", _selectedAmenities);
                var features = string.Join(",", new[] { "إطلالة على الحديقة", "إطلالة على الجبل", "اصنصير - مصاعد", "انترنت", "دخول ذاتي", "شاطئ خاص", "اطلاله على البحر", "مدخل سيارة", "شلال على الجبل", "مكتب امن" }
                    .Where(f => _selectedAmenities.Contains(f)));

                var query = @"INSERT INTO Properties (UserID, PropertyName, Category, City, Direction, Area, Bedrooms, SingleUnits, MasterUnits, Bathrooms, Amenities, Features, Description, Visibility) 
                              VALUES (@UserID, @PropertyName, @Category, @City, @Direction, @Area, @Bedrooms, @SingleUnits, @MasterUnits, @Bathrooms, @Amenities, @Features, @Description, @Visibility)";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", _userID);
                command.Parameters.AddWithValue("@PropertyName", _propertyName ?? "Unknown");
                command.Parameters.AddWithValue("@Category", _selectedCategory?.Name ?? "Unknown");
                command.Parameters.AddWithValue("@City", _city);
                command.Parameters.AddWithValue("@Direction", _direction);
                command.Parameters.AddWithValue("@Area", _area);
                command.Parameters.AddWithValue("@Bedrooms", _bedrooms);
                command.Parameters.AddWithValue("@SingleUnits", _singleUnits);
                command.Parameters.AddWithValue("@MasterUnits", _masterUnits);
                command.Parameters.AddWithValue("@Bathrooms", _bathroomCount);
                command.Parameters.AddWithValue("@Amenities", amenities);
                command.Parameters.AddWithValue("@Features", features);
                command.Parameters.AddWithValue("@Description", _description);
                command.Parameters.AddWithValue("@Visibility", "غير معروض");

                await command.ExecuteNonQueryAsync();
                Preferences.Set("HasCompletedBoarding", true); // Set HasCompletedBoarding to true after successful save
                await DisplayAlert("نجاح", "تم حفظ العقار بنجاح", "موافق");
            }
            catch (Exception ex)
            {
                await DisplayAlert("خطأ", $"فشل في حفظ العقار: {ex.Message}", "موافق");
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