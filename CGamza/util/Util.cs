using Colorify;

namespace CGamza.util
{
  public class Util
  {
    public static Format Colorify { get; set; }
    
    public static void DisplayLogo()
    {
      WriteColor("환영합니다.", Colors.txtSuccess);
    }

    public static void WriteColor(string text, string color = Colors.txtDefault, bool isInline = false)
    {
      if (isInline) Colorify.Write(text, color);
      else Colorify.WriteLine(text, color);
    }
  }
}
