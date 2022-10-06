namespace SchoolGameSite.Models;

public class QuestionSet
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Question>? Questions { get; set; }
    public int Timer { get; set; }
}