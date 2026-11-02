using StdManagement.Contexts;
using StdManagement.Exceptions;
using StdManagement.Interfaces;
using StdManagement.Models;
using StdManagement.Repositories;

public class Program
{
    static AppDBContext context = new AppDBContext();
    IStudentRepository repository = new StudentRepository(context);
    public static void Main(string[] args)
    {
        Program program = new Program();
        while (true)
        {
            Console.WriteLine("Please Choose Number:");
            Console.WriteLine("1. Add Sutdent");
            Console.WriteLine("2. Get All Student");
            Console.WriteLine("3. Get Student By Id");
            Console.WriteLine("4. Update Student");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Search Student By Name");
            Console.WriteLine("7. Search Student By Age");
            int choose = 0 ;
            bool check = int.TryParse(Console.ReadLine(), out choose);
            if (!check)
            {
                continue;
            }
            switch (choose)
            {
                case 1:
                    program.AddStudent();
                    break;
                case 2:
                    program.GetAllStudent();
                    break;
                case 3:
                    Console.WriteLine("Enter Student Id");
                    int id = int.Parse(Console.ReadLine());
                    program.GetStudentById(id);
                    break;
                case 4:
                    Console.WriteLine("Enter Student Id");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Student Name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Student Age");
                    int age = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Student Grade");
                    int grade = int.Parse(Console.ReadLine());
                    Student std = new Student
                    {
                        Id = id,
                        Name = name,
                        Age = age,
                        Grade= grade
                    };
                    program.UpdateStudent(std);
                    break;

                case 5:
                    Console.WriteLine("Enter Student Id");
                    id = int.Parse(Console.ReadLine());
                    program.DeleteStudent(id);
                    break;
                case 6:
                    Console.WriteLine("Enter Student Name");
                    name = Console.ReadLine();
                    program.StudentSearchByName(name);
                    break;
                case 7:
                    Console.WriteLine("Enter Student Age");
                    string age1 = Console.ReadLine();
                    program.StudentSearchByAge(age1);
                    break;

                default:
                    break;
            }
        }
    }

    public void AddStudent()
    {
        try
        {
            Console.WriteLine("Enter student name:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter student age:");
            var age = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter student grade:");
            var grade = int.Parse(Console.ReadLine());

            var student = new Student
            {
                Name = name,
                Age = age,
                Grade = grade
            };

            repository.AddStudent(student);
            Console.WriteLine("Save Successful!");
        }
        catch (InvalidStudentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (StudentAlreadyExistsException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (AgeIndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (GradeIndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return;

        }
    }
    public void GetAllStudent()
    {
        try
        {

            List<Student> _stds = repository.GetAllStudents();
            if(_stds.Count() == 0)
            {
                Console.WriteLine("Student List is Empty");
            }
            foreach(var item in _stds)
            {
                Console.WriteLine($"Id : {item.Id}, Name : {item.Name}, Age : {item.Age}, Grade : {GradeLetter(item.Grade)}");
            }

        }catch(Exception ex)
        {
            Console.WriteLine($"Error {ex.Message}");
        }
    }
    public void GetStudentById(int id)
    {
        try
        {


            var item = repository.GetStudentById(id);
            Console.WriteLine($"Id : {item.Id}, Name : {item.Name}, Age : {item.Age}, Grade : {GradeLetter(item.Grade)}");

        }
        catch(StudentNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error : {ex.Message}");
        }
    }
    public void UpdateStudent(Student std)
    {
        try
        {
            repository.UpdateStudent(std);
            Console.WriteLine("Update Successful");
        }
        catch (InvalidStudentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (StudentAlreadyExistsException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (AgeIndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (GradeIndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error : {ex.Message}");
        }
    }
    public void DeleteStudent(int id)
    {
        try
        {
            repository.DeleteStudent(id);
            Console.WriteLine("Delete Successful");
        }
        catch (StudentNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error : {ex.Message}");
        }
    }
    public string GradeLetter(int grade)
    {
        switch (grade)
        {
            case > 90:
                return "A";
            case > 80:
                return "B";
            case > 60:
                return "C";
            default:
                return "D";
        }
    }

    public void StudentSearchByName(string name)
    {
        try
        {
            List<Student> students = repository.GetStudentsByName(name);
            foreach (var item in students)
            {
                Console.WriteLine($"Id : {item.Id}, Name : {item.Name}, Age : {item.Age}, Grade : {GradeLetter(item.Grade)}");
            }
        }
        catch (StudentNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error : {ex.Message}");
        }
    }

    public void StudentSearchByAge(string age)
    {
        try
        {
            List<Student> students = repository.GetStudentsByAge(age);
            foreach (var item in students)
            {
                Console.WriteLine($"Id : {item.Id}, Name : {item.Name}, Age : {item.Age}, Grade : {GradeLetter(item.Grade)}");
            }
        }
        catch (StudentNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error : {ex.Message}");
        }
    }
}


