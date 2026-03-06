using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System.Answer_Files
{
    internal class AnswerList
    {
        private Answer[] answers;
        public int Count { get; set; }
        public Answer this[int index]
        {
            get
            {
                if(index >= 0 && index < Count)
                {
                    return answers[index];
                }
                return null;
            }
            set
            {
                if (index >= 0 && index < Count)
                {
                    answers[index] = value;
                }
            }
        }
        public AnswerList(int size = 5)
        {
            answers = new Answer[size];
        }
        public AnswerList(Answer[] secondAnswers)
        {
            Count = secondAnswers.Length;
            answers = new Answer[Count];
            for (int i = 0; i < Count; i++)
            {
                answers[i] = new Answer(secondAnswers[i]);
            }
        }
        public AnswerList(AnswerList secondAnswerList)
        {
            Count = secondAnswerList.Count;
            answers = new Answer[Count];
            for(int i=0;i < Count; i++)
            {
                answers[i] = new Answer(secondAnswerList.answers[i]);
            }
        }
        public void Add(Answer answer)
        {
            if(Count == answers.Length)
            {
                Array.Resize(ref answers, answers.Length * 2);
            }
            answers[Count++] = answer;
        }
        public Answer GetById(int id)
        {
            for(int i=0;i< Count; i++)
            {
                if (answers[i].Id == id)
                {
                    return answers[i];
                }
            }
            return null;
        }
        public override bool Equals(object? obj)
        {
            if (obj is AnswerList secondAnswerList && obj != null)
            {
                if (this.GetType() != secondAnswerList.GetType())
                {
                    return false;
                }
                if (Object.ReferenceEquals(this, secondAnswerList))
                {
                    return true;
                }
                if (secondAnswerList != null && Count == secondAnswerList.Count)
                {
                    for(int i=0;i< Count; i++)
                    {
                        if (!(answers[i].Equals(secondAnswerList.answers[i])))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            string answersContent = "";
            for(int i=0;i< Count - 1;i++)
            {
                answersContent += answers[i].ToString()+", ";
            }
            answersContent += answers[Count - 1];
            return answersContent;
        }
    }
}
