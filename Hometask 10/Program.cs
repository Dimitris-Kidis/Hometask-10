using System;
using System.Linq;


// 1. Create a collection ✅
// 2. Use delegates ✅
// 3. Rewrite using anonymous functions ✅
// 4. Rewrite using lambda expressions ✅
// 5. Using extension methods on the collection
// 6. Using Select/Where operators on the collection


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
            PrintPeopleOlderThanEightteen(list);

            List<Person> PeopleOlderThan18 = list.FindAll(delegate (Person element)
            {
                //if (DateTime.Now.Year - Int32.Parse(item.BirthDate.Substring(6, 4)) > 18) return
                    return DateTime.Now.Year - Int32.Parse(element.BirthDate.Substring(6, 4)) > 18;
            });
            Console.WriteLine("----------------------------");
            foreach (Person element in PeopleOlderThan18)
            {
                Console.WriteLine(element.ToString());
            }


            // Assigning a delegate using Lambda expression
            CustomNameOperations op5 = (List<Person> list) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----- Names Beginning With J -----");
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
            op6(list);


        }

        public static void PrintNames(List<Person> list) {
            Console.WriteLine("----- All The Names -----");
            foreach (var item in list)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
            }
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
    }


    class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string BirthDate { get; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
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