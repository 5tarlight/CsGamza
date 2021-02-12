using System;
using System.Collections.Generic;
using CGamza.Player;
using CGamza.util;
using Colorify;
using Colorify.UI;

namespace CGamza
{
  class Program
  {
    static void Main(string[] args)
    {
      Util.Colorify = new Format(Theme.Dark);
      Console.Clear();
      
      Util.DisplayLogo();
      Util.Pause();

      PlayerManager.LoadPlayer();

      while (true)
      {
        var q = new List<SelectableQuestion>();
        q.Add(new SelectableQuestion("캐릭터 확인하기"));
        q.Add(new SelectableQuestion("경험치 올리기"));

        var answer = Util.AskSelectableQuestion(q);

        switch (answer)
        {
          case 0:
            Console.Clear();
            PlayerManager.PrintCurrnetPlayerInfo();
            Util.Pause();
            break;
          case 1:
            PlayerManager.CurrentPlayer.SetExp(2, SetExpAction.Up);
            Util.WriteColor("경험치를 올렸다.");
            Util.Pause();
            break;
        }
      }
    }
  }
}
