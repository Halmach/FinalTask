using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dr;
            string sourcePath = "";
            string testPath = @"C:\Users\pc\Desktop\Students\Students.dat";
            string desktopStudents = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Students";
            Console.WriteLine(desktopStudents);
            if (!Directory.Exists(desktopStudents)) dr = Directory.CreateDirectory(desktopStudents);
            else dr = new DirectoryInfo(desktopStudents);
            Student[] sts = new Student[1];

            Console.WriteLine("Введите путь к бинарному файлу расширения .dat");
            try
            {   
                sourcePath = Console.ReadLine();
                if (sourcePath.Trim() == "") throw new Exception("Не указан путь");
                else if (!File.Exists(sourcePath)) throw new Exception("Файл не существует или неверно указан путь");
                else
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (var fs = new FileStream(sourcePath, FileMode.Open))
                    {
                        sts = (Student[])formatter.Deserialize(fs);
                    }
                    List<Student> students = new List<Student>(sts);

                    var groups = from student in students
                        group student by student.Group;

                    foreach (IGrouping<string, Student> g in groups)
                    {
                        using (BinaryWriter writer = new BinaryWriter(File.Create(desktopStudents + @"\" + g.Key + ".txt")))
                        {
                            foreach (var st in g)
                                writer.Write(st.Name + "," + st.DateOfBirth.ToString("d"));
                        }
                    }
                    Console.WriteLine("Файлы готовы");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

          

           

            


        }
    }
}
