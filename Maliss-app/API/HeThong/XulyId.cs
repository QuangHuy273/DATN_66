using Microsoft.EntityFrameworkCore;

using System.Reflection; 

namespace API.HeThong
{
    public class XulyId
    {
        public async Task<string> GenerateIdAsync<TEntity>(string prefix, DbSet<TEntity> dbSet, string columnName)
            where TEntity : class
        {
            
            var propertyInfo = typeof(TEntity).GetProperty(columnName, BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
            {
                throw new InvalidOperationException($"Lỗi cấu hình: Thuộc tính '{columnName}' không tồn tại hoặc không phải kiểu string trong model '{typeof(TEntity).Name}'.");
            }

            var lastItem = await dbSet
                .AsNoTracking() 
                .Where(e => EF.Property<string>(e, columnName).StartsWith(prefix))
                .OrderByDescending(e => EF.Property<string>(e, columnName))
                .FirstOrDefaultAsync();

            int newNumber = 1;

            if (lastItem != null)
            {
                
                var lastId = propertyInfo.GetValue(lastItem) as string;

                if (lastId != null && lastId.Length > prefix.Length)
                {
                    var numberPart = lastId.Substring(prefix.Length);

                    if (int.TryParse(numberPart, out int lastNumber))
                    {
                        newNumber = lastNumber + 1;
                    }
                }
            }
            return $"{prefix}{newNumber:D3}";
        }
    }
}