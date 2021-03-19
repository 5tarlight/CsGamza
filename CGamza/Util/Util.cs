using System;
using System.Collections.Generic;
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

    public static void Pause()
    {
      WriteColor("계속하려면 아무키나 누르세요.");
      Console.ReadKey();
    }
    
    public static int AskSelectableQuestion(string state, List<SelectableQuestion> questions)
    {
      var cursor = 0;
      var selectionEnd = false;

      while (!selectionEnd)
      {
        Console.Clear();
        
        WriteColor(state);
        WriteColor("");
        
        for (int i = 0; i < questions.Count; i++)
        {
          var msg = $"{i + 1}. {questions[i].GetQuestion()}";
          
          if (i == cursor)
            WriteColor(msg);
          else
            WriteColor(msg, Colors.txtMuted);
        }
        
        Console.WriteLine();
        WriteColor("\n↑ ↓ Enter");

        ConsoleKey key = Console.ReadKey().Key;

        switch (key)
        {
          case ConsoleKey.UpArrow:
            if (cursor != 0) cursor--;
            break;
          case ConsoleKey.DownArrow:
            if (cursor != questions.Count - 1) cursor++;
            break;
          case ConsoleKey.Enter:
            selectionEnd = true;
            break;
        }
      }

      return cursor;
    }

    public static string AskLine(string question, bool isInline = false)
    {
      if (isInline)
      {
        WriteColor($"{question} : ", Colors.txtDefault, true);

        return Console.ReadLine();
      }
      else
      {
        WriteColor(question);

        return Console.ReadLine();
      }
    }
  }
}
