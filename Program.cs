using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PhoneBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PhoneBook phoneBook = new PhoneBook();
            phoneBook.CheckDirectory();
            phoneBook.Read();

            Console.Write("Please choose an ordering to sort? :");
            var Ordering = Console.ReadLine();

            Console.Write("Please choose criteria? :");
            var sortBy = Console.ReadLine();
            SortingProperties sorting;
            Enum.TryParse<SortingProperties>(sortBy, out sorting);

            phoneBook.Sort(sorting, Ordering);
            Console.WriteLine();
            phoneBook.Print();
            phoneBook.Validate();
            Console.ReadLine();
        }
    }

    public class PhoneBook
    {
        private string path;
        private string text;
        List<PartsModel> sortText = new List<PartsModel>();
        public void Read()
        {
            using(var stream = new StreamReader(path))
            {
                text = stream.ReadToEnd();
            }
        }

        public void CheckDirectory()
        {
            DirectoryInfo d = new DirectoryInfo(@"D:\C# repository\PhoneBook\PhoneBook\Books");
            var dynamicPath = d.FullName;
            Console.Write("Path: ");
            var index = Console.ReadLine();
            path = dynamicPath + index;
        }
        
        public void Sort(SortingProperties howToSort, string OrderingBy)
        {
            List<PartsModel> parts = new List<PartsModel>();
            
            var lines = text.Split('\n');
            foreach(var line in lines)
            {
                if (!line.Any())
                    return;
                var x = line.Split(' ');
                
                parts.Add(new PartsModel()
                {
                    Name = x[0],
                    Lastname = x[1],
                    Separator = x[2],
                    PhoneNumber = x[3],
                });
            }

            if(OrderingBy == "Ascending")
            {
                switch (howToSort)
                {
                    case SortingProperties.Name:
                        sortText = parts.OrderBy(x => x.Name[0]).ToList();
                        break;
                    case SortingProperties.Lastname:
                        sortText = parts.OrderBy(x => x.Lastname[0]).ToList();
                        break;
                    case SortingProperties.PhoneNumber:
                        sortText = parts.OrderBy(x => x.PhoneNumber[0]).ToList();
                        break;
                }
            } else
            {
                switch (howToSort)
                {
                    case SortingProperties.Name:
                        sortText = parts.OrderByDescending(x => x.Name[0]).ToList();
                        break;
                    case SortingProperties.Lastname:
                        sortText = parts.OrderByDescending(x => x.Lastname[0]).ToList();
                        break;
                    case SortingProperties.PhoneNumber:
                        sortText = parts.OrderByDescending(x => x.PhoneNumber[0]).ToList();
                        break;
                }
            }
        }

        public void Print()
        {
            foreach (var item in sortText)
            {
                Console.Write(item.Name + ' ');
                Console.Write(item.Lastname + ' ');
                Console.Write(item.Separator + ' ');
                Console.Write(item.PhoneNumber);
                Console.WriteLine();
            }
        }

        public void Validate()
        {
            var counter = 1;
            foreach (var item in sortText)
            {
                var errors = "";
                errors += "line " + counter;
                if (item.PhoneNumber.Length - 1 != 9)
                {
                    errors += " Phone number should be with 9 digits";
                }
                if (item.Separator != ":" && item.Separator != "-")
                {
                    errors += " separator should be - or :";
                }
                counter++;
                Console.WriteLine(errors);
            }
        }
    }

    public class PartsModel
    {
        public string Name;
        public string Lastname;
        public string Separator;
        public string PhoneNumber;
    }
    
    public enum SortingProperties
    {
        Name,
        Lastname,
        Separator,
        PhoneNumber
    }
}
