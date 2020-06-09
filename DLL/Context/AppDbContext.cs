using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeData> EmployeeDatas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                      .HasOne(x => x.Manager)
                      .WithMany(x => x.EmpManager)
                      .HasForeignKey(x => x.ManagerId);

        }
    }
}
