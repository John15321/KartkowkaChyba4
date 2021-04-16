using System;
using System.Linq;
using System.Data.Entity;

namespace Kartkowka
{
    public class Student
    {
        public int Id { set; get; }
        public string name { set; get; }
        public string surname { set; get; }
        public string speciality { set; get; }

        public int deficit { set; get; }

        public void ShowStudent()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Fulname: {name} {surname}");
            Console.WriteLine($"Speciality: {speciality}");
        }
    }


    public class Dziewkanat : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }

        public Student AddDataToBase()
        {
            var student = new Student();
            Console.WriteLine("Name: ");
            student.name = Console.ReadLine();
            Console.WriteLine("Surname: ");
            student.surname = Console.ReadLine();
            Console.WriteLine("Speciality: ");
            student.speciality = Console.ReadLine();
            return student;
        }

        public void ShowDataBaseContent()
        {
            var students = (from a in this.Students select a).ToList<Student>();
            foreach (var stud in students)
            {
                stud.ShowStudent();
                Console.WriteLine("");
            }
        }

        public void ClearBase()
        {
            Students.RemoveRange(Students);
            this.SaveChanges();
        }

        public void RemoveLast()
        {
            var last = Students.OrderByDescending(g => g.Id).Take(1);
            Students.RemoveRange(last);
            this.SaveChanges();
        }

        public void FindByName(string name)
        {
            var student = this.Students.Where(stud => stud.name == name).ToList<Student>();
            foreach (var studenciak in student)
            {
                studenciak.ShowStudent();
                Console.WriteLine("");
            }
        }
        public void FindById(int id)
        {
            var student = this.Students.Where(studenciak => studenciak.Id == id).ToList<Student>();
            foreach (var studenciak in student)
            {
                studenciak.ShowStudent();
                Console.WriteLine("");
            }
        }

        public void FindBySpeciality(string speciality)
        {
            var students = this.Students.Where(studenciak => studenciak.speciality == speciality).ToList<Student>();
            foreach (var studenciak in students)
            {
                studenciak.ShowStudent();
                Console.WriteLine("");
            }
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            var dziekanatDB = new Dziewkanat();
            Console.Clear();
            while (true)
            {
                Console.WriteLine("'1' Add new student");
                Console.WriteLine("'2' Clear whole database");
                Console.WriteLine("'3' Show whole content");
                Console.WriteLine("'4' Remove last added student");
                Console.WriteLine("'5' Find student by Id");
                Console.WriteLine("'6' Find student by name");
                Console.WriteLine("'7' Find student by speciality");
                Console.WriteLine("'9' Exit");
                Console.WriteLine("Chose one option");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Insert new student");
                        dziekanatDB.Students.Add(dziekanatDB.AddDataToBase());
                        dziekanatDB.SaveChanges();
                        Console.WriteLine("Press ENTER to return");
                        Console.ReadLine();
                        break;

                    case "2":
                        dziekanatDB.ClearBase();
                        Console.WriteLine("Base Cleare");
                        Console.WriteLine("Press ENTER to return");
                        Console.ReadLine();
                        break;
                    case "3":
                        dziekanatDB.ShowDataBaseContent();
                        Console.WriteLine("Press ENTER to return");
                        Console.ReadLine();
                        break;

                    case "4":
                        dziekanatDB.RemoveLast();
                        Console.WriteLine("Element removed");
                        Console.WriteLine("Press ENTER to return");
                        Console.ReadLine();
                        break;

                    case "5":
                        Console.WriteLine("Find by Id: ");
                        dziekanatDB.FindById(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("Press ENTER to return");
                        Console.ReadLine();
                        break;

                    case "6":
                        Console.WriteLine("Find students with given name: ");
                        dziekanatDB.FindByName(Console.ReadLine());
                        Console.WriteLine("Press ENTER to return");
                        Console.ReadLine();
                        break;

                    case "7":
                        Console.WriteLine("Find students with givn speciality: ");
                        dziekanatDB.FindBySpeciality(Console.ReadLine());
                        Console.WriteLine("Press ENTER to return");
                        Console.ReadLine();
                        break;


                    case "8":
                        break;

                    default:
                        Console.WriteLine("Error: option not found!!!");
                        Console.WriteLine("\nPress ENTER to return\n");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
