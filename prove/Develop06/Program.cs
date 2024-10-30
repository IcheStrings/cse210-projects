using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;
    protected int _points;
    protected bool _isComplete;

    public Goal(string name, int points)
    {
        _name = name;
        _points = points;
        _isComplete = false;
    }

    public abstract void RecordEvent();
    public abstract string GetDetails();
    public virtual bool IsComplete() => _isComplete;
    public int GetPoints() => _points;
}

// Simple Goal class
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) {}

    public override void RecordEvent()
    {
        _isComplete = true;
        Console.WriteLine($"Completed {_name} and earned {_points} points!");
    }

    public override string GetDetails()
    {
        return $"{_name} - Simple Goal: [ {(IsComplete() ? "X" : " ")} ]";
    }
}

// Eternal Goal class
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) {}

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded {_name} and earned {_points} points!");
    }

    public override string GetDetails()
    {
        return $"{_name} - Eternal Goal";
    }
}

// Checklist Goal class
class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _currentCount = 0;
    }

    public override void RecordEvent()
    {
        _currentCount++;
        if (_currentCount >= _targetCount)
        {
            _isComplete = true;
            Console.WriteLine($"Completed {_name} and earned {_points + _bonusPoints} points including bonus!");
        }
        else
        {
            Console.WriteLine($"Recorded {_name}, completed {_currentCount}/{_targetCount} times, earned {_points} points!");
        }
    }

    public override string GetDetails()
    {
        return $"{_name} - Checklist Goal: Completed {_currentCount}/{_targetCount} [ {(IsComplete() ? "X" : " ")} ]";
    }
}

// Goal Manager class to handle user interactions and storage
class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _totalScore = 0;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex < 0 || goalIndex >= _goals.Count)
        {
            Console.WriteLine("Invalid goal selection.");
            return;
        }
        Goal goal = _goals[goalIndex];
        goal.RecordEvent();
        _totalScore += goal.GetPoints();
        if (goal.IsComplete()) _totalScore += goal.GetPoints(); // Optional: Add bonus points on completion
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetails()}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {_totalScore}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_totalScore);
            foreach (Goal goal in _goals)
            {
                writer.WriteLine($"{goal.GetType().Name}:{goal.GetDetails()}");
            }
        }
        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                _totalScore = int.Parse(reader.ReadLine());
                while (!reader.EndOfStream)
                {
                    string[] goalData = reader.ReadLine().Split(':');
                    string goalType = goalData[0];
                    // Additional parsing logic can be implemented based on saved goal details
                }
            }
            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("Save file not found.");
        }
    }
}

// Program Class to handle user menu
class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        bool quit = false;

        while (!quit)
        {
            Console.WriteLine("\n=== Eternal Quest ===");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. View Goals");
            Console.WriteLine("4. View Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("0. Quit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Select Goal Type:\n1. Simple\n2. Eternal\n3. Checklist");
                    string goalType = Console.ReadLine();
                    Console.Write("Enter Goal Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Points: ");
                    int points = int.Parse(Console.ReadLine());

                    if (goalType == "1")
                    {
                        goalManager.AddGoal(new SimpleGoal(name, points));
                    }
                    else if (goalType == "2")
                    {
                        goalManager.AddGoal(new EternalGoal(name, points));
                    }
                    else if (goalType == "3")
                    {
                        Console.Write("Enter Target Count: ");
                        int targetCount = int.Parse(Console.ReadLine());
                        Console.Write("Enter Bonus Points: ");
                        int bonusPoints = int.Parse(Console.ReadLine());
                        goalManager.AddGoal(new ChecklistGoal(name, points, targetCount, bonusPoints));
                    }
                    break;

                case "2":
                    goalManager.DisplayGoals();
                    Console.Write("Select Goal to Record Event: ");
                    int goalIndex = int.Parse(Console.ReadLine()) - 1;
                    goalManager.RecordEvent(goalIndex);
                    break;

                case "3":
                    goalManager.DisplayGoals();
                    break;

                case "4":
                    goalManager.DisplayScore();
                    break;

                case "5":
                    goalManager.SaveGoals("goals.txt");
                    break;

                case "6":
                    goalManager.LoadGoals("goals.txt");
                    break;

                case "0":
                    quit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
