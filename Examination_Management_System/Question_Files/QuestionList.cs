using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System.Question_Files
{
    internal class QuestionList: List<Question>
    {
        public string FileName { get; set; }
        public QuestionList(string fileName)
        {
            // check uniqueness of files?
            if(string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Invalid file name, as file name can not be empty or null");
            }
            FileName = fileName;
        }
        public void Add(Question question)
        {
            if(question == null)
            {
                Console.WriteLine("Invalid question can not be added");
                return;
            }
            // try catch block to make operation atomic, as if file can be created and question logged in it, then add this question in question list
            try
            {
                using (StreamWriter writer = new StreamWriter(FileName, true))
                { 
                    writer.WriteLine(question.ToString());
                }
                base.Add(question);
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException("Can not add question to question list",exception);
            }
            
        }

    }
}
