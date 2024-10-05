using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Entry
{
    // Adding new properties to store additional information
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }  // New: To store the location
    public string Mood { get; set; }      // New: To store the user's mood
    public List<string> Tags { get; set; } // New: To store tags related to the entry

    public Entry()
    {
        Tags = new List<string>();  // Initialize tags to avoid null references
    }

    public override string ToString()
    {
        // Modify the output to include the new fields (Location, Mood, Tags)
        string tagsFormatted = Tags.Count > 0 ? string.Join(", ", Tags) : "None";
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\nLocation: {Location}\nMood: {Mood}\nTags: {tagsFormatted}\n";
    }
}

public class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private Random _random = new Random();

    public void WriteNewEntry()
    {
        // Get random prompt
        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        // Ask user for additional information
        Console.Write("Location: ");
        string location = Console.ReadLine();

        Console.Write("Mood: ");
        string mood = Console.ReadLine();

        Console.Write("Enter tags (comma-separated): ");
        string tagsInput = Console.ReadLine();
        List<string> tags = tagsInput.Split(',').Select(tag => tag.Trim()).ToList();

        // Create new entry with additional information
        Entry newEntry = new Entry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now,
            Location = location,  // Store location
            Mood = mood,          // Store mood
            Tags = tags           // Store tags
        };

        // Save to journal
        _entries.Add(newEntry);
        Console.WriteLine("Entry saved.\n");
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries in the journal.\n");
        }
        else
        {
            foreach (var entry in _entries)
            {
                Console.WriteLine(entry);
            }
        }
    }

    public void SaveJournalToFile()
    {
        Console.Write("Enter filename to save journal: ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in _entries)
                {
                    writer.WriteLine(entry.Prompt);
                    writer.WriteLine(entry.Response);
                    writer.WriteLine(entry.Date);
                    writer.WriteLine(entry.Location);  // Save location
                    writer.WriteLine(entry.Mood);      // Save mood
                    writer.WriteLine(string.Join(",", entry.Tags));  // Save tags as comma-separated string
                    writer.WriteLine("----------");
                }
            }
            Console.WriteLine("Journal saved to file.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to file: {ex.Message}");
        }
    }

    public void LoadJournalFromFile()
    {
        Console.Write("Enter filename to load journal: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File does not exist.\n");
            return;
        }

        try
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            for (int i = 0; i < lines.Length; i += 7)
            {
                Entry entry = new Entry
                {
                    Prompt = lines[i],
                    Response = lines[i + 1],
                    Date = DateTime.Parse(lines[i + 2]),
                    Location = lines[i + 3],  // Load location
                    Mood = lines[i + 4],      // Load mood
                    Tags = lines[i + 5].Split(',').Select(tag => tag.Trim()).ToList() // Load tags as list
                };
                _entries.Add(entry);
            }
            Console.WriteLine("Journal loaded from file.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from file: {ex.Message}");
        }
    }

    public void ShowMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournalToFile();
                    break;
                case "4":
                    LoadJournalFromFile();
                    break;
                case "5":
                    exit = true;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Journal journal = new Journal();
        journal.ShowMenu();
    }
}
