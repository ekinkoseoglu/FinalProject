using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>,IResult
    {
        public SuccessDataResult(T data, string message) : base(data,true, message)
        {
            // İster Data ver Mesaj ver
        }

        public SuccessDataResult(T data) : base(data, true)
        {
            //İster Sadece data ver
        }

        public SuccessDataResult(string message):base(default,true,message) // Datayı default haliyle döndürmek ve mesaj cıkarmak isteyebilir bu çok az kullanacagımız bir version
        {
            // İster Sadece Mesaj ver
        }

        public SuccessDataResult():base(default,true) // Üsttekinin Mesaj vermeyen hali
        {
            // İstersen hiçbir şey verme
        }
    }
}
