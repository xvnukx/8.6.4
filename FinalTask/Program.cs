using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirdth { get; set; }

        public Student(string name, string group, DateTime dateOfBirdth)
        {
            Name = name;
            Group = group;
            DateOfBirdth = dateOfBirdth;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dirInfo = new DirectoryInfo("C:\\Users\\кек\\Desktop");
            if (!dirInfo.Exists)
                dirInfo.Create();

            dirInfo.CreateSubdirectory("Students");
            Deserialize();
        }

        static void Deserialize()
        {
            string path = "C:\\Users\\кек\\Desktop\\Students.dat";

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

            Student[] student = (Student[])formatter.Deserialize(fs);

            List<string> groups = new List<string>();

            for (int i = 0; i < student.Length; i++)
            {
                if (!groups.Contains(student[i].Group))
                {
                    groups.Add(student[i].Group);
                }
            }
           
            foreach (var item in groups)
            {
                string name = item.ToString();
                string filePath = $"C:\\Users\\кек\\Desktop\\Students\\{name}";
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        for (int i = 0; i < student.Length; i++)
                        {
                            if (item == student[i].Group)
                            {
                                sw.WriteLine($"{student[i].Name}, {student[i].DateOfBirdth}");
                            }
                        }
                    } 
                }
            }
        }
    }
}
