using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T>:DataResult<T>,IResult
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
            // İster Data ver Mesaj ver
        }

        public ErrorDataResult(T data) : base(data, false)
        {
            //İster Sadece data ver
        }

        public ErrorDataResult(string message) : base(default, false, message) // Datayı default haliyle döndürmek ve mesaj cıkarmak isteyebilir bu çok az kullanacagımız bir version
        {
            // İster Sadece Mesaj ver
        }

        public ErrorDataResult() : base(default, false) // Üsttekinin Mesaj vermeyen hali
        {
            // İstersen hiçbir şey verme
        }
    }
}
