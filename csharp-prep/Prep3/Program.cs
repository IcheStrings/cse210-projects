using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int magicNumber = random.Next(1, 101); // Generate a random number from 1 to 100

        int userGuess = 0;
        
        Console.WriteLine("I've picked a number between 1 and 100. Try to guess it!");

        while (userGuess != magicNumber)
        {
            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();

            try
            {
                userGuess = int.Parse(input);

                if (userGuess < magicNumber)
                {
                    Console.WriteLine("Go higher.");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Go lower.");
                }
                else
                {
                    Console.WriteLine("Congratulations! You guessed it right.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}