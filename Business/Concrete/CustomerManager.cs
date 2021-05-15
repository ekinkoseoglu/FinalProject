using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
   public class CustomerManager:ICustomerService
   {
       private ICustomerDal _customerDal;

       public CustomerManager(ICustomerDal customerDal)
       {
           _customerDal = customerDal;
       }

       public List<Customer> GetAll()
       {
           return _customerDal.GetAll();
       }

       public List<Customer> GetAllByCity(string city)
       {
           return _customerDal.GetAll(c => c.City.Contains(city));
       }



       public Customer GetByCustomerId(string id)
       {
           return _customerDal.Get(c=>c.CustomerID.Contains(id));
       }

        public void Add(Customer customer)
        {
            _customerDal.Add(customer);
        }

        public void Update(Customer customer)
        {
            _customerDal.Update(customer);
        }

       

        public void Delete(Customer customer)
        {
            var deletedCustomer = _customerDal.Get(c => c.CustomerID == customer.CustomerID);
            _customerDal.Delete(deletedCustomer);
        }
    }
}
