using Examination_Management_System.Answer_Files;
using Examination_Management_System.Question_Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System.Exam_Files
{
    internal class FinalExam: Exam
    {
        public FinalExam(int time, int numberOfQuestions, Question[] questions,
            Subject subject, ExamMode mode) : base(time, numberOfQuestions, questions, subject, mode)
        {

        }
        public FinalExam(int time, int numberOfQuestions, Question[] questions, Dictionary<Question, AnswerList> questionAnswerDictionary,
            Subject subject, ExamMode mode) : base(time, numberOfQuestions, questions, questionAnswerDictionary, subject, mode)
        {

        }
        public FinalExam(FinalExam secondFinalExam) : base(secondFinalExam.Time, secondFinalExam.NumberOfQuestions, secondFinalExam.Questions,
            secondFinalExam.QuestionAnswerDictionary, secondFinalExam.Subject, secondFinalExam.Mode)
        {

        }
        public override void Finish()
        {
            base.Finish();
            StringBuilder questionsAndStudentAnswers = new StringBuilder();
            foreach (var pair in QuestionAnswerDictionary)
            {
                // show question
                questionsAndStudentAnswers.Append($"Question:\n\n{pair.Key.ToString()}").Append("\n");
                // show student answer
                questionsAndStudentAnswers.Append($"Student answer:\n\n{pair.Value}").Append("\n\n");
            }

            //  Show only questions and student answers 
            Console.WriteLine($"Questions and student answers:\n\n{questionsAndStudentAnswers.ToString()}");
        }
        // check is this correct implementation for ShowExam()
        public override void ShowExam()
        {
            string examContent = base.ToString();
            Console.WriteLine("Practical exam: ");
            Console.WriteLine(examContent);
            foreach (var question in Questions)
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
                    while (!valid)
                    {
                        string input = Console.ReadLine();
                        if (!int.TryParse(input, out answerId))
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
            return new FinalExam(this);
        }
    }
}
