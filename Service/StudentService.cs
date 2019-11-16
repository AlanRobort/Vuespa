using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;
using Persistence;

namespace Service
{
    public class StudentService : IStudentservice
    {
        private readonly StudentDbContext _student;
        

        public StudentService(
            StudentDbContext student
            )
        {
            _student = student;
           
        }
        public async Task<bool> Add(Student Studentmodel)
        {
            if (Studentmodel != null) {
                
                    
                await _student.students.AddAsync(Studentmodel);
                var result = await _student.SaveChangesAsync();
                //throw new Exception(result.ToString());
                     return true;
            }
            return false;
        }

        public bool Delete(Student studentmodel)
        {
            if (studentmodel != null)
            {
                _student.students.Remove(studentmodel);
                _student.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var result = new List<Student>();
            try
            {
               result = await _student.students.ToListAsync();
                
            }
            catch (Exception ex)
            {
               
                    throw ex;
               

            }
            return result;
        }

        public async Task<Student> GetById(int id)
        {
            return  await _student.students.FindAsync(id);
        }

        public async Task<User> Login(User usermodel)
        {
           
            var result = await _student.users.FirstOrDefaultAsync(x => x.Id == usermodel.Id);
            return result;
        }

        public async Task<bool> Update(Student studentmodel)
        {
            try
            {
                var Original = await _student.students.SingleAsync(x => x.id == studentmodel.id);
                if (Original != null)
                {
                    Original.id = studentmodel.id;
                    Original.Name = studentmodel.Name;
                    Original.Desc = studentmodel.Desc;
                    await _student.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                //if (_env.IsDevelopment()) {
                //    throw ex;
                //}
                return false;
            }

        }
    }
}
