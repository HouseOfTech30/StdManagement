using StdManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StdManagement.Interfaces
{
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
        Student GetStudentById(int id);
        List<Student> GetAllStudents();
        List<Student> GetStudentsByName(string name);
        List<Student> GetStudentsByAge(string age);
        List<Student> GetStudentsByAge(int age);
    }
}
