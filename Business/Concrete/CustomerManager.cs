using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IDataResult<List<Customer>> GetAll()
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<List<Customer>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Customer>> GetAllByCity(string city)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<List<Customer>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(c => c.City.Contains(city)), Messages.ProductListed);
        }



        public IDataResult<Customer> GetByCustomerId(string id)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<Customer>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CustomerID.Contains(id)),Messages.HasShown);
        }

        public IResult Add(Customer customer)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Update(Customer customer)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour<9)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _customerDal.Update(customer);
            return new SuccessResult(Messages.Updated);
        }



        public IResult Delete(Customer customer)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            var deletedCustomer = _customerDal.Get(c => c.CustomerID == customer.CustomerID);
            _customerDal.Delete(deletedCustomer);
            return new SuccessResult(Messages.Updated);
        }
    }
}
