using Examination_Management_System.Answer_Files;
using Examination_Management_System.Question_Files;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Examination_Management_System.Exam_Files
{
    internal class PracticeExam: Exam
    {
        public PracticeExam(int time, int numberOfQuestions, Question[] questions,
            Subject subject, ExamMode mode) : base(time, numberOfQuestions, questions, subject, mode)
        {

        }
        public PracticeExam(int time, int numberOfQuestions, Question[] questions, Dictionary<Question, AnswerList> questionAnswerDictionary,
            Subject subject, ExamMode mode): base(time,numberOfQuestions,questions,questionAnswerDictionary,subject,mode)
        {
            
        }
        public PracticeExam(PracticeExam secondPracticeExam) : base(secondPracticeExam.Time, secondPracticeExam.NumberOfQuestions, secondPracticeExam.Questions,
            secondPracticeExam.QuestionAnswerDictionary, secondPracticeExam.Subject, secondPracticeExam.Mode)
        {
            
        }
        public override void Finish()
        {
            base.Finish();
            StringBuilder studentAnswers = new StringBuilder();
            StringBuilder correctAnswers = new StringBuilder();
            foreach (var pair in QuestionAnswerDictionary)
            {
                correctAnswers.Append(pair.Key.CorrectAnswer.ToString()).Append("\n");
                studentAnswers.Append(pair.Value.ToString()).Append("\n");
            }
            //  Show student answers
            Console.WriteLine($"Student answers:\n{studentAnswers.ToString()}");
            //Show correct answers
            Console.WriteLine($"Correct answers:\n{correctAnswers.ToString()}");
            // Show final grade
            Console.WriteLine($"Final grade: {base.CorrectExam()}");
        }
        // check is this correct implementation for ShowExam()
        public override void ShowExam()
        {
            string examContent = base.ToString();
            Console.WriteLine("Practical exam: ");
            Console.WriteLine(examContent);
            foreach(var question in Questions)
            {
                AnswerList studentAnswer = new AnswerList();
                bool valid = false;
                //string[] answerIds;
                Console.WriteLine(question.ToString());
                int answerId;
                if (question is ChooseAllQuestion)
                {
                    while (!valid)
                    {
                        string input = Console.ReadLine();
                        string[] answerIds = input.Split(',');
                        studentAnswer = new AnswerList();
                        valid = true;
                        for (int i = 0; i < answerIds.Length; i++)
                        {
                            if (!int.TryParse(answerIds[i], out answerId))
                            {
                                Console.WriteLine("Invalid number format. Try again.");
                                valid = false;
                                break;
                            }

                            Answer answer = question.Answers.GetById(answerId);
                            if (answer == null)
                            {
                                Console.WriteLine($"Answer id {answerId} does not exist. Try again.");
                                valid = false;
                                break;
                            }
                            studentAnswer.Add(answer);
                        }
                    }
                }
                else
                {
                    while(!valid)
                    {
                        string input = Console.ReadLine();
                        if(!int.TryParse(input, out answerId))
                        {
                            Console.WriteLine("Invalid number format. Try again.");
                            valid = false;
                            continue;
                        }
                        Answer answer = question.Answers.GetById(answerId);
                        if (answer == null)
                        {
                            Console.WriteLine($"Answer id {answerId} does not exist. Try again.");
                            valid = false;
                            continue;
                        }
                        studentAnswer.Add(question.Answers.GetById(answerId));
                        valid = true;
                    }
                    
                }
                QuestionAnswerDictionary.Add(question, studentAnswer);
            }
        }

        public override object Clone()
        {
            return new PracticeExam(this);
        }
    }
}
