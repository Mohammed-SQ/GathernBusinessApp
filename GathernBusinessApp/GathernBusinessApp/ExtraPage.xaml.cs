using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace GathernBusinessApp;

public partial class ExtraPage : ContentPage
{
    public ObservableCollection<MenuItem> MenuItems { get; set; }

    public ExtraPage()
    {
        InitializeComponent();

        MenuItems = new ObservableCollection<MenuItem>
        {
            new MenuItem("الملف الشخصي", "profile_icon.png"),
            new MenuItem("التقييمات", "ratings_icon.png"),
            new MenuItem("المعاملات المالية", "transactions_icon.png"),
            new MenuItem("نظام السياحة الجديد", "tourism_icon.png"),
            new MenuItem("الاسعار", "price_icon.png"),
            new MenuItem("برنامج حماية المضيف", "protection_icon.png"),
            new MenuItem("الإعدادات", "settings_icon.png"),
            new MenuItem("مقترحات المضيفين", "suggestions_icon.png"),
            new MenuItem("المقالات", "articles_icon.png"),
            new MenuItem("البلاغات والشكاوي", "complaints_icon.png"),
            new MenuItem("طلبات التغيير", "change_requests_icon.png"),
            new MenuItem("رابط الإحالة وأكواد الخصم", "referral_icon.png"),
            new MenuItem("البرنامج التعريفي", "intro_icon.png"),
            new MenuItem("الفواتير و كشوف الحسابات", "invoices_icon.png"),
            new MenuItem("إتفاقية الإستخدام", "agreement_icon.png"),
            new MenuItem("لم توقع", "not_signed_icon.png"),
            new MenuItem("تواصل معنا نخدمك بعيوننا", "support_icon.png")
        };

        BindingContext = this;
    }

    private async void OnCalendarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CalendarPage");
    }

    private async void OnBookingClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//BookingPage");
    }

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DashboardPage");
    }

    private async void OnPropertyClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//PropertyPage");
    }
}

public class MenuItem
{
    public string Title { get; set; }
    public string Icon { get; set; }

    public MenuItem(string title, string icon)
    {
        Title = title;
        Icon = icon;
    }
}
