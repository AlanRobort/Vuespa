
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
  public  class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options):base(options)
        {

        }
        public DbSet<Student> students { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<ClientAddressInfo> clientAddressInfos { get; set; }
    }
}
