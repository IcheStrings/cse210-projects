using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("WHAT IS YOUR GRADE PERCENTAGE");
        string gradePercentage = Console.ReadLine();
        int number = int.Parse(gradePercentage);

        if (number >= 90)
        {
            Console.WriteLine("Your grade is A");
        }
        else if (number >= 80)
        {
            Console.WriteLine("Your grade is B");
        }
        else if (number >= 70)
        {
            Console.WriteLine("Your grade is C");
        }
        else if (number >= 60)
        {
            Console.WriteLine("Your grade is D");
        }
        else
        {
            Console.WriteLine("Your grade is F");
        }

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