public class Job
    public int _startYear;
    public int _endYear;

    public void DisplayDetails()
    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
