using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool// Bu tip toollar genellikle static olarak yapılır, tek bir instance olusturur ve uygulama belleği sadece onu kullanır
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<Object>(entity); // ProductValidator'ün içine Validate methodunu bulana kadar F12 ile girdik sonunda IValidator'ün o methodu yarattığını gördük
            var result = validator.Validate(context);
            if (!result.IsValid) // eğer sonuç geçersizse 
            {
                throw new ValidationException(result.Errors); // Hata fırlat
            }
        }
    }
}
