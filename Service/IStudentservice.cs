using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public interface IStudentservice
    {
        Task<bool> Add(Student Studentmodel);
        Task<IEnumerable<Student>> GetAll();
        Task<bool> Update(Student studentmodel);
        bool Delete(Student studentmodel);
        Task<Student> GetById(int id);
        Task<User> Login(User usermodel);
    }
}
