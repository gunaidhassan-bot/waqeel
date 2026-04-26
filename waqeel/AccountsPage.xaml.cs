using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace waqeel
{
    public partial class AccountsPage : ContentPage
    {
        public AccountsPage()
        {
            InitializeComponent();
            SetEditMode(false);
        }

        private void SetEditMode(bool isEditing)
        {
            TxtName.IsReadOnly = !isEditing;
            PkrParent.IsEnabled = isEditing;
            PkrType.IsEnabled = isEditing;
            TxtName.BackgroundColor = isEditing ? Colors.White : Color.FromArgb("#F5F5F5");
            PkrParent.BackgroundColor = isEditing ? Colors.White : Color.FromArgb("#F5F5F5");
            PkrType.BackgroundColor = isEditing ? Colors.White : Color.FromArgb("#F5F5F5");
            BtnSave.IsEnabled = isEditing;
            BtnCancel.IsEnabled = isEditing;
            BtnNew.IsEnabled = !isEditing;
            BtnEdit.IsEnabled = !isEditing;
            BtnDelete.IsEnabled = !isEditing;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCode?.Text))
            {
                await DisplayAlert("تنبيه", "يرجى اختيار حساب من الشجرة أولاً", "موافق");
                return;
            }
            SetEditMode(true);
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            SetEditMode(false);
            // يمكن إضافة كود لإعادة تعيين البيانات الأصلية هنا
        }

        private void OnNewClicked(object sender, EventArgs e)
        {
            TxtCode.Text = string.Empty;
            TxtName.Text = string.Empty;
            PkrParent.SelectedIndex = -1;
            PkrType.SelectedIndex = -1;
            TxtBalance.Text = "0";
            SetEditMode(true);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName?.Text))
            {
                await DisplayAlert("تنبيه", "الرجاء إدخال اسم الحساب.", "موافق");
                return;
            }
            // حفظ الحساب لقاعدة البيانات هنا...
            SetEditMode(false);
            await DisplayAlert("نجاح", "تم حفظ الحساب.", "موافق");
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var ok = await DisplayAlert("تأكيد", "هل تريد حذف هذا الحساب؟", "نعم", "لا");
            if (ok)
            {
                // حذف الحساب من قاعدة البيانات هنا...
                await DisplayAlert("تم", "تم حذف الحساب.", "موافق");
            }
        }

        private void OnPrintClicked(object sender, EventArgs e)
        {
            // وظيفة الطباعة أو التصدير هنا
        }

        // زر العودة للرئيسية
        private async void OnBackToHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }
    }
}
