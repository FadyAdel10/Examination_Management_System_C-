using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System
{
    internal class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Student(string name, int id)
        {
            Name = name;
            Id = id;
        }
        public Student(Student secondStudent)
        {
            Name = secondStudent.Name;
            Id = secondStudent.Id;
        }
        public void OnExamStarted(object sender, ExamEventArgs e)
        {
            if (sender is Exam exam && exam != null)
            {
                Console.WriteLine($"Dear {Name}, {exam.Subject.Name} exam will be start");
            }
        }
    }
}
