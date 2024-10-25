using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Variable to control the main menu loop
        bool continueRunning = true;

        while (continueRunning)
        {
            // Display the main menu
            Console.WriteLine("Welcome to the Mindfulness App!");
            Console.WriteLine("Select an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Enter your choice (1-4): ");
            string choice = Console.ReadLine();

            // Handle user input for activity selection
            switch (choice)
            {
                case "1":
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.StartBreathing(); // Start the breathing activity
                    break;

                case "2":
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.StartReflection(); // Start the reflection activity
                    break;

                case "3":
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.StartListing(); // Start the listing activity
                    break;

                case "4":
                    continueRunning = false; // Exit the loop and quit the program
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again."); // Handle invalid input
                    break;
            }
        }
    }
}

// Base class representing a general activity
class Activity
{
    protected string _name; // Name of the activity
    protected string _description; // Description of the activity
    protected int _duration; // Duration of the activity in seconds

    // Common method to start an activity
    public void StartActivity()
    {
        Console.WriteLine($"Activity: {_name}"); // Display activity name
        Console.WriteLine($"Description: {_description}"); // Display activity description
        Console.Write("Enter duration in seconds: "); // Prompt user for duration
        _duration = int.Parse(Console.ReadLine()); // Set duration
        Console.WriteLine("Get ready...");
        ShowSpinner(3); // Show spinner for preparation time
    }

    // Common method to end an activity
    public void EndActivity()
    {
        Console.WriteLine("Well done!"); // Congratulate user
        ShowSpinner(3); // Show spinner before concluding
        Console.WriteLine($"You completed the {_name} for {_duration} seconds."); // Display completion message
    }

    // Method to display a spinner animation for a specified duration
    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds * 4; i++) // 4 states per second
        {
            Console.Write("/"); 
            Thread.Sleep(250); // Wait for a short duration
            Console.Write("\b"); // Erase the previous character
            Console.Write("-"); 
            Thread.Sleep(250); 
            Console.Write("\b"); 
            Console.Write("\\"); 
            Thread.Sleep(250); 
            Console.Write("\b"); 
            Console.Write("|"); 
            Thread.Sleep(250); 
            Console.Write("\b"); // Prepare for the next character
        }
        Console.WriteLine(); // Move to the next line after the spinner completes
    }
}

// Class representing the Breathing Activity
class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Activity"; // Set activity name
        _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing."; // Set activity description
    }

    // Method to start the breathing activity
    public void StartBreathing()
    {
        StartActivity(); // Call base method to start the activity
        int elapsed = 0; // Variable to track elapsed time

        // Loop until the specified duration is reached
        while (elapsed < _duration)
        {
            Console.WriteLine("Breathe in..."); // Prompt to breathe in
            ShowSpinner(4); // Show spinner for breathing in
            Console.WriteLine("Breathe out..."); // Prompt to breathe out
            ShowSpinner(4); // Show spinner for breathing out
            elapsed += 8; // Update elapsed time (4 seconds in + 4 seconds out)
        }

        EndActivity(); // Call base method to end the activity
    }
}

// Class representing the Reflection Activity
class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string> // List of reflection prompts
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string> // List of reflection questions
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        _name = "Reflection Activity"; // Set activity name
        _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life."; // Set activity description
    }

    // Method to start the reflection activity
    public void StartReflection()
    {
        StartActivity(); // Call base method to start the activity
        Random rand = new Random(); // Random number generator for selecting prompts/questions
        int elapsed = 0; // Variable to track elapsed time

        // Loop until the specified duration is reached
        while (elapsed < _duration)
        {
            string prompt = _prompts[rand.Next(_prompts.Count)]; // Select a random prompt
            Console.WriteLine(prompt); // Display the prompt
            ShowSpinner(3); // Show spinner for prompt display
            for (int i = 0; i < 3; i++) // Show 3 random questions
            {
                string question = _questions[rand.Next(_questions.Count)]; // Select a random question
                Console.WriteLine(question); // Display the question
                ShowSpinner(5); // Show spinner for each question
            }
            elapsed += 15; // Update elapsed time (3 seconds for prompt + 3*5 seconds for questions)
        }

        EndActivity(); // Call base method to end the activity
    }
}

// Class representing the Listing Activity
class ListingActivity : Activity
{
    private List<string> _prompts = new List<string> // List of listing prompts
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        _name = "Listing Activity"; // Set activity name
        _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."; // Set activity description
    }

    // Method to start the listing activity
    public void StartListing()
    {
        StartActivity(); // Call base method to start the activity
        Random rand = new Random(); // Random number generator for selecting prompts
        string prompt = _prompts[rand.Next(_prompts.Count)]; // Select a random prompt
        Console.WriteLine(prompt); // Display the prompt
        ShowSpinner(3); // Countdown for thinking time

        Console.WriteLine("Start listing your items now (press enter for each item, type 'done' to finish):");
        List<string> items = new List<string>(); // List to store user items
        string input; // Variable to store user input

        // Loop to collect user input until they type 'done'
        while (true)
        {
            input = Console.ReadLine(); // Read user input
            if (input.ToLower() == "done") break; // Exit loop if user types 'done'
            items.Add(input); // Add input to the list
        }

        Console.WriteLine($"You listed {items.Count} items."); // Display the count of listed items
        EndActivity(); // Call base method to end the activity
    }
}
