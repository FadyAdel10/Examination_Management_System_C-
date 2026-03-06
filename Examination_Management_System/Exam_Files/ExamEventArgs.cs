using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System.Exam_Files
{
    internal class ExamEventArgs : EventArgs
    {
        public Subject Subject { get; }
        public Exam Exam { get; }

        public ExamEventArgs(Subject subject,Exam exam)
        {
            Subject = subject;
            Exam = exam;
        }
    }
}
