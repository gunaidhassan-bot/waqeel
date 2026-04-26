using System;
using Microsoft.Maui.Controls;
using waqeel.Models;
using waqeel.Services;

namespace waqeel
{
    public partial class MainPage : ContentPage
    {
        // استدعاء محرك البيانات (Data Service)
        private readonly DataService _dataService = new DataService();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // تأكد أن العناصر موجودة (حماية ضد NullReference إذا لم تُحمَّل من XAML)
                var farmerText = FarmerEntry?.Text ?? string.Empty;
                var qtyText = QtyEntry?.Text ?? string.Empty;

                // التحقق من إدخال البيانات الأساسية
                if (string.IsNullOrWhiteSpace(farmerText) || string.IsNullOrWhiteSpace(qtyText))
                {
                    await DisplayAlertAsync("تنبيه", "يرجى إدخال اسم المزارع والكمية على الأقل", "موافق");
                    return;
                }

                // تحويل الكمية بأمان
                int quantity = 0;
                if (!int.TryParse(qtyText, out quantity))
                {
                    await DisplayAlertAsync("تنبيه", "الكمية غير صالحة. الرجاء إدخال رقم صحيح.", "موافق");
                    return;
                }

                // إنشاء كائن السجل
                var newRecord = new PackingRecord
                {
                    Id = Guid.NewGuid(),
                    EntryDate = DateTime.Now,
                    FarmerName = farmerText,
                    ItemType = ItemEntry?.Text,
                    Quantity = quantity,
                    DriverName = DriverEntry?.Text,
                    Status = "Pending",
                    Notes = "تم الإدخال عبر الجوال"
                };

                // حفظ البيانات في الخدمة
                await _dataService.SavePackingRecord(newRecord);

                // تحديث الواجهة عند النجاح
                StatusLabel.Text = $"تم حفظ بضاعة {farmerText} بنجاح ✅";

                // تفريغ الحقول لإدخال جديد
                FarmerEntry.Text = ItemEntry.Text = QtyEntry.Text = DriverEntry.Text = string.Empty;
            }
            catch (Exception ex)
            {
                // عرض تفاصيل الخطأ في حال فشل الاتصال بقاعدة البيانات
                var msg = ex.GetBaseException()?.Message ?? ex.Message;
                await DisplayAlertAsync("خطأ في النظام", "تأكد من تشغيل PostgreSQL: " + msg, "موافق");
            }
        }
    }
}
