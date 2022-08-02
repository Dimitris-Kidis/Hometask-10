using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public static class NameExtension
    {
        public static bool IsOlderThan(this List<Person> list, int ID, int age)
        {
            return DateTime.Now.Year - Int32.Parse(list[ID].BirthDate.Substring(6, 4)) > age;
        }
    }


    public static class CityExtension
    {
        public static List<Person> CheckCity(this List<Person> list, string city)
        {
            List<Person> tmp = new List<Person>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].City == city)
                {
                    tmp.Add(list[i]);
                }
            }
            return tmp;
        }
    }
}
