using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Linq;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) // Eğer gönderilen Validator tipi bir IValidator değilse yani öyle bir Validator yoksa
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");// Ona kız
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // (Reflection)  ProductValidator'un instancesini oluştur
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // ProductValidator'i inherite eden Class'ı bul o class'ında Generic tipinden ilkini bul (Product) 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // Bu sefer de Manager methodlarımızın parametrelerine bak, ProductValidator'i inherite eden Class ın generic tipine eşit olan parametreleri bul (Birden fazla parametre de olabilir çünkü manager methodlarında, biz sadece O parametreyi tutup çıkarmak istiyoruz.)
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); // ValidationTool'u kullanarak Validate et.
            }
        }

    }
}
