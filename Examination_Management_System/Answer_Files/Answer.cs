using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System.Answer_Files
{
    internal class Answer: IComparable<Answer>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Answer(int id,string text)
        {
            Id = id;
            Text = text;
        }
        public Answer(Answer secondAnswer)
        {
            Id = secondAnswer.Id;
            Text = secondAnswer.Text;
        }
        public override string ToString()
        {
            return $"{Id}. {Text}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Answer secondAnswer && obj != null)
            {
                if (this.GetType() != secondAnswer.GetType())
                {
                    return false;
                }
                if (Object.ReferenceEquals(this, secondAnswer))
                {
                    return true;
                }
                return (Id == secondAnswer.Id && Text == secondAnswer.Text);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id,Text);
        }

        public int CompareTo(Answer? otherAnswer)
        {
            if(otherAnswer != null)
            {
                return Id.CompareTo(otherAnswer.Id);
            }
            return 1;
        }

    }
}
