using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product> // Product için Validation yarattığımız için Parametre "Product"
    {
        // Hangi nesne için kurallar yazacaksak o kuralları Constructor'un içine yazıyoruz
        public ProductValidator()
        {
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1); // Categoryid'si 1 olan ürünlerin ürün fiyatları en az 0 değil 10 tl den büyük olmalı
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün ismi 'A' ile başlamak zorunda"); // Burada olmayan ama benim spesifik yazmak istediğim kurallar için "Must()" methodunu kullanıyoruz,



            // .WithMessage(""); ile hata olduğu zaman ne yazması gerektiğini belirleyebilirsiniz
        }

        private bool StartWithA(string arg)
        {
            if (arg.StartsWith("A") | arg.StartsWith("a"))
            {
                return true;
            }
            // Ya da direkt return arg.StartsWith("A");
            return false;
        }
    }
}
