using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Examination_Management_System
{
    internal class ChooseAllQuestion: Question
    {
        public ChooseAllQuestion(string header, string body, int marks, AnswerList answers, AnswerList correctAnswer) : base(header, body, marks, answers, correctAnswer)
        { }
        public ChooseAllQuestion(ChooseAllQuestion secondChooseAllQuestion) : base(secondChooseAllQuestion.Header,
            secondChooseAllQuestion.Body, secondChooseAllQuestion.Marks, secondChooseAllQuestion.Answers, secondChooseAllQuestion.CorrectAnswer)
        { }
        public override void Display()
        {
            Console.WriteLine("Choose all question");
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
            return new ChooseAllQuestion(this);
        }
    }
}
