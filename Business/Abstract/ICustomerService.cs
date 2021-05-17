using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface ICustomerService
   {
       IDataResult<List<Customer>> GetAll();
       IDataResult<List<Customer>> GetAllByCity(string city);
       IDataResult<Customer> GetByCustomerId(string id);
       IResult Add(Customer customer); 
       IResult Update(Customer customer);
       IResult Delete(Customer customer);
    }
}
