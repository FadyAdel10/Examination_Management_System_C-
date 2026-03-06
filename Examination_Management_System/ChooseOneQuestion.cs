using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System
{
    internal class ChooseOneQuestion: Question
    {
        public ChooseOneQuestion(string header, string body, int marks, AnswerList answers, AnswerList correctAnswer) : base(header, body, marks, answers, correctAnswer)
        {
            if (correctAnswer.Count != 1)
            {
                throw new ArgumentException("Question must have one correct answer only");
            }
        }
        public ChooseOneQuestion(ChooseOneQuestion secondChooseOneQuestion) : base(secondChooseOneQuestion.Header,
            secondChooseOneQuestion.Body, secondChooseOneQuestion.Marks, secondChooseOneQuestion.Answers, secondChooseOneQuestion.CorrectAnswer)
        { }
        public override void Display()
        {
            Console.WriteLine("Choose one question");
            Console.WriteLine($"{Header}\n{Body}");
            for (int i = 0; i < Answers.Count; i++)
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
            return new ChooseOneQuestion(this);
        }
    }
}
