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
                var newAccount = new Account
                {
                    AccountName = EntName.Text,
                    ParentCode = 101,
                    AccountType = "Sub",
                    Balance = decimal.Parse(EntOpeningBalance.Text ?? "0")
                };
                _context.Accounts.Add(newAccount);
                await _context.SaveChangesAsync();

                var newFarmer = new Farmer
                {
                    FullName = EntName.Text,
                    PhoneNumber = EntPhone.Text,
                    Address = EntAddress.Text,
                    AccountId = newAccount.AccountCode
                };
                _context.Farmers.Add(newFarmer);
                await _context.SaveChangesAsync();
                await DisplayAlert("تم بنجاح", $"تم تسجيل المزارع وفتح حساب مالي له برقم {newAccount.AccountCode}", "موافق");
                await Shell.Current.GoToAsync("//main");
            }
            catch (Exception ex)
            {
                var msg = ex.GetBaseException()?.Message ?? ex.Message;
                await DisplayAlert("خطأ", "فشل الربط: " + msg, "موافق");
            }
        }
        // زر العودة للرئيسية
        private async void OnBackToHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }
    }
}
