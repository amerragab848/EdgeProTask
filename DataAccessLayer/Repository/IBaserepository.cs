using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IBaserepository<T> :IRepository<T>where T :BaseEntity
    {
        List<T> GetUsersDataByManagerId(int id);
    }
}
