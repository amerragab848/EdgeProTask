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
    public class EmployeeDataService
    {
        private readonly IRepository<EmployeeData> _repository;

        public EmployeeDataService()
        {
           // _repository = repository;
            _repository = new Repository<EmployeeData>(new ApplicationDbContext());
        }
        //List<Employee> GetUsersDataByManagerId(int id)
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //       //var res=  context.Employees.Join(context.EmployeeDatas, c => c.Id, b => b.EmployeeId,
        //       // (c, b) => c).Where(a => a.ManagerId == id).ToList();
        //    return res;
        //}


        public IEnumerable<EmployeeData> GetAllEmployeesData()
        {
            return _repository.GetAll();
        }
        public EmployeeData GetEmployeeDataById(long id)
        {
            var emloyee = _repository.Get(id);
            return emloyee;
        }
        public void InsertEmployeeData(EmployeeData entity)
        {
            if (entity != null)
            {
                _repository.Insert(entity);
            }
        }
        public void UpdateEmployeeData(EmployeeData entity)
        {
            if (entity != null)
            {
                _repository.Update(entity);
            }
        }
        public void DeleteEmployeeData(EmployeeData entity)
        {
            if (entity != null)
            {
                _repository.Delete(entity);
            }
        }
    }
}
