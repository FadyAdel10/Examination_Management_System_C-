

using Examination_Management_System.Answer_Files;
using Examination_Management_System.Exam_Files;
using Examination_Management_System.Question_Files;

namespace Examination_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Subject oop = new Subject("OOP");
            Subject dataStructure = new Subject("Data Structure");

            Student ahmed = new Student("Ahmed", 1);
            Student zaki = new Student("Zaki", 2);
            Student hany = new Student("Hany", 3);
            Student mona = new Student("Mona", 3);

            oop.Enroll(ahmed);
            oop.Enroll(zaki);
            oop.Enroll(hany);
            dataStructure.Enroll(mona);
            dataStructure.Enroll(hany);

            Answer oopQuestion1Answer1 = new Answer(1, "True");
            Answer oopQuestion1Answer2 = new Answer(2, "False");

            Answer oopQuestion2Answer1 = new Answer(1, "3");
            Answer oopQuestion2Answer2 = new Answer(2, "2");
            Answer oopQuestion2Answer3 = new Answer(3, "1");
            Answer oopQuestion2Answer4 = new Answer(4, "4");

            Answer oopQuestion3Answer1 = new Answer(1, "Encapsulation");
            Answer oopQuestion3Answer2 = new Answer(2, "Inheritance");
            Answer oopQuestion3Answer3 = new Answer(3, "Polymorphism");
            Answer oopQuestion3Answer4 = new Answer(4, "Abstraction");

            Answer dataStructureQuestion1Answer1 = new Answer(1, "True");
            Answer dataStructureQuestion1Answer2 = new Answer(2, "False");

            Answer dataStructureQuestion2Answer1 = new Answer(1, "O(n)");
            Answer dataStructureQuestion2Answer2 = new Answer(2, "O(n*m)");
            Answer dataStructureQuestion2Answer3 = new Answer(3, "O(1)");
            Answer dataStructureQuestion2Answer4 = new Answer(4, "O(n+m)");

            Answer dataStructureQuestion3Answer1 = new Answer(1, "Array");
            Answer dataStructureQuestion3Answer2 = new Answer(2, "Linked list");
            Answer dataStructureQuestion3Answer3 = new Answer(3, "Tree");
            Answer dataStructureQuestion3Answer4 = new Answer(4, "Graph");

            Answer[] oopQuestion1CorrectAnswers = [oopQuestion1Answer1];
            Answer[] oopQuestion2CorrectAnswers = [oopQuestion2Answer4];
            Answer[] oopQuestion3CorrectAnswers = [oopQuestion3Answer1, oopQuestion3Answer2,
                oopQuestion3Answer3,oopQuestion3Answer4];

            Answer[] oopQuestion1Answers = [oopQuestion1Answer1, oopQuestion1Answer2];
            Answer[] oopQuestion2Answers = [oopQuestion2Answer1, oopQuestion2Answer2
                , oopQuestion2Answer3, oopQuestion2Answer4];
            Answer[] oopQuestion3Answers = [oopQuestion3Answer1, oopQuestion3Answer2,
                oopQuestion3Answer3, oopQuestion3Answer4];


            Answer[] dataStructureQuestion1Answers = [dataStructureQuestion1Answer1, dataStructureQuestion1Answer2];
            Answer[] dataStructureQuestion2Answers = [dataStructureQuestion2Answer1, dataStructureQuestion2Answer2
                , dataStructureQuestion2Answer3, dataStructureQuestion2Answer4];
            Answer[] dataStructureQuestion3Answers = [dataStructureQuestion3Answer1, dataStructureQuestion3Answer2,
                dataStructureQuestion3Answer3, dataStructureQuestion3Answer4];

            Answer[] dataStructureQuestion1CorrectAnswers = [dataStructureQuestion1Answer2];
            Answer[] dataStructureQuestion2CorrectAnswers = [dataStructureQuestion2Answer1];
            Answer[] dataStructureQuestion3CorrectAnswers = [dataStructureQuestion3Answer1, dataStructureQuestion3Answer2,
                dataStructureQuestion3Answer3,dataStructureQuestion3Answer4];

            Question oopQuestion1 = new TrueFalseQuestion("True/False", "Does OOP stand for object oriented programming?",
                2, new AnswerList(oopQuestion1Answers),new AnswerList(oopQuestion1CorrectAnswers));
            Question oopQuestion2 = new ChooseOneQuestion("choose one answer", "what are number of pillers in OOP?",
                2, new AnswerList(oopQuestion2Answers), new AnswerList(oopQuestion2CorrectAnswers));
            Question oopQuestion3 = new ChooseAllQuestion("choose more than one answer if there is", "Which of the following is OOP principle?",
                3, new AnswerList(oopQuestion3Answers), new AnswerList(oopQuestion3CorrectAnswers));

            Question dataStructureQuestion1 = new TrueFalseQuestion("True/False", "Does insertion sort faster than merge sort?",
                2, new AnswerList(dataStructureQuestion1Answers),new AnswerList(dataStructureQuestion1CorrectAnswers));

            Question dataStructureQuestion2 = new ChooseOneQuestion("choose one answer", "What is time complexity of for loop?",
                2, new AnswerList(dataStructureQuestion2Answers), new AnswerList(dataStructureQuestion2CorrectAnswers));
            Question dataStructureQuestion3 = new ChooseAllQuestion("choose more than one answer if there is", "Which of the following is data structure example?",
                3, new AnswerList(dataStructureQuestion3Answers), new AnswerList(dataStructureQuestion3CorrectAnswers));

            Question[] oopQuestions = [oopQuestion1, oopQuestion2, oopQuestion3];
            Question[] dataStructureQuestions = [dataStructureQuestion1, dataStructureQuestion2, dataStructureQuestion3];


            PracticeExam practiceExam = new PracticeExam(65, 3, oopQuestions, oop, ExamMode.Queued);
            FinalExam finalExam = new FinalExam(90, 3, dataStructureQuestions, dataStructure, ExamMode.Queued);

            
            int selectedExam;
            bool validInput = false;
            do
            {
                Console.WriteLine($"Select Exam Type:\n1 - Practice\n2 - Final ");
                int.TryParse(Console.ReadLine(), out selectedExam);

                if (selectedExam == 1)
                {
                    Console.Clear();
                    practiceExam.Start();
                    Console.WriteLine();
                    practiceExam.ShowExam();
                    practiceExam.Finish();
                    validInput = true;
                }
                else if (selectedExam == 2)
                {
                    Console.Clear();
                    finalExam.Start();
                    Console.WriteLine();
                    finalExam.ShowExam();
                    finalExam.Finish();
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                }
            } while (!validInput);

            // log questions in files
            QuestionList oopQuestionFile = new QuestionList("D:\\Faculty\\ITI\\C#\\Assignments\\Assignment_7\\Examination_Management_System\\Logged_Question_Files\\OOP.txt");
            QuestionList dataStructureQuestionFile = new QuestionList("D:\\Faculty\\ITI\\C#\\Assignments\\Assignment_7\\Examination_Management_System\\Logged_Question_Files\\Data_Structure.txt");

            oopQuestionFile.Add(oopQuestion1);
            oopQuestionFile.Add(oopQuestion2);
            oopQuestionFile.Add(oopQuestion3);

            dataStructureQuestionFile.Add(dataStructureQuestion1);
            dataStructureQuestionFile.Add(dataStructureQuestion2);
            dataStructureQuestionFile.Add(dataStructureQuestion3);
        }
    }
}
