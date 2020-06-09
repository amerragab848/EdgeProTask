using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using DLL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService
    {
          ApplicationDbContext context=new ApplicationDbContext();

        private IRepository<Employee> repository; //= new Repository<Employee>(new ApplicationDbContext());
        public EmployeeService()
        {
            this.repository = new Repository<Employee>(new ApplicationDbContext());
        }

        //public EmployeeService()
        //{
        //}

        public IEnumerable<Employee> GetAllEmployees()
        {
              var emps= repository.GetAll();
            if (emps !=null)
            {
                return emps;
            }
            return new List<Employee>();
        }
        public  Employee GetEmployeeById(long id)
        {
            var emloyee = repository.Get(id);
            return emloyee;
        }
        public  void InsertEmployee(Employee entity)
        {
            if (entity != null)
            {
                repository.Insert(entity);
            }
        }
        public  void UpdateEmployee(Employee entity)
        {
            if (entity != null)
            {
                repository.Update(entity);
            }
        }
       public void DeleteEmployee(Employee entity)
        {
            if (entity != null)
            {
                repository.Delete(entity);
            }
        }

    }
}
