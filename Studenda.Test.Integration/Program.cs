using Studenda.Library;
using Studenda.Library.Model;

namespace Studenda.Test.Integration;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting...");

        using (ApplicationContext context = new ApplicationContext())
        {
            var dept1 = new Department { Name = "dept1" };
            var dept2 = new Department { Name = "dept2" };
            var dept3 = new Department { Name = "dept3" };

            var crs1 = new Course { DepartmentId = 1, Name = "crs1" };
            var crs2 = new Course { DepartmentId = 1, Name = "crs2" };
            var crs3 = new Course { DepartmentId = 2, Name = "crs3" };
            var crs4 = new Course { DepartmentId = 2, Name = "crs4" };
            var crs5 = new Course { DepartmentId = 3, Name = "crs5" };
            var crs6 = new Course { DepartmentId = 3, Name = "crs6" };

            var grp1 = new Group { CourseId = 1, Name = "grp1" };
            var grp2 = new Group { CourseId = 1, Name = "grp2" };
            var grp3 = new Group { CourseId = 2, Name = "grp3" };
            var grp4 = new Group { CourseId = 3, Name = "grp4" };
            var grp5 = new Group { CourseId = 3, Name = "grp5" };
            var grp6 = new Group { CourseId = 4, Name = "grp6" };
            var grp7 = new Group { CourseId = 4, Name = "grp7" };
            var grp8 = new Group { CourseId = 5, Name = "grp8" };
            var grp9 = new Group { CourseId = 6, Name = "grp9" };

            context.Departments.Add(dept1);
            context.Departments.Add(dept2);
            context.Departments.Add(dept3);
            context.SaveChanges();

            context.Courses.Add(crs1);
            context.Courses.Add(crs2);
            context.Courses.Add(crs3);
            context.Courses.Add(crs4);
            context.Courses.Add(crs5);
            context.Courses.Add(crs6);
            context.SaveChanges();

            context.Groups.Add(grp1);
            context.Groups.Add(grp2);
            context.Groups.Add(grp3);
            context.Groups.Add(grp4);
            context.Groups.Add(grp5);
            context.Groups.Add(grp6);
            context.Groups.Add(grp7);
            context.Groups.Add(grp8);
            context.Groups.Add(grp9);
            context.SaveChanges();

            // Email test.
        }

        Console.WriteLine("Completed!");
        Console.ReadLine();
    }
}
