using System.ComponentModel.DataAnnotations;

namespace SchoolGameSite.Models;

public class Question
{
    public int Id { get; set; }
    public string? QuestionText { get; set; }
    public Answer[]? Answers { get; set; }
    public int Difficulty { get; set; }
}

public class Answer
{
    public int Id { get; set; }
    public string? Text { get; set; }
    
}