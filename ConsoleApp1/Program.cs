using System;
using System.Collections.Generic;

class FairSplit
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to FairSplit!");

        // Get total amount
        Console.Write("Enter the total amount (£): ");
        decimal totalAmount = Convert.ToDecimal(Console.ReadLine());

        // Get number of people
        Console.Write("Enter the number of people to split the bill: ");
        int numberOfPeople = Convert.ToInt32(Console.ReadLine());

        // Calculate and display the share
        if (numberOfPeople <= 0)
        {
            Console.WriteLine("Number of people must be greater than zero.");
        }
        else
        {
            decimal share = totalAmount / numberOfPeople;
            Console.WriteLine($"\nEach person should pay: £{share:F2}");
        }

        Console.WriteLine("\nThanks for using FairSplit!");
    }
}
// **Features to add:**
//- Input validation to prevent dividing by zero
// Simple and clean calculation
//- Formatted output for currency

// expand it with names, rounding options

