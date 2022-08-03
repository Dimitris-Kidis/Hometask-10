using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections;
using System.Reflection;


// 1. Create a collection ✅
// 2. Use delegates ✅
// 3. Rewrite using anonymous functions ✅
// 4. Rewrite using lambda expressions ✅
// 5. Using extension methods on the collection ✅
// 6. Using Select/Where operators on the collection ✅


namespace App
{
    class Program
    {

        public delegate void CustomNameOperations(List<Person> list); // Declaring a delegate




        static void Main(string[] args)
        {

            // Colletion
            var list = new List<Person>
            {
                new Person(0, "John", "Smith", "Baker St. 221b", "London", "01-01-2000", "+2 453 544 553", "M"),
                new Person(1, "Emma", "Doe", "Lupu St. 52", "Moscow", "05-07-1997", "+2 453 544 553", "F"),
                new Person(2, "Liam", "Jones", "Bucuresti St. 84", "Kiev", "14-03-1988", "+2 453 544 553", "M"),
                new Person(3, "Amelia", "Williams", "Armeneasca St. 93", "Chisinau", "07-12-2009", "+2 453 544 553", "F"),
                new Person(4, "Oliver", "Brown", "Creanga St. 11", "Havana", "11-04-2008", "+2 453 544 553", "M"),
                new Person(5, "Mia", "Johnson", "31 August St. 32", "Cairo", "25-06-2004", "+2 453 544 553", "F"),
                new Person(6, "James", "Taylor", "Columna St. 6", "Beijing", "09-01-1998", "+2 453 544 553", "M"),
                new Person(7, "Sophia", "Evans", "Siusev St. 97", "Tallinn", "29-07-1995", "+2 453 544 553", "F"),
                new Person(8, "William", "Wilson", "Puskin St. 53", "Tbilisi", "12-09-2001", "+2 453 544 553", "M"),
                new Person(9, "Bella", "Roberts", "Matievici St. 32", "Paris", "19-04-2002", "+2 453 544 553", "F"),
                new Person(10, "Benjamin", "Thomas", "Izvor St. 2", "Rome", "02-11-1978", "+2 453 544 553", "M")
            };


            // Assigning methods to delegates
            CustomNameOperations op1 = PrintNames;
            CustomNameOperations op2 = PrintAllMaleNames;
            CustomNameOperations op3 = PrintAllFemaleNames;

            op1(list);
            op2(list);
            op3(list);


            // Anonymously Created PrintAllFemaleNames Method
            CustomNameOperations op4 = delegate (List<Person> list)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----- Female Names -----");
                foreach (var item in list)
                {
                    if (item.Gender == "F") Console.WriteLine(item.FirstName + " " + item.LastName);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            };
            

            // One more usage of an anonymous delegate
            //PrintPeopleOlderThanEightteen(list);

            List<Person> PeopleOlderThan18 = list.FindAll(delegate (Person element)
            {
                //if (DateTime.Now.Year - Int32.Parse(item.BirthDate.Substring(6, 4)) > 18) return
                    return DateTime.Now.Year - Int32.Parse(element.BirthDate.Substring(6, 4)) > 18;
            });
            Console.WriteLine("------------ People Over 18 -------------");
            
            foreach (Person element in PeopleOlderThan18)
            {
                Console.WriteLine(element.ToString());
            }


            // Assigning a delegate using Lambda expression
            CustomNameOperations op5 = (List<Person> list) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n----- Names Beginning With J -----");
                foreach (var item in list)
                {
                    if (item.FirstName.Substring(0,1) == "J") Console.WriteLine(item.FirstName + " " + item.LastName);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            };
            op5(list);

            // Lambda expression for incrementing IDs
            CustomNameOperations op6 = (List<Person> list) =>
            {
                foreach (var item in list)
                {
                    item.ID++;
                    Console.WriteLine(item.ToString());
                }
            };
            Console.WriteLine("------- List With Incremented IDs -------");
            op6(list);


            // Extenstion Method
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n------- Check if a person with ID = 0 older than 24 -------");
            var olderThan24 = list.IsOlderThan(0, 24);
            Console.WriteLine(olderThan24);
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n------- Print people from London -------");
            var city = list.CheckCity("London"); 
            foreach (var item in city)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ForegroundColor = ConsoleColor.White;




            // Select & Where
            Console.WriteLine("\n------- Print people with ID greater than 5 and older than 20 -------");
            // Fluent Style
            var filteredListFluentStyle1 = list
                                .Where(x => x.ID > 5 && x.Age > 20)
                                .OrderBy(x => x.Age)
                                .Select(x => new { Name = x.LastName});
            var type5 = new
            {
                Name = "John"
            };
            
            foreach (var item in filteredListFluentStyle1)
            {
                Console.WriteLine(item);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n------- Print people with ID less than 5 and under 20 -------");
            // Query Style
            var filteredListQueryStyle1 = from person in list
                                          where person.ID < 5 && person.Age < 20
                                          orderby person.Age
                                          select person.ID + " " + person.FirstName + " " + person.LastName + ", " + person.Age;


            foreach (var item in filteredListQueryStyle1)
            {
                Console.WriteLine(item);
            }

            // Action & Func Delegates
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n------- Action Delegate Work -------");

            Action<int, int> actionDelegate = Sum;
            actionDelegate += Multiply;
            actionDelegate += Divide;
            actionDelegate += Subtract;

            actionDelegate(100, 20);

            Console.WriteLine("\n------- Func Delegate Work -------");
            Func<List<Person>, double> funcDelegate = AverageAge;

            Console.WriteLine("Average Person's Age: " + funcDelegate(list) + " years old");





        }

        public static void PrintNames(List<Person> list) {
            Console.WriteLine("----- All The Names -----");
            foreach (var item in list)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintAllMaleNames(List<Person> list)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("----- Male Names -----");
            foreach (var item in list)
            {
                if (item.Gender == "M") Console.WriteLine(item.FirstName + " " + item.LastName);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintAllFemaleNames(List<Person> list)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----- Female Names -----");
            foreach (var item in list)
            {
                if (item.Gender == "F") Console.WriteLine(item.FirstName + " " + item.LastName);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintPeopleOlderThanEightteen(List<Person> list)
        {
            foreach (var item in list)
            {
                if ( DateTime.Now.Year - Int32.Parse(item.BirthDate.Substring(6, 4)) > 18 )
                    Console.WriteLine(item.FirstName + " " + item.LastName);
            }
        }
        // Action Delegate Work Example
        public static void Sum(int x, int y)
        {
            Console.WriteLine($"{x} + {y} = {x + y}");
        }
        public static void Multiply(int x, int y)
        {
            Console.WriteLine($"{x} * {y} = {x * y}");
        }
        public static void Divide(int x, int y)
        {
            if (y == 0) throw new ArithmeticException("Nope");
            Console.WriteLine($"{x} : {y} = {x / y}");
        }
        public static void Subtract(int x, int y)
        {
            Console.WriteLine($"{x} - {y} = {x - y}");
        }

        // Func Deleagte Work Example
        public static double AverageAge (List<Person> list)
        {
            double tmp = 0;
            foreach (var item in list)
            {
                tmp += item.Age;
            }
            return Math.Round(tmp / list.Count, 2);
        }

    }


    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string BirthDate { get; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age {
            get 
            {
                return DateTime.Now.Year - Int32.Parse(BirthDate.Substring(6, 4));
            }
        }
        public Person(int iD, string firstName, string lastName, string address, string city, string birthDate, string phoneNumber, string gender)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"[{ID,2}]: {FirstName,10} {LastName,10}, {BirthDate,10}, from {Address}, {City}. ({PhoneNumber})";
        }
    }


   
}