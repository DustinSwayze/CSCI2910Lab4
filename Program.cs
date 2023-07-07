using System;
using System.IO;

namespace CSCI2910Lab4
{
    //FileRoot class to find the root directory
    public class FileRoot 
    {
        public static string GetProjectRoot()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectRoot = Directory.GetParent(currentDirectory).Parent.FullName;
            return projectRoot;
        }
    }

    //basic person class w/ properties and tostring with psv
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public Phone Phone { get; set; }

        public override string ToString()
        {
            return $"{FirstName}|{LastName}|{Address}|{Phone}";
        }
    }

    //basic address class w/ properties and tostring
    public class Address
    {
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string ZipCode { get; set; }

        public override string ToString()
        {
            return $"{Number} {Street}, {City}, {State.Abbreviation}, {ZipCode}";
        }
    }

    //basic state class with properties
    public class State
    {
        public string Abbreviation { get; set; }
        public string FullName { get; set; }
    }

    //basic phone class w/ properties and tostring
    public class Phone
    {
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string ExchangeCode { get; set; }
        public string LineNumber { get; set; }

        public string FullNumber => $"{CountryCode}-{AreaCode}-{ExchangeCode}-{LineNumber}";

        public override string ToString()
        {
            return FullNumber;
        }
    }

    public class Program
    {
        public static void Main()
        {
            string projectRoot = FileRoot.GetProjectRoot();
            string csvFilePath = Path.Combine(projectRoot, "data.csv");
            string psvFilePath = Path.Combine(projectRoot, "data.psv");

            var people = LoadPeopleFromCSV(csvFilePath);
            ExportToPSV(people, psvFilePath);

            Console.WriteLine("CSV to PSV conversion completed.");
        }


        //method that returns a Person array by passing it the csv
        public static Person[] LoadPeopleFromCSV(string filePath)
        {
            var people = new List<Person>();

            //crunch the data where the comma splits
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    var person = new Person
                    {
                        FirstName = data[0],
                        LastName = data[1],
                        Address = new Address
                        {
                            Number = data[2],
                            Street = data[3],
                            City = data[4],
                            State = new State
                            {
                                Abbreviation = data[5],
                                FullName = data[6]
                            },
                            ZipCode = data[7]
                        },
                        Phone = new Phone
                        {
                            CountryCode = data[8],
                            AreaCode = data[9],
                            ExchangeCode = data[10],
                            LineNumber = data[11]
                        }
                    };

                    people.Add(person);
                }
            }

            return people.ToArray();
        }

        //export data to .psv
        public static void ExportToPSV(Person[] people, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var person in people)
                {
                    writer.WriteLine(person.ToString());
                }
            }
        }
    }

}