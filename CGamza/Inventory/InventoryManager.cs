using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CGamza.Player;
using CGamza.util;
using Colorify;

namespace CGamza.Inventory
{
  public class InventoryManager
  {
    private const string Dir = "data";
    private const string Suffix = ".idata";

    private static string Path(string name)
    {
      return $"{Dir}/{name}/inventory{Suffix}";
    }

    public static void initEmptyInventory(string name)
    {
      var path = Path(name);

      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();

      serializer.Serialize(ws, new Inventory());
      ws.Close();
    }

    public static Inventory loadInventory(string name)
    {
      for (int i = 0; i < 5; i++)
      {
        try
        {
          var path = Path(name);
          Stream ws = new FileStream(path, FileMode.OpenOrCreate);
          BinaryFormatter deserializer = new BinaryFormatter();

          var result = (Inventory) deserializer.Deserialize(ws);
          ws.Close();

          return result;
        }
        catch (Exception)
        {
          Util.WriteColor("Retrying...");
        }
      }

      return null;
    }

    public static void saveCurrentInventory()
    {
      if (PlayerManager.CurrentInventory == null) return;

      var path = Path(PlayerManager.CurrentPlayer.Name);

      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();

      serializer.Serialize(ws, PlayerManager.CurrentInventory);
      ws.Close();
    }

    public static void DisplayCurrentInventory(int row = 5)
    {
      var inv = PlayerManager.CurrentInventory;
      if (inv == null) return;
      var page = 0;
      var index = 0;

      while (true)
      {
        Console.Clear();

        var list = inv.items.Count - 1 >= page * row + row
          ? inv.items.GetRange(page * row, row)
          : inv.items.GetRange(page * row, inv.items.Count - page * row);

        for (var i = 0; i < list.Count; i++)
        {
          var msg = $"{page * row + i + 1}. {list[i].Item.Name} x{list[i].Count}";

          if (i == index)
            Util.WriteColor(msg);
          else
            Util.WriteColor(msg, Colors.txtMuted);
        }

        Util.WriteColor("");
        Util.WriteColor($"\n페이지: {page + 1}, 개수 : {page * row + index + 1}/{inv.items.Count}");
        Util.WriteColor("↑ ↓ ← →");
        Util.WriteColor("Enter로 선택, Esc로 종료");

        var key = Console.ReadKey().Key;

        switch (key)
        {
          case ConsoleKey.UpArrow:
            if (index != 0) index--;
            break;
          case ConsoleKey.DownArrow:
            if (index != inv.items.Count - 1) index++;
            break;
          case ConsoleKey.LeftArrow:
            if (page != 0)
            {
              page--;
              index = 0;
            }

            break;
          case ConsoleKey.RightArrow:
            var maxPage = (int) Math.Ceiling(((double) inv.items.Count / row)) - 1;
            if (page != maxPage)
            {
              page++;
              index = 0;
            }

            break;
          case ConsoleKey.Enter:
            return;
          case ConsoleKey.Escape:
            return;
        }
      }

      //   
      //   while (true)
      //   {
      //     Console.Clear();
      //     var pageItem = inv.items.Count > itemInPage ? itemInPage : inv.items.Count;
      //
      //     for (var i = 0; i < pageItem; i++)
      //     {
      //       var index = currentPage * itemInPage + cursor;
      //
      //       string msg;
      //       
      //       if (inv.items.Count > 0)
      //       {
      //         var curItem = inv.items[index];
      //         msg = $"{i + 1}. {curItem.Item.Name}";
      //       }
      //       else
      //         msg = "아이템이 없습니다.";
      //
      //       if (i == cursor)
      //         Util.WriteColor(msg);
      //       else
      //         Util.WriteColor(msg, Colors.txtMuted);
      //     }
      //     
      //     Util.WriteColor("");
      //     Util.WriteColor($"{currentPage + 1} / {maxPage}");
      //     Util.WriteColor("← → ↑ ↓ 로 이동, ESC를 눌러 나가기");
      //
      //     var key = Console.ReadKey().Key;
      //
      //     switch (key)
      //     {
      //       case ConsoleKey.UpArrow:
      //         if (cursor > 0) cursor--;
      //         break;
      //       case ConsoleKey.DownArrow:
      //         if (cursor < itemInPage - 1) cursor++;
      //         break;
      //       case ConsoleKey.LeftArrow:
      //         if (currentPage != 0)
      //         {
      //           currentPage--;
      //           cursor = 0;
      //         }
      //
      //         break;
      //       case ConsoleKey.RightArrow:
      //         if (currentPage != maxPage - 1)
      //         {
      //           currentPage++;
      //           cursor = 0;
      //         }
      //         
      //         break;
      //       case ConsoleKey.Enter:
      //         if (inv.items.Count == 0)
      //           goto endOfWhile;
      //         Util.WriteColor("개발중");
      //         Util.Pause();
      //         break;
      //       case ConsoleKey.Escape:
      //         goto endOfWhile;
      //     }
      //   }
      //   endOfWhile: {}
    }
  }
}