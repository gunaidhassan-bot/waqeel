using System;
using Microsoft.Maui.Controls;
using waqeel.Models;

namespace waqeel
{
    public partial class AddFarmerPage : ContentPage
    {
        private readonly AppDbContext _context = new AppDbContext();

        public AddFarmerPage()
        {
            InitializeComponent();
        }

        private async void OnSaveFarmerClicked(object sender, EventArgs e)
        {
            try
            {
                // 1. إنشاء الحساب في الدليل المحاسبي أولاً
                var newAccount = new Account
                {
                    AccountName = EntName.Text,
                    ParentCode = 101, // افترضنا أن 101 هو رقم حساب "الأب: المزارعين"
                    AccountType = "Sub",
                    Balance = decimal.Parse(EntOpeningBalance.Text ?? "0")
                };

                _context.Accounts.Add(newAccount);
                await _context.SaveChangesAsync(); // نحفظ لكي نحصل على AccountCode إذا كان مُولدًا

                // 2. ربط المزارع بالحساب الذي أنشئ للتو
                var newFarmer = new Farmer
                {
                    FullName = EntName.Text,
                    PhoneNumber = EntPhone.Text,
                    Address = EntAddress.Text,
                    AccountId = newAccount.AccountCode
                };

                _context.Farmers.Add(newFarmer);
                await _context.SaveChangesAsync();

                await DisplayAlertAsync("تم بنجاح", $"تم تسجيل المزارع وفتح حساب مالي له برقم {newAccount.AccountCode}", "موافق");
                await Navigation.PopAsync(); // العودة للخلف
            }
            catch (Exception ex)
            {
                var msg = ex.GetBaseException()?.Message ?? ex.Message;
                await DisplayAlertAsync("خطأ", "فشل الربط: " + msg, "موافق");
            }
        }
    }
}
