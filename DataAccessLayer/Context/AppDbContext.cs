using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DLL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("EmployeeContext") 
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeData> EmployeeDatas { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

        }
    }
}
