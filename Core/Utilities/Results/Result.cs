using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
   public class Result:IResult
    {
       

        public Result(bool success, string message):this(success) // 'This' yani bu Result class'ının (success) isimli tek parametresi olan Constructor'una 'succes' i yolla
        {
            Message = message;
            
        }
        public Result(bool success) // Her methoddan sonra mesaj vermek istemiyorsamsa sadece işlemi yapacak mesajı göndermeyecek constructor'u overload edebilirim
        {
            
            Success = success;
        }

       
        // YANİ BEN ŞUAN SADECE SUCCESS CONSTRUCTOR'UNUDA ÇALIŞTIRABİLİRİM, MESSAGE VE SUCCESS OLAN CONSTRUCTORUDA ÇALIŞTIRABİLİRİM

        public bool Success { get; }
        public string Message { get; }
    }
}
