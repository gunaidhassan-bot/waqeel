using System;
using System.Collections.Generic;

namespace waqeel.Models
{
    // 1. دليل الحسابات (نظام ثلاثي)
    public class Account
    {
        public long AccountCode { get; set; }
        public string AccountName { get; set; }
        public long? ParentCode { get; set; }
        public int Level { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }

    // 2. جدول التعبئة (الميداني - خارج الموازنة)
    public class PackingRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public string FarmerName { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public string DriverName { get; set; }
        public decimal EstimatedPrice { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; } = "Pending";
    }

    // 3. جدول فواتير الشراء (التحديث الجوهري للخصومات الثلاثة)
    public class PurchaseInvoice
    {
        public int InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public long FarmerAccountCode { get; set; }
        public decimal TotalGrossAmount { get; set; } // المبلغ الإجمالي قبل الخصم

        // الخصم 1: عمولة المكتب (تُستبعد من متوسط التكلفة)
        public bool ApplyAgencyCommission { get; set; } // خيار اعتماد أو لا
        public decimal AgencyCommissionPercent { get; set; } // نسبة قابلة للتعديل
        public decimal AgencyCommissionAmount { get; set; } // مبلغ ناتج عن النسبة

        // الخصم 2: زكاة وزارة الزراعة (لا تُستبعد من متوسط التكلفة)
        public bool ApplyZakat { get; set; } // خيار اعتماد أو لا
        public decimal ZakatPercent { get; set; } // نسبة الوزارة
        public decimal ZakatAmount { get; set; }

        // الخصم 3: خصم رقمي إضافي (متاح دائماً - لا يُستبعد من متوسط التكلفة)
        public decimal OtherDiscountAmount { get; set; } // مبلغ مباشر (رقمي وليس نسبي)
        public long? OtherDiscountAccountId { get; set; } // الحساب الذي يختاره المحاسب

        public decimal NetAmount { get; set; } // الصافي النهائي للمزارع بعد كل الخصومات
        public string Notes { get; set; }
        public Guid? PackingRecordId { get; set; }
    }

    // 4. جدول سندات الصرف والقبض (في جدول واحد مع حقل نوع)
    public class Voucher
    {
        public int VoucherNo { get; set; }
        public string Type { get; set; } // صرف / قبض
        public DateTime Date { get; set; }
        public long AccountCode { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
    }

    // 5. القيود اليومية (المحرك المالي الرسمي)
    public class JournalHeader
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int JournalNo { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public List<JournalDetail> Details { get; set; } = new();
    }

    public class JournalDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long AccountCode { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Notes { get; set; }
    }

    // 6. الجداول المساعدة (أصناف وسائقين)
    public class Item
    {
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
    }

    public class Driver
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
    }
}
