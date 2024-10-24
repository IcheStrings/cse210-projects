using System;
using System.Collections.Generic;
using System.Linq;

public class Word
{
    private string _text;       // The actual text of the word
    private bool _isHidden;     // State to track if the word is hidden

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public string GetDisplayText()
    {
        return _isHidden ? "_____" : _text; // Display underscores if hidden
    }

    public void Hide()
    {
        _isHidden = true; // Hide the word
    }

    public bool IsHidden()
    {
        return _isHidden; // Check if the word is hidden
    }
}

public class Reference
{
    private string _book;           // The book of scripture
    private int _chapter;           // The chapter number
    private int _verseStart;        // The starting verse number
    private int _verseEnd;          // The ending verse number

    
    
    // Constructor for a single verse
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verse;
        _verseEnd = verse; // Same verse for single reference
    }

    // Constructor for a range of verses
    public Reference(string book, int chapter, int verseStart, int verseEnd)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verseStart;
        _verseEnd = verseEnd;
    }

    public string GetDisplayText()
    {
        return _verseStart == _verseEnd 
            ? $"{_book} {_chapter}:{_verseStart}" 
            : $"{_book} {_chapter}:{_verseStart}-{_verseEnd}";
    }
}

public class Scripture
{
    private Reference _reference;          // Holds the reference of the scripture
    private List<Word> _words;             // List of words in the scripture
    private int _totalHiddenWords;         // Total number of hidden words during the session

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList(); // Split text into words
        _totalHiddenWords = 0; // Initialize total hidden words
    }

    public void Display()
    {
        Console.WriteLine(_reference.GetDisplayText()); // Display reference
        Console.WriteLine(string.Join(" ", _words.Select(word => word.GetDisplayText()))); // Display words
    }

    public void HideRandomWords(int count)
    {
        Random random = new Random();
        int hiddenCount = 0;

        while (hiddenCount < count && !AllWordsHidden())
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                hiddenCount++;
                _totalHiddenWords++; // Increment total hidden words count
            }
        }
    }

    private bool AllWordsHidden()
    {
        return _words.All(word => word.IsHidden()); // Check if all words are hidden
    }

    // New method to get the total number of hidden word, is my own additional requirement. The variable added will help to keep track of the total number of words that have been hidden throughout the session
    public int GetTotalHiddenWords()
    {
        return _totalHiddenWords; // Return the total number of hidden words
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Reference reference = new Reference("Proverbs", 3, 5, 6); // Create a scripture reference
        Scripture scripture = new Scripture(reference, "Trust in the Lord with all your heart and lean not on your own understanding.");

        while (true)
        {
            Console.Clear(); // Clear the console screen
            scripture.Display(); // Display the current scripture

            // Display total hidden words to the user
            Console.WriteLine($"\nTotal words hidden so far: {scripture.GetTotalHiddenWords()}");

            Console.WriteLine("\nPress Enter to hide some words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input?.ToLower() == "quit")
            {
                break; // Exit the program if the user types 'quit'
            }

            scripture.HideRandomWords(2); // Hide 2 random words each time
        }
    }
}
