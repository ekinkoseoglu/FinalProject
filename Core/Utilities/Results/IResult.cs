using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    // Temel voidler için başlangıç
    //İçerisinde bir tane işlem sonucu olsun bir tane de kullanıcıyı bilgilendirmek adına bir mesaj olsun 
    //Yaptığımız şey bizim API lerimizi ya da uygulamalarımızı kullanacak kişileri doğru yönlendirmek
   public interface IResult
    {
       public bool Success { get; } // Yapmaya çalıştığım ekleme işi başarılı mı başarısız mı olduğunu okuyan property
       public string Message { get; }// Başarılıysa başarılı olduğunu, başarısızsa başarısız olduğunu söyleyen proeprty
    }
}
