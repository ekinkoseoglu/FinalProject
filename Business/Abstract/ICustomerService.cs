using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface ICustomerService
   {
       List<Customer> GetAll();
       List<Customer> GetAllByCity(string city);
       Customer GetByCustomerId(string id);
       void Add(Customer customer);
       void Update(Customer customer);
       void Delete(Customer customer);
    }
}
