using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_Management_System
{
    internal class Repository<T> where T : ICloneable, IComparable<T>
    {
        private T[] repositoryList;
        private int count;
        public Repository(int size = 5)
        {
            repositoryList = new T[size];
        }
        public void Add(T item)
        {
            if(count == repositoryList.Length)
            {
                Array.Resize(ref repositoryList, repositoryList.Length * 2);
            }
            repositoryList[count++] = item;
        }

        public void Remove(T item)
        {
            int remove_index = -1;
            for(int i=0;i<repositoryList.Length;i++)
            {
                if (repositoryList[i].Equals(item))
                {
                    remove_index = i;
                    break;
                }
            }
            if(remove_index != -1)
            {
                for(int i=remove_index;i<count-1;i++)
                {
                    repositoryList[i] = repositoryList[i + 1];
                }
                count--;
            }
        }

        public void Sort()
        {
            Array.Sort(repositoryList, 0, count);
        }

        public T[] GetAll()
        {
            T[] result = new T[count];
            Array.Copy(repositoryList, result, count);
            return result;
        }

    }
}
