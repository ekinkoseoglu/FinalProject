using FluentValidation;
using System;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool// Bu tip tool'lar genellikle static olarak yapılır, tek bir instance olusturur ve uygulama belleği sadece onu kullanır
    {
        public static void Validate(IValidator validator, object entity) // Kendimize merkezi bir "Validate" methodu yarattık böylece her bir Manager için "IValidator" in Validate kodunu kullanmamıza gerek kalmadı
        {
            var context = new ValidationContext<Object>(entity); // ProductValidator'ü Base Class'ı olan AbstractValidator<> class'ının içine "Validate()" methodunu bulana kadar F12 ile girdik sonunda "IValidator"'ün o methodu yarattığını gördük. Parametre olarak da onu ekledik ki burada kullanabilelim.
            var result = validator.Validate(context);
            if (!result.IsValid) // eğer sonuç geçersizse 
            {
                throw new ValidationException(result.Errors); // Hata fırlat
            }
        }
    }
   
}
