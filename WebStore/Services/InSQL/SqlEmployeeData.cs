using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Services.Abstract;

namespace WebStore.Services.InSQL
{
    public class SqlEmployeeData : IEmployeesData
    {
        public int Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
