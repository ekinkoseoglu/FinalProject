using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
   public class Result:IResult
    {
        private bool v1;
        private string v2;

        public Result(bool v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
