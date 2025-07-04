using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SimpleFairSplit
{
    class Person
    {
        public string Name { get; }
        public bool HasPaid { get; }

        public Person(string name, bool hasPaid)
        {
            Name = name;
            HasPaid = hasPaid;
        }
    }

    class FairSplit
    {
        public List<Person> People { get; } = new();

        public void AddPerson(Person p) => People.Add(p);

        public decimal EqualShare(decimal total) => total / People.Count;

        public IDictionary<string, decimal> Owes(decimal total)
        {
            decimal share = EqualShare(total);
            return People
                .Where(p => !p.HasPaid)
                .ToDictionary(p => p.Name, _ => share);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== FairSplit (paid / unpaid view) ===\n");

            var split = new FairSplit();

            // 1. People (max 10)
            int count;
            while (true)
            {
                Console.Write("How many people? (max 10): ");
                if (int.TryParse(Console.ReadLine(), out count) && count is >= 1 and <= 10)
                    break;
                Console.WriteLine("Enter a number between 1 and 10.");
            }

            for (int i = 1; i <= count; i++)
            {
                string name;
                do
                {
                    Console.Write($"Name of person {i}: ");
                    name = Console.ReadLine()?.Trim();
                } while (string.IsNullOrWhiteSpace(name) ||
                         split.People.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));

                bool paid;
                while (true)
                {
                    Console.Write("Has this person already paid? (y/n): ");
                    var ans = Console.ReadLine()?.Trim().ToLowerInvariant();
                    if (ans is "y" or "yes") { paid = true; break; }
                    if (ans is "n" or "no")  { paid = false; break; }
                    Console.WriteLine("Type y/yes or n/no.");
                }

                split.AddPerson(new Person(name, paid));
                Console.WriteLine();
            }

            // 2.Total expense
            decimal totalExpense;
            while (true)
            {
                Console.Write("Total expense (£): ");
                if (decimal.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint,
                                     CultureInfo.InvariantCulture, out totalExpense) && totalExpense > 0)
                    break;
                Console.WriteLine("Enter a positive amount, e.g. 42.75");
            }

            // 3.Calculation
            decimal share = split.EqualShare(totalExpense);
            var owes = split.Owes(totalExpense);

            // 4.Output
            Console.WriteLine($"\nEqual share for everyone: £{share:F2}\n");

            Console.WriteLine("--- Paid ---");
            foreach (var p in split.People.Where(p => p.HasPaid))
                Console.WriteLine($"{p.Name,-20} £0.00 (already settled)");

            Console.WriteLine("\n--- Unpaid ---");
            if (owes.Count == 0)
            {
                Console.WriteLine("All settled!");
            }
            else
            {
                foreach (var kv in owes)
                    Console.WriteLine($"{kv.Key,-20} £{kv.Value:F2} still owed");
            }

            Console.WriteLine("\nDone. Thanks for using FairSplit!");
        }
    }
}