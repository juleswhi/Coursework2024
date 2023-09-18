namespace QuestionLib;

public class TextQuestion : IQuestionable
{
    public static QuestionType QuestionType { get; } = QuestionType.Text;
    public string Question { get; set; } = String.Empty;
    public List<string> Answers { get; set; } = new();
}
