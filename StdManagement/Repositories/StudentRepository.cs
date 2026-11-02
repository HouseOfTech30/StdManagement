using StdManagement.Contexts;
using StdManagement.Exceptions;
using StdManagement.Interfaces;
using StdManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StdManagement.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDBContext _context;
        private readonly List<Student> _students = new List<Student>();
        private int _nextId = 1;

        public StudentRepository(AppDBContext context)
        {
            _context = context;
        }
        public void AddStudent(Student student)
        {
           if(student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }

           if(string.IsNullOrWhiteSpace(student.Name))
            {
                throw new InvalidStudentException("Student name cannot be null or empty.");
            }

           if(student.Age < 5)
            {
                throw new AgeIndexOutOfRangeException("Student age cannot be less than 5.");
            }

           if(student.Grade <= 0 || student.Grade > 100)
            {
                throw new GradeIndexOutOfRangeException("Student grade must be between 0 and 100.");
            }

           var existingStudent = _students.FirstOrDefault(s => s.Name.Equals(student.Name, StringComparison.OrdinalIgnoreCase));

            if(existingStudent != null)
            {
                throw new StudentAlreadyExistsException("A student with the same name already exists.");
            }

            //student.Id = _nextId++;
            //_students.Add(student);
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void UpdateStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
            {
                throw new InvalidStudentException("Student name cannot be null or empty.");
            }

            if (student.Age < 5)
            {
                throw new AgeIndexOutOfRangeException("Student age cannot be less than 5.");
            }

            if (student.Grade <= 0 || student.Grade > 100)
            {
                throw new GradeIndexOutOfRangeException("Student grade must be between 0 and 100.");
            }
            //var existingStudentByName = _students.FirstOrDefault(s => s.Name.Equals(student.Name, StringComparison.OrdinalIgnoreCase) && s.Id != student.Id);
            var existingStudentByName = _context.Students.FirstOrDefault(s => s.Name.ToUpper().Equals(student.Name.ToUpper()) && s.Id != student.Id);
            if (existingStudentByName != null)
            {
                throw new StudentAlreadyExistsException("A student with the same name already exists.");
            }

            var existingStudent = GetStudentById(student.Id);

            if(existingStudent == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Grade = student.Grade;
            _context.Students.Update(existingStudent);
            _context.SaveChanges();
            //_students.Remove(existingStudent);
            //_students.Add(student);
        }
        public void DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            //_students.Remove(student);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
        public Student GetStudentById(int id)
        {
            //var existingStd = _students.Find(s => s.Id == id);
            var existingStd = _context.Students.Find(id);
            if (existingStd == null)
            {
                throw new StudentNotFoundException("Student is empty");
            }

            return existingStd;
        }
        public List<Student> GetAllStudents()
        {
            //return _students;
            return _context.Students.ToList();
        }

        public List<Student> GetStudentsByName(string name)
        {
            //var existingStd = _students.Where(std => std.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            var existingStd = _context.Students.Where(std => std.Name.ToLower().Contains(name.ToLower())).ToList();
            if (existingStd.Count == 0)
            {
                throw new StudentNotFoundException("Student Name Is Not Found");
            }

            return existingStd;
        }

        public List<Student> GetStudentsByAge(string age)
        {
            //var existingStd = _students.Where(std => std.Age.ToString().Contains(age)).ToList();
            var existingStd = _context.Students.Where(std => std.Age.ToString().Contains(age)).ToList();
            if (existingStd.Count == 0)
            {
                throw new StudentNotFoundException("Student Name Is Not Found");
            }

            return existingStd;
        }

        public List<Student> GetStudentsByAge(int age)
        {
            //var existingStd = _students.Where(std => std.Age == age).ToList();
            var existingStd = _context.Students.Where(std => std.Age == age).ToList();
            if (existingStd.Count == 0)
            {
                throw new StudentNotFoundException("Student Name Is Not Found");
            }

            return existingStd;
        }
    }
}
