using Examination_Management_System.Answer_Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System.Question_Files
{
    internal abstract class Question: ICloneable
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Marks { get; set; }
        public AnswerList Answers { get; set; }
        public AnswerList CorrectAnswer { get; set; }

        public Question(string header,string body,int marks, AnswerList answers, AnswerList correctAnswer)
        {
            if(marks <= 0)
            {
                throw new ArgumentException("Marks must be greater than zero");
            }
            if(header == null)
            {
                throw new ArgumentException("Invalid header question");
            }
            if (body == null)
            {
                throw new ArgumentException("Invalid body question");
            }
            Header = header;
            Body = body;
            Marks = marks;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
        // Copy constructor
        public Question(Question secondQuestion)
        {
            Header = secondQuestion.Header;
            Body = secondQuestion.Body;
            Marks = secondQuestion.Marks;
            Answers = new AnswerList(secondQuestion.Answers);
            CorrectAnswer = new AnswerList(secondQuestion.CorrectAnswer);
            for (int i=0;i<secondQuestion.Answers.Count;i++)
            {
                Answers[i] = new Answer(secondQuestion.Answers[i]);
            }
            for (int i = 0; i < secondQuestion.CorrectAnswer.Count; i++)
            {
                CorrectAnswer[i] = new Answer(secondQuestion.CorrectAnswer[i]);
            }
        }

        public abstract void Display();
        public abstract bool CheckAnswer(AnswerList studentAnswer);
        // check ToString() implementaion done
        public override string ToString()
        {
            // using stringBuilder instead of string to avoid creating many objects in memory as string is immutable 
            StringBuilder question = new StringBuilder($"Header: {Header}\nBody: {Body}\nMarks: {Marks}\n");
            for(int i=0;i<Answers.Count;i++)
            {
                question.Append(Answers[i].ToString()).Append("\n");
            }
            return question.ToString();
        }
        // check best practice for Equals() here done
        public override bool Equals(object? obj)
        {
            if(obj is Question secondQuestion&& obj != null)
            {
                if (this.GetType() != secondQuestion.GetType())
                {
                    return false;
                }
                if (Object.ReferenceEquals(this, secondQuestion))
                {
                    return true;
                }
                return EqualsHelpler(secondQuestion);
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(Header);
            hash.Add(Body);
            hash.Add(Marks);

            if (Answers != null)
            {
                for(int i=0;i<Answers.Count;i++)
                {
                    hash.Add(Answers[i]);
                }
            }
            if (CorrectAnswer != null)
            {
                for (int i = 0; i < CorrectAnswer.Count; i++)
                {
                    hash.Add(CorrectAnswer[i]);
                }
            }
            return hash.ToHashCode();
        }

        // Helper method to Equals()
        private bool EqualsHelpler(Question secondQuestion)
        {
            if((!Header.Equals(secondQuestion.Header)) || (!Body.Equals(secondQuestion.Body))
                || (!Marks.Equals(secondQuestion.Marks)) || (!CorrectAnswer.Equals(secondQuestion.CorrectAnswer)))
            {
                return false;
            }
            if(Answers.Count != secondQuestion.Answers.Count || CorrectAnswer.Count != secondQuestion.CorrectAnswer.Count)
            {
                return false;
            }
            for(int i=0;i< Answers.Count;i++)
            {
                if (!Answers[i].Equals(secondQuestion.Answers[i]))
                {
                    return false;
                }
            }
            for (int i = 0; i < CorrectAnswer.Count; i++)
            {
                if (!CorrectAnswer[i].Equals(secondQuestion.CorrectAnswer[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract object Clone();
    }
}
