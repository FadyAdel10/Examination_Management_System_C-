using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System
{
    internal class Subject
    {
        public string Name { get; set; }
        public List<Student> EnrolledStudents { get; set; }
        public Subject(string name,int numOfStudents = 5)
        {
            Name = name;
            EnrolledStudents = new List<Student>();
        }
        public Subject(Subject secondSubject)
        {
            Name = secondSubject.Name;
            EnrolledStudents = new List<Student>();
            for (int i = 0; i < secondSubject.EnrolledStudents.Count; i++)
            {
                EnrolledStudents.Add(new Student(secondSubject.EnrolledStudents[i]));
            }
        }
        public void Enroll(Student student)
        {
            EnrolledStudents.Add(student);
            
        }

        public void NotifyStudents(Exam exam)
        {
            for(int i=0;i<EnrolledStudents.Count;i++)
            {
                exam.ExamStarted += EnrolledStudents.ElementAt(i).OnExamStarted;
            }
        }
    }
}
