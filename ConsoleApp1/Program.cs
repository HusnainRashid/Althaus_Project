using System;
using System.Collections.Generic;
using System.Linq;

class Person
{
    public string Name { get; set; }     // Person's name
    public bool HasPaid { get; set; }    // true if this person already paid
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FairSplit ===\n");

        //  Ask for people and paid status
        List<Person> people = GetPeopleList();

        //  Base total amount
        decimal baseTotal = GetAmount("Enter total expense (£): ");

        //  Optional extras (adds to the total)
        decimal extraTotal = GetExtraExpenses();
        decimal grandTotal = baseTotal + extraTotal;

        //  Equal share for everyone
        decimal share = grandTotal / people.Count;
        Console.WriteLine($"\nEach person should pay: £{share:F2}\n");

        //  Output paid list (owe nothing)
        Console.WriteLine("--- Paid ---");
        foreach (var p in people.Where(p => p.HasPaid))
            Console.WriteLine($"{p.Name} has already paid.");

        //  Output unpaid list (owe 'share')
        Console.WriteLine("\n--- Unpaid ---");
        foreach (var p in people.Where(p => !p.HasPaid))
            Console.WriteLine($"{p.Name} owes £{share:F2}");

        Console.WriteLine("\nThanks for using FairSplit!");
    }

    // ---------------- collect names & paid flag ----------------
    static List<Person> GetPeopleList()
    {
        var people = new List<Person>();

        // limit 1‑10
        int count;
        while (true)
        {
            Console.Write("How many people? (1-10): ");
            if (int.TryParse(Console.ReadLine(), out count) && count >= 1 && count <= 10)
                break;
            Console.WriteLine("Invalid number.");
        }

        // loop through each person
        for (int i = 0; i < count; i++)
        {
            // name (non‑empty, no duplicates)
            string name;
            do
            {
                Console.Write($"Name of person {i + 1}: ");
                name = Console.ReadLine().Trim();
            } while (string.IsNullOrWhiteSpace(name) ||
                     people.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));

            // ask if they already paid
            bool paid;
            while (true)
            {
                Console.Write("Has this person paid? (y/n): ");
                string ans = Console.ReadLine().Trim().ToLower();
                if (ans is "y" or "yes") { paid = true;  break; }
                if (ans is "n" or "no")  { paid = false; break; }
                Console.WriteLine("Please enter 'y' or 'n'.");
            }

            people.Add(new Person { Name = name, HasPaid = paid });
            Console.WriteLine();
        }

        return people;
    }

    // ----------------  read a positive decimal ------------------
    static decimal GetAmount(string prompt)
    {
        decimal value;
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out value) && value > 0)
                return value;
            Console.WriteLine("Invalid amount.");
        }
    }

    // ----------------  loop for extra expenses ------------------
    static decimal GetExtraExpenses()
    {
        decimal extra = 0m;

        while (true)
        {
            Console.Write("Add extra expense? (y/n): ");
            string input = Console.ReadLine().Trim().ToLower();

            if (input is "y" or "yes")
            {
                extra += GetAmount("Enter extra expense amount (£): ");
            }
            else if (input is "n" or "no")
            {
                return extra; // done
            }
            else
            {
                Console.WriteLine("Please enter 'y' or 'n'.");
            }
        }
    }
}