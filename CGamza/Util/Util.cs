using System;
using System.Collections.Generic;
using Colorify;

namespace CGamza.Util
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
          case ConsoleKey.D1:
            if (questions.Count > 0)
              cursor = 0;
            break;
          case ConsoleKey.D2:
            if (questions.Count > 1)
              cursor = 1;
            break;
          case ConsoleKey.D3:
            if (questions.Count > 2)
              cursor = 2;
            break;
          case ConsoleKey.D4:
            if (questions.Count > 3)
              cursor = 3;
            break;
          case ConsoleKey.D5:
            if (questions.Count > 4)
              cursor = 4;
            break;
          case ConsoleKey.D6:
            if (questions.Count > 5)
              cursor = 5;
            break;
          case ConsoleKey.D7:
            if (questions.Count > 6)
              cursor = 6;
            break;
          case ConsoleKey.D8:
            if (questions.Count > 7)
              cursor = 7;
            break;
          case ConsoleKey.D9:
            if (questions.Count > 8)
              cursor = 8;
            break;
          case ConsoleKey.D0:
            if (questions.Count > 9)
              cursor = 9;
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

    public static string AskLine(string question, Func<string, bool> check, bool isInline = false)
    {
      string result;
      do
      {
        result = AskLine(question, isInline);
      }
      while (!check(result));

      return result;
    }
  }
}
