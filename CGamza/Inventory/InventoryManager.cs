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

    public static void DisplayCurrentInventory()
    {
      var inv = PlayerManager.CurrentInventory;
      if (inv == null) return;
      
      const int itemInPage = 5;
      var maxPage = inv.items.Count / itemInPage;
      var currentPage = 0;
      var cursor = 0;
      
      while (true)
      {
        Console.Clear();
        int pageItem;

        if (inv.items.Count > itemInPage)
          pageItem = itemInPage;
        else
          pageItem = inv.items.Count;
        
        for (int i = 0; i < pageItem; i++)
        {
          var index = currentPage * itemInPage + cursor;
          Inventory.InventoryItem invItem;

          string msg;
          
          if (inv.items.Count > 0)
          {
            invItem = inv.items[index];
            msg = $"{i + 1}. {invItem.Item.Name}";
          }
          else
          {
            invItem = null;
            msg = "아이템이 없습니다.";
          }

          if (i == cursor)
            Util.WriteColor(msg);
          else
            Util.WriteColor(msg, Colors.txtMuted);
        }
        
        Util.WriteColor("");
        Util.WriteColor($"{currentPage} / {maxPage + 1}");
        Util.WriteColor("← → ↑ ↓ 로 이동, ESC를 눌러 나가기");

        var key = Console.ReadKey().Key;

        switch (key)
        {
          case ConsoleKey.UpArrow:
            if (cursor > 0) cursor--;
            break;
          case ConsoleKey.DownArrow:
            if (cursor < itemInPage - 1) cursor++;
            break;
          case ConsoleKey.LeftArrow:
            if (currentPage != 0)
            {
              currentPage--;
              cursor = 0;
            }

            break;
          case ConsoleKey.RightArrow:
            if (currentPage != maxPage)
            {
              currentPage++;
              cursor = 0;
            }
            
            break;
          case ConsoleKey.Enter:
            if (inv.items.Count == 0)
              goto endOfWhile;
            Util.WriteColor("개발중");
            Util.Pause();
            break;
          case ConsoleKey.Escape:
            goto endOfWhile;
        }
      }
      endOfWhile: {}
    }
  }
}
