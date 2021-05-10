﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICustomerDal:IEntityRepository<Customer>// Sen bir IEntityRepository'sin ve çalışma tipin "Customer"'dır dedik
    {
    }
}
