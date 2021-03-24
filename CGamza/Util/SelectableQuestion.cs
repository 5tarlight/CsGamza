namespace CGamza.Util
{
  public class SelectableQuestion
  {
    private string question;

    public SelectableQuestion(string question)
    {
      this.question = question;
    }

    public string GetQuestion()
    {
      return question;
    }

    public override string ToString()
    {
      return question;
    }
  }
}
