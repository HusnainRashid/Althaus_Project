using System;
using System.Collections.Generic;
using System.Linq;

class Person
{
    public string Name { get; set; }
    public bool HasPaid { get; set; }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FairSplit ===\n");

        var people = new List<Person>();

        // Input: Get up to 10 people
        int num;
        while (true)
        {
            Console.Write("How many people? (1-10): ");
            if (int.TryParse(Console.ReadLine(), out num) && num > 0 && num <= 10)
                break;
            Console.WriteLine("Invalid number.");
        }

        for (int i = 0; i < num; i++)
        {
            Console.Write($"Name of person {i + 1}: ");
            string name = Console.ReadLine();

            bool paid;
            while (true)
            {
                Console.Write("Has this person paid? (y/n): ");
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "y" || input == "yes") { paid = true; break; }
                if (input == "n" || input == "no")  { paid = false; break; }
                Console.WriteLine("Please enter 'y' or 'n'.");
            }

            people.Add(new Person { Name = name, HasPaid = paid });
            Console.WriteLine();
        }

        // Input: Total amount
        decimal total;
        while (true)
        {
            Console.Write("Enter total expense (£): ");
            if (decimal.TryParse(Console.ReadLine(), out total) && total > 0)
                break;
            Console.WriteLine("Invalid amount.");
        }

        // Calculate and output
        decimal share = total / num;
        Console.WriteLine($"\nEqual share per person: £{share:F2}");

        Console.WriteLine("\n--- Paid ---");
        foreach (var p in people.Where(p => p.HasPaid))
            Console.WriteLine($"{p.Name} has already paid.");

        Console.WriteLine("\n--- Unpaid ---");
        foreach (var p in people.Where(p => !p.HasPaid))
            Console.WriteLine($"{p.Name} owes £{share:F2}");

        Console.WriteLine("\nThanks for using FairSplit!");
    }
}