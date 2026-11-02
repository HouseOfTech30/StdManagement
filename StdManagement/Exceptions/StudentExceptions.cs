using System;
using System.Collections.Generic;
using System.Text;

namespace StdManagement.Exceptions
{
    public class InvalidStudentException:Exception
    {
        public InvalidStudentException(string message) : base(message)
        {

        }
    }

    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException(string message) : base(message)
        {

        }
    }
    public class StudentAlreadyExistsException : Exception
    {
        public StudentAlreadyExistsException(string message) : base(message)
        {

        }
    }
    public class AgeIndexOutOfRangeException : Exception
    {
        public AgeIndexOutOfRangeException(string message) : base(message)
        {
        }
    }  
    
    public class GradeIndexOutOfRangeException : Exception
    {
        public GradeIndexOutOfRangeException(string message) : base(message)
        {
        }
    }
}
