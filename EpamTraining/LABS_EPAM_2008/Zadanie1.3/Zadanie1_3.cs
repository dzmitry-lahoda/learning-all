/*
 * Залание 1.3
 *В среде Visual Studio 2005 создать проект Empty Project, добавить новый файл 
 * исходного текста (выбрать из шаблонов «CodeFile»). Создать класс Apple 
 * c полем колво_семечек и методом Созревать(). Создать массив объектов Apple. 
 * Отсортировать по возрастанию кол-ва семечек и вывести информацию, вызвав 
 * метод.

*/

using System;

namespace EPAM.Zadanie1_3
{
    class Apple : IComparable,IEquatable<Apple>
    {
        private int kolvo_semechek;

        /// <summary>
        /// Gets and sets Kolvo_semechek. If Kolvo_semechek lesser then 0 sets 0.
        /// </summary>
        public int Kolvo_semechek
        {
            get
            {
                return kolvo_semechek;
            }
            set
            {
                if (value >= 0)
                {
                    kolvo_semechek = value;
                }
                else
                {
                    kolvo_semechek = 0;
                }
            }
        }

        public Apple()
        {
            Kolvo_semechek = 0;
        }

        public Apple(int kolvo_semechek)
        {
            Kolvo_semechek = kolvo_semechek;
        }

        /// <summary>
        /// Writes to console Kolvo_semechek
        /// </summary>
        public void Sozrevat()
        {
            Console.WriteLine(kolvo_semechek);
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj != null && obj is Apple)
            {
                Apple apple = (Apple)obj;
                if (apple > this)
                {
                    return -1;
                }
                if (apple < this)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }


        public override bool Equals(Object obj)
        {
            if (obj != null && obj is Apple)
            {
                Apple apple = (Apple)obj;
                return (this.Kolvo_semechek == apple.Kolvo_semechek);
            }
            return false; 
        }

        public override string ToString()
        {
 	        return String.Format("Apple.Kolvo_semechek={0}",kolvo_semechek);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        bool IEquatable<Apple>.Equals(Apple other)
        {
            return this.Equals(other);
        }

        public static bool operator ==(Apple a, Apple b)
        {
            return (a.Equals(b));
        }
        public static bool operator !=(Apple a, Apple b)
        {
            return !(a.Equals(b));
        }
        public static bool operator >(Apple a, Apple b)
        {
            return (a.Kolvo_semechek > b.Kolvo_semechek);
        }
        public static bool operator <(Apple a, Apple b)
        {
            return (a.Kolvo_semechek < b.Kolvo_semechek);
        }
    }

    static class Program
    {
        static void Main()
        {
            Random random = new Random();

            Apple[] apples = new Apple[10];
            for (int i = 0; i < apples.Length; i++)
            {
                apples[i] = new Apple(random.Next(100));
            }
            Array.Sort(apples);
            for (int i = 0; i < apples.Length; i++)
            {
                apples[i].Sozrevat();
            }
            Console.ReadLine();
        }
    }
}