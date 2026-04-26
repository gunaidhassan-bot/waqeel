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

        // دالة للتحكم في حالة الحقول والأزرار
        private void SetEditMode(bool isEditing)
        {
            // الحقول
            TxtName.IsReadOnly = !isEditing;
            PkrParent.IsEnabled = isEditing;
            PkrType.IsEnabled = isEditing;

            // تغيير مظهر الحقول (اختياري لزيادة التوضيح البصري)
            TxtName.BackgroundColor = isEditing ? Colors.White : Color.FromArgb("#F5F5F5");
            PkrParent.BackgroundColor = isEditing ? Colors.White : Color.FromArgb("#F5F5F5");
            PkrType.BackgroundColor = isEditing ? Colors.White : Color.FromArgb("#F5F5F5");

            // الأزرار
            BtnSave.IsEnabled = isEditing;
            BtnCancel.IsEnabled = isEditing;

            BtnNew.IsEnabled = !isEditing;
            BtnEdit.IsEnabled = !isEditing;
            BtnDelete.IsEnabled = !isEditing;
        }

        // عند الضغط على زر تعديل
        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCode?.Text))
            {
                await DisplayAlertAsync("تنبيه", "يرجى اختيار حساب من الشجرة أولاً", "موافق");
                return;
            }
            SetEditMode(true);
        }

        // عند الضغط على تراجع
        private void OnCancelClicked(object sender, EventArgs e)
        {
            SetEditMode(false);
            // هنا يمكن إضافة كود لإعادة تحميل البيانات الأصلية من قاعدة البيانات لإلغاء أي تغييرات مؤقتة
        }

        // عناصر أزرار مساعدة قد تُستخدم من XAML
        private void OnNewClicked(object sender, EventArgs e)
        {
            // تهيئة الحقول لإنشاء حساب جديد
            TxtCode.Text = string.Empty;
            TxtName.Text = string.Empty;
            PkrParent.SelectedIndex = -1;
            PkrType.SelectedIndex = -1;
            TxtBalance.Text = "0";
            SetEditMode(true);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // هنا يتم حفظ التغييرات إلى قاعدة البيانات
            // تنفيذ تحقق سريع
            if (string.IsNullOrWhiteSpace(TxtName?.Text))
            {
                await DisplayAlertAsync("تنبيه", "الرجاء إدخال اسم الحساب.", "موافق");
                return;
            }

            // TODO: استدعاء خدمة حفظ الحساب
            SetEditMode(false);
            await DisplayAlertAsync("نجح", "تم حفظ الحساب.", "موافق");
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var ok = await DisplayAlertAsync("تأكيد", "هل تريد حذف هذا الحساب؟", "نعم", "لا");
            if (ok)
            {
                // TODO: تنفيذ حذف
                await DisplayAlertAsync("تم", "تم حذف الحساب.", "موافق");
            }
        }

        private void OnPrintClicked(object sender, EventArgs e)
        {
            // TODO: طباعة أو تصدير
        }
    }
}
