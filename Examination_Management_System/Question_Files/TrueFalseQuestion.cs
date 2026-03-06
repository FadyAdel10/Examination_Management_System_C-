using Examination_Management_System.Answer_Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System.Question_Files
{
    internal class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string header, string body, int marks, AnswerList answers, AnswerList correctAnswer) : base(header, body, marks, answers, correctAnswer)
        {
            if(answers.Count != 2)
            {
                throw new ArgumentException("True/False question must have two answers only");
            }
            if(correctAnswer.Count != 1)
            {
                throw new ArgumentException("True/False question must have one correct answer only");
            }
        }
        public TrueFalseQuestion(TrueFalseQuestion secondTrueFalseQuestion) : base(secondTrueFalseQuestion.Header,
            secondTrueFalseQuestion.Body, secondTrueFalseQuestion.Marks, secondTrueFalseQuestion.Answers, secondTrueFalseQuestion.CorrectAnswer)
        { }
        public override void Display()
        {
            Console.WriteLine("True/False question");
            Console.WriteLine($"{Header}\n{Body}");
            for(int i=0;i<Answers.Count;i++)
            {
                Console.WriteLine(Answers[i]);
            }
            Console.WriteLine($"{Marks} grade.");
        }
        public override bool CheckAnswer(AnswerList studentAnswer)
        {
            return CorrectAnswer.Equals(studentAnswer);
        }

        public override object Clone()
        {
            return new TrueFalseQuestion(this);
        }
    }
}
