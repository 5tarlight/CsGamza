using System;
using System.Collections.Generic;
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

      var q = new List<SelectableQuestion>();
      q.Add(new SelectableQuestion("캐릭터 확인하기"));
      q.Add(new SelectableQuestion("도감 확인하기"));

      Util.AskSelectableQuestion(q);
    }
  }
}
