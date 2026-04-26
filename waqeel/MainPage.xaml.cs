namespace waqeel;

public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();

    // 1. الانتقال للدليل المحاسبي
    private async void OnAccountsClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new AccountsPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("تنبيه", "تأكد من وجود صفحة AccountsPage", "موافق");
        }
    }

    // 2. الانتقال لبيانات التحميل
    private async void OnPackingClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new beanatthmel());
        }
        catch (Exception ex)
        {
            await DisplayAlert("تنبيه", "تأكد من وجود صفحة beanatthmel", "موافق");
        }
    }

    // 3. الانتقال لإضافة مزارع
    private async void OnAddFarmerClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new AddFarmerPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("تنبيه", "تأكد من وجود صفحة AddFarmerPage", "موافق");
        }
    }
}
