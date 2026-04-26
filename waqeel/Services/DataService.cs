using waqeel.Models;
using Microsoft.EntityFrameworkCore;

namespace waqeel.Services
{
    public class DataService
    {
        private readonly AppDbContext _context;

        public DataService()
        {
            _context = new AppDbContext();
            // هذه السطر يضمن إنشاء قاعدة البيانات إذا لم تكن موجودة
            _context.Database.EnsureCreated();
        }

        // وظيفة حفظ سجل تعبئة جديد من الجوال
        public async Task SavePackingRecord(PackingRecord record)
        {
            _context.PackingRecords.Add(record);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                // رمي استثناء جديد يحتوي على رسالة الخطأ الداخلية ليسهل التشخيص عند العرض في الواجهة
                var inner = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new Exception($"Database update failed: {inner}", dbEx);
            }
            catch (Exception)
            {
                throw; // أعد رمي الاستثناء ليتم التعامل معه في طبقة العرض
            }
        }

        // وظيفة جلب كل السجلات لعرضها لمسؤول الشراء
        public async Task<List<PackingRecord>> GetPackingRecords()
        {
            return await _context.PackingRecords.OrderByDescending(r => r.EntryDate).ToListAsync();
        }
    }
}
