using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("WHAT IS YOUR GRADE PERCENTAGE");
        string gradePercentage = Console.ReadLine();
        int number = int.Parse(gradePercentage);
        string letter= ""

        if (number >= 90)
        {
            letter = "A";
        }
        else if (number >= 80)
        {
            letter = "B";
        }
        else if (number >= 70)
        {
            letter = "C";
        }
        else if (number >= 60)
        {
           letter = "D";
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your grade is: {letter}");

        if (number >= 70)
        {
            Console.WriteLine("Congratulations! You Passed");
        }
        else
        {
            Console.WriteLine("You did your best, but you can still try better next time");
        }
    }
}