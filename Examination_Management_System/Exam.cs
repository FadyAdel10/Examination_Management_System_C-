using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Timers;

namespace Examination_Management_System
{
    internal abstract class Exam: ICloneable, IComparable<Exam>
    {
        public int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public Question[] Questions { get; set; }
        public Dictionary<Question, AnswerList> QuestionAnswerDictionary { get; set; }
        public Subject Subject { get; set; }
        public ExamMode Mode { get; set; }

        public Exam(int time, int numberOfQuestions, Question[] questions, Subject subject, ExamMode mode)
        {
            Time = time;
            NumberOfQuestions = numberOfQuestions;
            Mode = mode;

            Questions = new Question[questions.Length];
            QuestionAnswerDictionary = new Dictionary<Question, AnswerList>();
            for (int i = 0; i < Questions.Length; i++)
            {
                Questions[i] = (Question)questions[i].Clone();
            }
            Subject = new Subject(subject);
        }
        public Exam(int time, int numberOfQuestions, Question[] questions, Dictionary<Question, AnswerList> questionAnswerDictionary, Subject subject, ExamMode mode)
        {
            Time = time;
            NumberOfQuestions = numberOfQuestions;
            Mode = mode;

            Questions = new Question[questions.Length];
            QuestionAnswerDictionary = new Dictionary<Question, AnswerList>();
            for (int i = 0; i < Questions.Length; i++)
            {
                Questions[i] = (Question)questions[i].Clone();
            }

            foreach (var pair in questionAnswerDictionary)
            {
                QuestionAnswerDictionary.Add((Question)pair.Key.Clone(), new AnswerList(pair.Value));
            }
            Subject = new Subject(subject);
        }
        public Exam(Exam secondExam)
        {
            Time = secondExam.Time;
            NumberOfQuestions = secondExam.NumberOfQuestions;
            Mode = secondExam.Mode;
            Questions = new Question[secondExam.Questions.Length];
            QuestionAnswerDictionary = new Dictionary<Question, AnswerList>();
            for (int i = 0; i < Questions.Length; i++)
            {
                Questions[i] = (Question)secondExam.Questions[i].Clone();
            }

            foreach (var pair in secondExam.QuestionAnswerDictionary)
            {
                QuestionAnswerDictionary.Add((Question)pair.Key.Clone(), new AnswerList(pair.Value));
            }
            Subject = new Subject(secondExam.Subject);
        }
        public abstract void ShowExam();

        
        public virtual void Start()
        {
            Mode = ExamMode.Starting;
            Subject.NotifyStudents(this);
            OnExamStarted(new ExamEventArgs(Subject,this));
        }
        public virtual void Finish()
        {
            Mode = ExamMode.Finished;
        }

        public double CorrectExam()
        {
            int studentMarksOfExam = 0;
            int totalMarksOfExam = 0;
            AnswerList examAnswer;
            AnswerList studentAnswer;
            foreach (var pair in QuestionAnswerDictionary)
            {
                studentAnswer = pair.Value;
                examAnswer = pair.Key.CorrectAnswer;
                totalMarksOfExam += pair.Key.Marks;
                if(pair.Key.CheckAnswer(studentAnswer))
                {
                    studentMarksOfExam += pair.Key.Marks;
                }
            }
            double grade = (studentMarksOfExam * 100.00) / totalMarksOfExam;
            grade = Math.Round(grade, 2);
            return grade;
        }
        public override string ToString()
        {
            StringBuilder examContent = new StringBuilder($"Subject: {Subject.Name}\nExam time: {Time}\nNumber of questions: {NumberOfQuestions}\n\n");
            //for(int i=0;i<Questions.Length;i++)
            //{
            //    examContent.Append(Questions[i].ToString()).Append("\n");
            //}
            return examContent.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Exam secondExam && obj != null)
            {
                if (this.GetType() != secondExam.GetType())
                {
                    return false;
                }
                if (Object.ReferenceEquals(this, secondExam))
                {
                    return true;
                }
                return EqualsHelpler(secondExam);
            }
            return false;
        }
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Time);
            hash.Add(NumberOfQuestions);
            hash.Add(Mode);
            hash.Add(Subject);

            if(Questions != null)
            {
                foreach(var question in Questions)
                {
                    hash.Add(question);
                }
            }
            if (QuestionAnswerDictionary != null)
            {
                foreach (var pair in QuestionAnswerDictionary)
                {
                    hash.Add(pair.Key);
                    hash.Add(pair.Value);
                }
            }
            return hash.ToHashCode();
        }
        public int CompareTo(Exam? otherExam)
        {
            if(otherExam != null)
            {
                if(Time == otherExam.Time)
                {
                    return NumberOfQuestions.CompareTo(otherExam.NumberOfQuestions);
                }
                return Time.CompareTo(otherExam.Time);
            }
            return 1;
        }
        public abstract object Clone();
        // Helper method to Equals()
        private bool EqualsHelpler(Exam secondExam)
        {
            AnswerList currentAnswer;
            if ((Time != secondExam.Time) || (NumberOfQuestions != secondExam.NumberOfQuestions)
                || (!Subject.Equals(secondExam.Subject)) || (Mode != secondExam.Mode))
            {
                return false;
            }
            if(Questions.Length != secondExam.Questions.Length)
            {
                return false;
            }
            for (int i = 0; i < Questions.Length; i++)
            {
                if (!Questions[i].Equals(secondExam.Questions[i]))
                {
                    return false;
                }
            }
            if (QuestionAnswerDictionary.Count != secondExam.QuestionAnswerDictionary.Count)
            {
                return false;
            }
            foreach (var pair in QuestionAnswerDictionary)
            {
                // Check key is found in second exam dictionary
                if (!secondExam.QuestionAnswerDictionary.TryGetValue(pair.Key,out currentAnswer))
                {
                    return false;
                }
                if(!(pair.Value.Equals(currentAnswer)))
                {
                    return false;
                }
            }
            return true;
        }

        protected string getExamMode(ExamMode examMode)
        {
            if(examMode == ExamMode.Starting)
            {
                return "starting";
            }
            else if(examMode == ExamMode.Queued)
            {
                return "queued";
            }
            else if(examMode == ExamMode.Finished)
            {
                return "finished";
            }
            else
            {
                return "invalid exam mode";
            }
        }


        public event ExamStartedHandler ExamStarted;
        protected virtual void OnExamStarted(ExamEventArgs e)
        {
            ExamStarted?.Invoke(this, e);
        }
    }
}
