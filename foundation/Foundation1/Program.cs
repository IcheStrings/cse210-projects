using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = CreateVideos();
        DisplayVideos(videos);
    }

    static List<Video> CreateVideos()
    {
        // Create videos
        var video1 = new Video("Amazing Video 1", "Alice", 120);
        video1.AddComment(new Comment("Bob", "Great video!"));
        video1.AddComment(new Comment("Charlie", "I love the content!"));
        video1.AddComment(new Comment("David", "Keep it up, Alice!"));

        var video2 = new Video("Amazing Video 2", "Bob", 180);
        video2.AddComment(new Comment("Alice", "Nice video, Bob!"));
        video2.AddComment(new Comment("Eve", "Very informative."));
        video2.AddComment(new Comment("Frank", "Can't wait for the next one!"));

        var video3 = new Video("Amazing Video 3", "Charlie", 150);
        video3.AddComment(new Comment("David", "Excellent!"));
        video3.AddComment(new Comment("Bob", "I learned a lot from this."));
        video3.AddComment(new Comment("Alice", "This is awesome, Charlie!"));

        // Add videos to a list
        var videos = new List<Video> { video1, video2, video3 };

        return videos;
    }

    static void DisplayVideos(List<Video> videos)
    {
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");

            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}
