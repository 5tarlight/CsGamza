using System;
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
      
      Util.DisplayLogo();
    }
  }
}
