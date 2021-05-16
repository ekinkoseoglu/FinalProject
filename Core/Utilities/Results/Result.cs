using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
   public class Result:IResult
    {
       

        public Result(bool success, string message):this(success) // Result class'ının constructoruna "success"'i yolla dersen aslında alttaki Success constructoruda çalısır bu constructorda çalışır
        {
            Message = message;
            Success = success;
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
