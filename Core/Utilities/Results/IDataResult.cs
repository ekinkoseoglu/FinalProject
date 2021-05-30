using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
   public interface IDataResult<T>:IResult // sen aynı zamanda bir IResult sın çünkü IResult'ın bulundurduğu Success ve Message methodlarını da kullanacağız sende, ek olarak yukarıda belirttiğim T türünde bir data da olacak
    {
        public T Data { get;}
    }
}
